using System.Collections.Generic;
using Library.Controllers;
using System.Linq;

namespace Library.Models
{
    public class Book
    {
        public int Id {get;set;}
        public string Title {get;set;}
        public virtual IEnumerable<AuthorBook> Authors{get;set;}
        public virtual IEnumerable<Copy> Copies {get;set;}
        public Book()
        {
            this.Copies = new HashSet<Copy>();
            this.Authors = new HashSet<AuthorBook>();
        }

        public static List<Book> SearchByTitle(BooksController bc, string title)
        {
            return bc._db.Books.Where(x=>x.Title == title).ToList();
        }
    }
}