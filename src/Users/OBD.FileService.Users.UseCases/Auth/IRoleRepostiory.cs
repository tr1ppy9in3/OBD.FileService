using OBD.FileService.Users.Core.Auth;

namespace OBD.FileService.Users.UseCases.Auth;

public interface IRoleRepostiory
{
    public Task<Role> FindOrCreate(string name);

    public Task AddRoleToUser(Guid roleId, long userId);
}
