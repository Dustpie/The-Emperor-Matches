using API.DTOs;
using API.Entities;

namespace API.Interfaces;

/**
 * This interface is used to define the methods that will be used to interact with the User table in the database
 */
public interface IUserRepository
{
    void Update(User user);
    Task<bool> SaveAllSync();
    Task<IEnumerable<User>> GetUsersAsync();
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> GetUserByUsernameAsync(string username);
    Task<IEnumerable<MemberDto>> GetMembersAsync();
    Task<MemberDto?> GetMemberAsync(string username);
}