using System.Collections.Generic;
using Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Library.Models
{
    public class Author
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public IEnumerable<AuthorBook> Books{get;set;}

        public static List<Author> Search(AuthorsController ac,string str)
        {
           List<Author> authors = ac._db.Authors.Where(x=>x.Name == str).ToList(); 
           return authors;     
        }
    }
}