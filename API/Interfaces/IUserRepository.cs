using API.Entities;

namespace API.Interfaces;

public interface IUserRepository
{
    void Update(User user);
    Task<bool> SaveAllSync();
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
}