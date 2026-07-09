using Bioingenieria.Models;

namespace Bioingenieria.Services;

public class AuthService
{
    private readonly UserService _userService;

    public AuthService(UserService userService)
    {
        _userService = userService;
    }

    public User? TryLogin(string username, string password)
    {
        var user = _userService.FindByUsername(username);
        if (user is null || !user.IsActive)
        {
            return null;
        }

        return PasswordHasher.Verify(password, user.PasswordHash) ? user : null;
    }
}
