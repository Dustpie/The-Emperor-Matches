namespace API.Entities;


/* Code First approach --> To update the database, we can update the entities and then run the migrations */

/**
 * This class is an entity class that represents the User table in the database
 */
public class User
{
    // By using [Key] attribute, we are telling Entity Framework that this is the primary key
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }

    public User()
    {
    }
}