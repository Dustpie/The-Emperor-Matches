namespace API.Entities;
// Namespace is the folder where the class is located

public class User
{
    // By using [Key] attribute, we are telling Entity Framework that this is the primary key, not needed now
    public int Id { get; set; }
    public required string UserName { get; set; }

    public User()
    {
        
    }
}