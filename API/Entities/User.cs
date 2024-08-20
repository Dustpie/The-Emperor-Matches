using API.Extensions;

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
    public byte[] PasswordHash { get; set; } = [];
    public byte[] PasswordSalt { get; set; } = [];
    
    public DateOnly DateOfBirth { get; set; }
    
    public required string KnownAs { get; set; }
    
    public DateTime Created { get; set; } = DateTime.UtcNow;
    
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    
    public required string Gender { get; set; }
    
    public string? Introduction { get; set; }
    
    public string? LookingFor { get; set; }
    
    public string? Interests { get; set; }
    
    public string? City { get; set; }
    
    public string? Country { get; set; }
    public List<Photo> Photos { get; set; } = []; // NAVIGATION PROPERTY
    
    /*
    public required HomeWorld HomeWorld { get; set; }
    
    public required bool ChaosTainted { get; set; }
    
    public required Faction Faction { get; set; }
    
    public List<Army> Armies { get; set; }
    
    **/     
    
}