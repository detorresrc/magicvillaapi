using System.ComponentModel.DataAnnotations;

namespace MagicVilla.Models.DTOs;

public class LoginDTO
{
    [Required]
    public string Username { get; set; }

    [Required]
    public string Password { get; set; }
}