using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Data;

/* The usual class pattern would be to have a constructor that takes in a DbContextOptions object
 and passes it to the base class constructor

public class DataContext : DbContext {
    public DataContext(DbContextOptions options) : base(options) {
    }
}
*/

// Primary Constructor for DataContext
public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
}