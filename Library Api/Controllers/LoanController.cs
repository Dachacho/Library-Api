using Library_Api.Data;
using Library_Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoanController : ControllerBase
{
    private readonly LibraryContext _context;

    public LoanController(LibraryContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
    {
        return await _context.Loans.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Loan>> GetLoan(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan == null) return NotFound();
        return loan;
    }

    [HttpPost]
    public async Task<ActionResult<Loan>> CreateLoan(Loan loan)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        
        var userExists = await _context.Users.AnyAsync(u => u.Id == loan.UserId);
        if(!userExists) return BadRequest("User not found");
        
        var book = await _context.Books.FindAsync(loan.BookId);
        if(book == null) return BadRequest("Book not found");

        if (book.CopiesAvailable <= 0) return BadRequest("No copies avaliable");

        if (loan.LoanDate > DateTime.Now)
        {
            return BadRequest("Loan date can not be in the future");
        }

        if (loan.ReturnDate != null && loan.ReturnDate < loan.LoanDate)
        {
            return BadRequest("Return date can not be in the future");
        }
        
        var duplicateLoan = await _context.Loans.AnyAsync(l =>
            l.UserId == loan.UserId &&
            l.BookId == loan.BookId &&
            l.ReturnDate == null);
        
        if (duplicateLoan)
            return BadRequest("User already has an active loan for this book.");
        
        book.CopiesAvailable--;
        
        _context.Loans.Add(loan);
        await _context.SaveChangesAsync();
        return  CreatedAtAction("GetLoan", new { id = loan.Id }, loan);
    }

    [HttpPost("{id}/return")]
    public async Task<IActionResult> ReturnLoan(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan == null) return NotFound();
        if (loan.ReturnDate != null) return BadRequest("Loan already returned");
        
        loan.ReturnDate = DateTime.Now;
        
        var book = await _context.Books.FindAsync(loan.BookId);
        if(book == null) return BadRequest("Book not found");
        
        book.CopiesAvailable++;
        
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateLoan(int id, Loan loan)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        _context.Entry(loan).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLoan(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if(loan == null) return NotFound();
        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}