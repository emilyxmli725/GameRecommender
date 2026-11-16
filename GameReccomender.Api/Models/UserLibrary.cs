namespace GameReccomender.Api.Models
{
    public class UserLibrary
    {
        public long SteamId { get; set; }
        public User User { get; set; }

        public int AppId { get; set; }
        public Game Game { get; set; }

        public int PlaytimeForever { get; set; }
        public DateTime? LastPlayed { get; set; }
    }
}