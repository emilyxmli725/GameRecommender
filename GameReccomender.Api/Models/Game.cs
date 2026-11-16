using System;
using System.ComponentModel.DataAnnotations; 
using System.ComponentModel.DataAnnotations.Schema;

namespace GameReccomender.Api.Models;

public class Game
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int AppId { get; set; }
    public string? Name { get; set; }
    
    public string? Developer { get; set; }

    public string? Price { get; set; }
    public int PlaytimeForever { get; set; }
    public string? ImgIconUrl { get; set; }

}
