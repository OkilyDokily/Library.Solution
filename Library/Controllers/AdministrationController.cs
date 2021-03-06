using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;

namespace Library.Controllers
{
  public class AdministrationController : Controller
  {
    private readonly LibraryContext _db;
    public AdministrationController(LibraryContext db)
    {
      _db = db;
    }

    public ActionResult Overdue()
    {
      if (LibraryList.libraryList.Any(x => x == User.FindFirstValue(ClaimTypes.Name)))
      {
        DateTime today = DateTime.Now.Date;
        List<PatronCopy> patroncopies = _db.PatronCopies.Include(x => x.Copy).ThenInclude(x => x.Book).Include(x => x.Patron).Where(x => (today.CompareTo(x.DueDate) > 0) && x.Returned == false).ToList();
        return View(patroncopies);
      }
      else
      {
        return RedirectToAction("Index", "Home");
      }
    }
  }
}