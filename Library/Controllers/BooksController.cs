using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Library.Models;
using System;
using System.Security.Claims;

namespace Library.Controllers
{
  public class BooksController : Controller
  {
    public readonly LibraryContext _db;
    public BooksController(LibraryContext db)
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
    public ActionResult Create(Book b)
    {
      if (LibraryList.libraryList.Any(x => x == User.FindFirstValue(ClaimTypes.Name)))
      {
        _db.Books.Add(b);
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
      List<Book> books = _db.Books.ToList();
      return View(books);
    }

    [HttpPost]
    public ActionResult Index(string title)
    {
      return RedirectToAction("Results", new { title = title });
    }

    public ActionResult Results(string title)
    {
      List<Book> books = Book.SearchByTitle(this, title);
      return View(books);
    }

    public ActionResult Details(int id)
    {
      Book book = _db.Books.Include(x => x.Copies).Include(x => x.Authors).ThenInclude(x => x.Author).FirstOrDefault(x => x.Id == id);
      return View(book);
    }
  }
}