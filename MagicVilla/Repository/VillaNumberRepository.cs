using MagicVilla.Data;
using MagicVilla.Models;
using MagicVilla.Repository.IRepository;

namespace MagicVilla.Repository;

public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
{
    private readonly ApplicationDbContext _context;

    public VillaNumberRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public new async Task<bool> UpdateAsync(VillaNumber entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;

        return await base.UpdateAsync(entity);
    }
}