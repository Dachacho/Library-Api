using Library_Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Api.Data;

public class LibraryContext:DbContext
{ 
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
}