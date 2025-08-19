using System.ComponentModel.DataAnnotations;

namespace Library_Api.Models;

public class User
{
    public int Id { get; set; }
    [Required] 
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;
}