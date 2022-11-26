using DTO.Identity;

namespace Thoughts.Identity.DAL.Interfaces
{
    public interface IAuthUtils<TUser>
    {
        TokenResponse CreateSessionToken(TUser user, IList<string> role);
    }
}
