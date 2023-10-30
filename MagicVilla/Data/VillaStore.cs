using MagicVilla.Models.DTOs;

namespace MagicVilla.Data;

public static class VillaStore
{
    public static List<VillaDTO?> villaList = new List<VillaDTO?>()
    {
        new() { Id = 1, Name = "PoolVIew", Sqft = 100, Occupancy = 4},
        new() { Id = 2, Name = "SeaView" , Sqft = 150, Occupancy = 5}
    };
    
    public static int GetNextId()
    {
        if (!villaList.Any()) return 1;
        
        return villaList.MaxBy(u => u?.Id ?? 0)!.Id + 1;
    }
}