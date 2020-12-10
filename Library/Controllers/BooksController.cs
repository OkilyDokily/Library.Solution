using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Library.Models;
using System;

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
      return View();
    }

    [HttpPost]
    public ActionResult Create(Book b)
    {
      _db.Books.Add(b);
      _db.SaveChanges();
      return RedirectToAction("Index");
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
      Console.WriteLine(title);
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