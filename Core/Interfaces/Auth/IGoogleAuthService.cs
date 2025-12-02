using Core.DTOs;
using System.Threading.Tasks;

namespace Core.Interfaces.Auth
{
    public interface IGoogleAuthService
    {
        Task<string> AuthenticateUserAsync(GoogleLoginDto googleLoginDto);
    }
}