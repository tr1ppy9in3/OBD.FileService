using AutoMapper;

using OBD.FileService.Users.Core.Auth;
using OBD.FileService.Users.UseCases.Auth.Commands.LoginComamnd;

namespace OBD.FileService.Users.UseCases.Auth;

public class AuthMappingProfile : Profile
{
    public AuthMappingProfile()
    {
        CreateMap<Token, TokenModel>();
    }
}
