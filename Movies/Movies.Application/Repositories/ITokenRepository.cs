
using Microsoft.AspNetCore.Identity;

namespace Movies.Application.Repositories
{
    public interface ITokenRepository
    {
        string CreateJwtToken(IdentityUser user, List<string> roles);
    }
}
