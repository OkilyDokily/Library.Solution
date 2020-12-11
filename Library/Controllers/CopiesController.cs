using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Library.Controllers
{
  public class CopiesController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly LibraryContext _db;
    public CopiesController(LibraryContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }

    public ActionResult Create(int id)
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
    public ActionResult Create(int id, int number)
    {
      if (LibraryList.libraryList.Any(x => x == User.FindFirstValue(ClaimTypes.Name)))
      {
        for (int i = 0; i < number; i++)
        {
          Copy c = new Copy() { BookId = id, IsCheckedOut = false };
          _db.Copies.Add(c);
          _db.SaveChanges();
        }

        return RedirectToAction("Details", "Books", new { id = id });
      }
      else
      {
        return RedirectToAction("Index", "Home");
      }
    
    }

    public ActionResult Details(int id)
    {
      Copy c = _db.Copies.Include(x=>x.Book).FirstOrDefault(x => x.Id == id);
      return View(c);
    }
  }
}