using JAQOps.Data.Entities;
using JAQOps.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JAQOps.Data.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(JAQOpsDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted);
    }

    public async Task<IEnumerable<User>> GetUsersByTenantIdAsync(Guid tenantId)
    {
        return await _context.Users
            .AsNoTracking()
            .Where(u => u.TenantId == tenantId && !u.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<string>> GetUserRolesAsync(Guid userId)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted);

        if (user == null)
            return new List<string>();

        return user.UserRoles
            .Select(ur => ur.Role.Name)
            .ToList();
    }
}
