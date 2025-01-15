using Microsoft.EntityFrameworkCore;

using OBD.FileService.Users.Core.Auth;
using OBD.FileService.Users.UseCases.Auth;

namespace OBD.FileService.DataAccess.Users.Auth;

/// <summary>
/// 
/// </summary>
public class RoleRepository(Context context) : IRoleRepostiory
{
    private readonly Context _context = context
        ?? throw new ArgumentNullException(nameof(context));

    public async Task<Role> FindOrCreate(string name)
    {
        var role = _context.Roles.FirstOrDefault(r => r.Name == name);

        if (role is null)
        {
            role = new Role { Name = name };
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        return role;
    }

    public async Task AddRoleToUser(Guid roleId, long userId)
    {
        var userRole = await _context.RoleToUsers
                                     .FirstOrDefaultAsync(ru => ru.RoleId == roleId &&
                                                                ru.UserId == userId);

        if (userRole == null)
        {
            userRole = new UserRole { RoleId = roleId, UserId = userId };
            _context.RoleToUsers.Add(userRole);
            await _context.SaveChangesAsync();
        }
    }
}

