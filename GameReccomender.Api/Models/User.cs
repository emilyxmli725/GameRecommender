using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameReccomender.Api.Models;

public class User
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long SteamId { get; set; }
    public string? DisplayName { get; set; }

    public string? AvatarUrl { get; set; }

    public  DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

}
