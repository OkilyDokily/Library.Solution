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
    private readonly LibraryContext _db;
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

    public ActionResult Details(int id)
    {
      Book book = _db.Books.Include(x => x.Copies).FirstOrDefault(x => x.Id == id);

      return View(book);
    }
  }
}