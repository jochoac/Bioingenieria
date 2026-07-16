using Bioingenieria.Models;

namespace Bioingenieria.Services;

public class UserService
{
    private readonly string _usersFilePath;
    private List<User> _users;

    public UserService(string usersFilePath)
    {
        _usersFilePath = usersFilePath;
        _users = JsonFileStore.Read<List<User>>(_usersFilePath) ?? SeedDefaultUsers();
    }

    private List<User> SeedDefaultUsers()
    {
        var users = new List<User>
        {
            new()
            {
                Username = "admin",
                PasswordHash = PasswordHasher.Hash("admin123"),
                Role = UserRole.Administrator,
                IsActive = true
            }
        };

        JsonFileStore.Write(_usersFilePath, users);
        return users;
    }

    public IReadOnlyList<User> GetAll()
    {
        return _users;
    }

    public User? FindByUsername(string username)
    {
        return _users.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.OrdinalIgnoreCase));
    }

    public void AddUser(string username, string password, UserRole role)
    {
        if (FindByUsername(username) != null)
        {
            throw new InvalidOperationException($"Ya existe un usuario '{username}'.");
        }

        _users.Add(new User
        {
            Username = username,
            PasswordHash = PasswordHasher.Hash(password),
            Role = role,
            IsActive = true
        });

        Save();
    }

    public void SetRoleAndStatus(string username, UserRole role, bool isActive)
    {
        var user = FindByUsername(username) ?? throw new InvalidOperationException($"Usuario '{username}' no encontrado.");
        user.Role = role;
        user.IsActive = isActive;
        Save();
    }

    public void ResetPassword(string username, string newPassword)
    {
        var user = FindByUsername(username) ?? throw new InvalidOperationException($"Usuario '{username}' no encontrado.");
        user.PasswordHash = PasswordHasher.Hash(newPassword);
        Save();
    }

    public void Deactivate(string username)
    {
        var user = FindByUsername(username) ?? throw new InvalidOperationException($"Usuario '{username}' no encontrado.");
        user.IsActive = false;
        Save();
    }

    private void Save()
    {
        JsonFileStore.Write(_usersFilePath, _users);
    }
}
