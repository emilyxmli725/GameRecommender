using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using GameReccomender.Api.DTOs;
namespace GameReccomender.Api.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public GamesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        [HttpGet("library/{steamId}")]
        public async Task<IActionResult> GetUserLibrary(string steamId)
        {
            var apiKey = _configuration["Steam:ApiKey"];

            var url = $"http://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={apiKey}&steamid={steamId}&include_appinfo=true&include_played_free_games=true&format=json";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Failed to fetch data from Steam.");
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            var libraryData = JsonSerializer.Deserialize<SteamLibraryResponseDto>(jsonResult);

            var cleanGamesList = libraryData?.Response?.Games?
            .Where(g => g.PlaytimeForever > 0)
            .Select(g => new GameDto(
            g.AppId,
            g.Name,
            g.PlaytimeForever,
            $"http://media.steampowered.com/steamcommunity/public/images/apps/{g.AppId}/{g.ImgIconUrl}.jpg"
        ))
.ToList();
            return Ok(cleanGamesList);
        }
        [HttpGet("library/{steamId}/{appId}")]
        public async Task<IActionResult> GetUserGameDetails(string steamId, string appId)
        {
            var apiKey = _configuration["Steam:ApiKey"];

            var url = $"http://api.steampowered.com/IPlayerService/GetOwnedGames/v1/?key={apiKey}&steamid={steamId}&include_appinfo=true&format=json";

            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, "Failed to fetch data from Steam.");
            }

            var jsonResult = await response.Content.ReadAsStringAsync();
            var libraryData = System.Text.Json.JsonDocument.Parse(jsonResult);

            var gameDetails = libraryData.RootElement
                .GetProperty("response")
                .GetProperty("games")
                .EnumerateArray()
                .FirstOrDefault(game => game.GetProperty("appid").GetInt32().ToString() == appId);

            if (gameDetails.ValueKind == System.Text.Json.JsonValueKind.Undefined)
            {
                return NotFound($"Game with AppID {appId} not found in user's library.");
            }

            return Content(gameDetails.ToString(), "application/json");
        }
    }
}