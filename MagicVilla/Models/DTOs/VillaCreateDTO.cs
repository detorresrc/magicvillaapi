using System.ComponentModel.DataAnnotations;

namespace MagicVilla.Models.DTOs;

public class VillaCreateDTO
{ 
    [Required]
    [MaxLength(30)]
    public required string Name { get; set; }
    public string Details { get; set; }
    [Required]
    public double Rate { get; set; }
    public int Sqft { get; set; }
    public int Occupancy { get; set; }
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
}