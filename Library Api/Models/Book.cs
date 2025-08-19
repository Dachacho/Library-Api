using System.ComponentModel.DataAnnotations;

namespace Library_Api.Models;

public class Book
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    public string Author { get; set; } = string.Empty;

    [Required]
    [Range(0, int.MaxValue)]
    public int CopiesAvailable { get; set; }
}