using System.Linq.Expressions;
using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Repository;

public class VillaRepository : Repository<Villa>, IVillaRepository
{
    private readonly ApplicationDbContext _context;

    public VillaRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public new async Task<bool> UpdateAsync(Villa entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;

        return await base.UpdateAsync(entity);
    }
}