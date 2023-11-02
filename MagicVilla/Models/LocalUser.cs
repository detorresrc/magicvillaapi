using Microsoft.AspNetCore.Identity;

namespace MagicVilla.Models;

public class LocalUser : IdentityUser
{
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}