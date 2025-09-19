using ApiWebKut.Models;

namespace ApiWebKut.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateJwtToken(Users users);
    }
}
