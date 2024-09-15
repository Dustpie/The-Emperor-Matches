using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

/**
 * This class is a repository class that implements the IUserRepository interface
 */
public class UserRepository(DataContext context, IMapper mapper) : IUserRepository
{
    public async Task<IEnumerable<MemberDto>> GetMembersAsync()
    {
        return await context.Users
            .ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .ToListAsync();
    }
    
    public async Task<MemberDto?> GetMemberAsync(string username)
    {
        return await context.Users
            .Where(x => x.UserName == username).ProjectTo<MemberDto>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
    }
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
        return await context.Users.Include(p => p.Photos).ToListAsync(); // Eager loading the photos
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await context.Users.Include(p => p.Photos).SingleOrDefaultAsync(x => x.UserName == username);
    }
    
    
}