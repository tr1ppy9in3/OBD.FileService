using Microsoft.EntityFrameworkCore;

using OBD.FileService.Users.UseCases;

using OBD.FileService.Users.Core;
using OBD.FileService.Users.Core.Auth.Options;

namespace OBD.FileService.DataAccess.Users;

/// <summary>
/// Реализация <see cref="IUserRepository"/>.
/// </summary>
public class UserRepository(Context context) : IUserRepository
{
    /// <summary>
    /// Контекст БД.
    /// </summary>
    private readonly Context _context = context ?? throw new ArgumentNullException(nameof(context));

    /// <inheritdoc/>
    public IAsyncEnumerable<BaseUser> GetAll()
    {
        return _context.Users
                       .AsNoTracking()
                       .AsAsyncEnumerable();
    }

    /// <inheritdoc/>
    public async Task<BaseUser?> Resolve(string login, string password)
    {
        return await _context.Users
                             .AsNoTracking()
                             .Include(u => u.Roles)
                             .FirstOrDefaultAsync(u => u.Login == login && u.PasswordHash == password);

    }

    /// <inheritdoc/>
    public async Task<BaseUser?> GetById(long id)
    {
        return await _context.Users
                             .Include(u => u.Roles)
                             .FirstOrDefaultAsync(u => u.Id == id);
    }

    /// <inheritdoc/>
    public async Task<BaseUser?> GetByEmail(string email)
    {
        return await _context.Users
                             .Include(u => u.Roles)
                             .FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <inheritdoc/>
    public async Task<BaseUser?> GetByLogin(string login)
    {
        return await _context.Users
                             .Include(u => u.Roles)
                             .FirstOrDefaultAsync(u => u.Login == login);
    }

    /// <inheritdoc/>
    public async Task ChangeEmail(BaseUser user, string email)
    {
        user.Email = email;
        await _context.SaveChangesAsync();

    }

    /// <inheritdoc/>
    public async Task ChangePassword(BaseUser user, string password, PasswordOptions passwordOptions)
    {
        user.SetPassword(password, passwordOptions);
        await _context.SaveChangesAsync();

    }

    /// <inheritdoc/>
    public async Task Add(BaseUser user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Update(BaseUser user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task Delete(BaseUser user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

    }

    /// <inheritdoc/>
    public async Task Delete(long id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user is not null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
