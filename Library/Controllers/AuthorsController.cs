using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Library.Controllers
{
  public class AuthorsController : Controller
  {
    public readonly LibraryContext _db;

    public AuthorsController(LibraryContext db)
    {
      _db = db;
    }

    public ActionResult Create()
    {
      if (LibraryList.libraryList.Any(x => x == User.FindFirstValue(ClaimTypes.Name)))
      {
        return View();
      }
      else
      {
        return RedirectToAction("Index", "Home");
      }
    }

    [HttpPost]
    public ActionResult Create(Author a)
    {
      if (LibraryList.libraryList.Any(x => x == User.FindFirstValue(ClaimTypes.Name)))
      {
        _db.Authors.Add(a);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
      else
      {
        return RedirectToAction("Index", "Home");
      }
    }

    public ActionResult Index()
    {
      List<Author> authors = _db.Authors.ToList();
      return View(authors);
    }

    public ActionResult Details(int id)
    {
      Author author = _db.Authors.Include(a => a.Books).ThenInclude(ab => ab.Book).FirstOrDefault();
      return View(author);
    }

    public ActionResult Add(int id)
    {
      if (LibraryList.libraryList.Any(x => x == User.FindFirstValue(ClaimTypes.Name)))
      {
        Book b = _db.Books.FirstOrDefault(x => x.Id == id);
        return View(b);
      }
      else
      {
        return RedirectToAction("Index", "Home");
      }  
    }

    [HttpPost]
    public ActionResult Search(int id, string name)
    {
      return RedirectToAction("Results", new { id = id, name = name });
    }

    public ActionResult Results(int id, string name)
    {
      List<Author> authors = Author.Search(this, name);
      return View(authors);
    }

    [HttpPost, ActionName("Results")]
    public ActionResult ResultsPost(int id, int authorid)
    {
      _db.AuthorBooks.Add(new AuthorBook { AuthorId = authorid, BookId = id });
      _db.SaveChanges();
      return RedirectToAction("Details", "Books", new { id = id });
    }
  }
}