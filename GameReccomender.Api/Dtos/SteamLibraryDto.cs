using System.Text.Json.Serialization;
namespace GameReccomender.Api.DTOs
{
    public record SteamLibraryResponseDto
    {
        [JsonPropertyName("response")]
        public SteamLibraryDataDto Response { get; init; }
    }

    public record SteamLibraryDataDto
    {
        [JsonPropertyName("game_count")]
        public int GameCount { get; init; }

        [JsonPropertyName("games")]
        public List<GameDto> Games { get; init; }
    } 

    public record GameDto(
        [property: JsonPropertyName("appid")] int AppId,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("playtime_forever")] int PlaytimeForever,
        [property: JsonPropertyName("img_icon_url")] string ImgIconUrl
    );
}
