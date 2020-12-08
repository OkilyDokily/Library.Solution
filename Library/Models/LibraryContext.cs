using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Library.Models
{
  public class LibraryContext : IdentityDbContext
  {
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Book> Books { get; set; }
    public virtual DbSet<AuthorBook> AuthorBooks { get; set; }
    public virtual DbSet<Copy> Copies { get; set; }
    public virtual DbSet<Patron> Patrons { get; set; }
    public virtual DbSet<PatronCopy> PatronCopies { get; set; }

    public LibraryContext(DbContextOptions options) : base(options) { }
  }
}