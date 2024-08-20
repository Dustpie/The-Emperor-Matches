using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Photos")] // DB annotation
public class Photo
{
    
    public int Id { get; set; }
    
    public required string Url { get; set; }
    
    public bool IsMain { get; set; }
    
    public string PublicId { get; set; }
    
    // Navigation properties
    
    public int UserId { get; set; }

    public User User { get; set; } = null!; // ! --> Null forgiving operator
}