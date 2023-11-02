using MagicVilla.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Data;

public class Seeder
{
    public static async Task SeedData(ApplicationDbContext applicationDbContext, UserManager<LocalUser> userManager)
    {
        var hasUser = await userManager.Users.AnyAsync();
        if (!hasUser)
        {
            var users = new List<LocalUser>
            {
                new ()
                {
                    Email = "detorresrc@gmail.com",
                    UserName = "detorresrc",
                    EmailConfirmed = true
                },
                new ()
                {
                    Email = "admin@gmail.com",
                    UserName = "admin",
                    EmailConfirmed = true
                }
            };

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "P@55w0rd");
            }
        }

        var hasVilla = await applicationDbContext.Villas.AnyAsync();
        if (!hasVilla)
        {
            var villas = new List<Villa>
            {
                new Villa
                {
                  Id=1,
                  Name = "Royal Villa",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa3.jpg",
                  Occupancy = 4,
                  Rate = 200,
                  Sqft = 550,
                  Amenity = "",
                  CreatedDate = DateTime.UtcNow,
                  UpdatedDate = DateTime.UtcNow
                },
                new Villa
                {
                  Id = 2,
                  Name = "Premium Pool Villa",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa1.jpg",
                  Occupancy = 4,
                  Rate = 300,
                  Sqft = 550,
                  Amenity="",
                  CreatedDate = DateTime.UtcNow,
                  UpdatedDate = DateTime.UtcNow
                },
                new Villa
                {
                  Id = 3,
                  Name = "Luxury Pool Villa",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa4.jpg",
                  Occupancy = 4,
                  Rate = 400,
                  Sqft = 750,
                  Amenity = "",
                  CreatedDate = DateTime.UtcNow,
                  UpdatedDate = DateTime.UtcNow
                },
                new Villa
                {
                  Id = 4,
                  Name = "Diamond Villa",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa5.jpg",
                  Occupancy = 4,
                  Rate = 550,
                  Sqft = 900,
                  Amenity = "",
                  CreatedDate = DateTime.UtcNow,
                  UpdatedDate = DateTime.UtcNow
                },
                new Villa
                {
                  Id = 5,
                  Name = "Diamond Pool Villa",
                  Details = "Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.",
                  ImageUrl = "https://dotnetmasteryimages.blob.core.windows.net/bluevillaimages/villa2.jpg",
                  Occupancy = 4,
                  Rate = 600,
                  Sqft = 1100,
                  Amenity = "",
                  CreatedDate = DateTime.UtcNow,
                  UpdatedDate = DateTime.UtcNow
                }
            };
            
            await applicationDbContext.Villas.AddRangeAsync(villas);
            await applicationDbContext.SaveChangesAsync();
        }
    }
}