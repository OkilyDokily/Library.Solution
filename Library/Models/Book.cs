using System.Collections.Generic;

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
    }
}