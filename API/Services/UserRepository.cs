using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class UserRepository(DataContext context) : IUserRepository
{
    
    public void Update(User user)
    {
        context.Entry(user).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllSync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await context.Users.SingleOrDefaultAsync(x => x.UserName == username);
    }
    
    
}