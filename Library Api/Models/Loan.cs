using System.ComponentModel.DataAnnotations;

namespace Library_Api.Models;

public class Loan
{
    public int Id { get; set; }
    
    [Required]
    public int UserId { get; set; }
    
    [Required]
    public int BookId { get; set; }
    
    [Required]
    public DateTime LoanDate { get; set; }
    
    public DateTime? ReturnDate { get; set; }
    public User User { get; set; }
    public Book Book { get; set; }
}