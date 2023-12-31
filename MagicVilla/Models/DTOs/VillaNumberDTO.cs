using System.ComponentModel.DataAnnotations;

namespace MagicVilla.Models.DTOs;

public class VillaNumberDTO
{
    [Required]
    public int VillaNo { get; set; }
    public string SpecialDetails { get; set; }
}