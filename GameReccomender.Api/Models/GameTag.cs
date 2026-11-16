using System;

namespace GameReccomender.Api.Models;

public class GameTag
{
    public int AppId { get; set; }
    public Game Game { get; set; }

    public int TagId { get; set; }
    public Tag Tag { get; set; }
    
    public int Weight { get; set; }
}
