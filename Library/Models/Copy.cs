using System.Collections.Generic;

namespace Library.Models
{
    public class Copy
    {
        public int Id {get;set;}
        public int BookId {get;set;}
        public virtual Book Book {get;set;}
        public bool IsCheckedOut{get;set;}
        public virtual IEnumerable<PatronCopy> Patrons{get;set;}
        public Copy()
        {
            this.Patrons = new HashSet<PatronCopy>();
        }
    }
}