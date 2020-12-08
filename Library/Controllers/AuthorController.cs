using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class AuthorController : Controller
    {
        public readonly LibraryContext _db;

        public AuthorController(LibraryContext db)
        {
            _db = db;
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Author a)
        {
            _db.Authors.Add(a);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            List<Author> authors = _db.Authors.ToList();
            return View();
        }

        public ActionResult Details(int id)
        {
            Author author = _db.Authors.Include(a => a.Books).ThenInclude(ab => ab.Book).FirstOrDefault();
            return View(author);
        }

        public ActionResult Add(int id)
        {
            Book b = _db.Books.FirstOrDefault(x=>x.Id == id);
            return View(b);
        }
    }
}