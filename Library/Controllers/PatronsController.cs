using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;


namespace Library.Controllers
{
  public class PatronsController : Controller
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly LibraryContext _db;
    public PatronsController(LibraryContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }

    public ActionResult Index()
    {
      if (LibraryList.libraryList.Any(x => x == User.FindFirstValue(ClaimTypes.Name)))
      {
        List<Patron> patrons = _db.Patrons.ToList();
        return View(patrons);
      }
      else
      {
        return RedirectToAction("Index", "Home");
      }
    }

    public async Task<ActionResult> Details(int id)
    {
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      Patron p = _db.Patrons.Include(x => x.User).Include(x => x.Copies).ThenInclude(x => x.Copy).ThenInclude(x => x.Book).FirstOrDefault(x => x.User.Id == currentUser.Id);
      if ((LibraryList.libraryList.Any(x => x == User.FindFirstValue(ClaimTypes.Name)) || id == p.Id) && userId != null)
      {
        return View(p);
      }
      else
      {
        return RedirectToAction("Index", "Home");
      }

    }

    [HttpPost]
    public async Task<ActionResult> Checkout(int id)
    {

      string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      DateTime today = (DateTime.Now).AddDays(42);
      Patron p = _db.Patrons.FirstOrDefault(x => x.User.Id == currentUser.Id);

      PatronCopy pc = new PatronCopy() { CopyId = id, PatronId = p.Id, DueDate = today };
      Copy c = _db.Copies.FirstOrDefault(x => x.Id == id);
      c.IsCheckedOut = true;
      _db.Entry(c).State = EntityState.Modified;
      _db.PatronCopies.Add(pc);
      _db.SaveChanges();
      return RedirectToAction("Details", "Patrons", new { id = p.Id });
    }

    [HttpPost]
    public async Task<ActionResult> Return(int patroncopyid)
    {
      string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      Patron p = _db.Patrons.FirstOrDefault(x => x.User.Id == currentUser.Id);
      PatronCopy patronCopy = _db.PatronCopies.Include(x => x.Copy).FirstOrDefault(x => x.Id == patroncopyid);
      patronCopy.Returned = true;
      _db.Entry(patronCopy).State = EntityState.Modified;
      Copy c = _db.Copies.FirstOrDefault(x => x.Id == patronCopy.Copy.Id);
      c.IsCheckedOut = false;
      _db.Entry(c).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = p.Id });
    }
  }
}