using System;

namespace Library.Models
{
    public class PatronCopy
    {
        public int Id {get;set;}
        public int PatronId{get;set;}
        public virtual Patron Patron{get;set;}
        public int CopyId {get;set;}
        public DateTime DueDate{get;set;}
        public bool Returned {get;set;}
        public virtual Copy Copy{get;set;}
    }
}