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

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Patron p)
    {
      _db.Patrons.Add(p);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Index()
    {
      List<Patron> patrons = _db.Patrons.ToList();
      return View(patrons);
    }

    public ActionResult Details(int id)
    {
      Patron patron = _db.Patrons.Include(x => x.Copies).ThenInclude(x => x.Copy).ThenInclude(x=>x.Book).FirstOrDefault(x => x.Id == id);
      return View(patron);
    }

    [HttpPost]
    public async Task<ActionResult> Checkout(int id)
    {

      string userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      Patron p = _db.Patrons.FirstOrDefault(x => x.User.Id == currentUser.Id);

      PatronCopy pc = new PatronCopy() { CopyId = id, PatronId = p.Id };

      _db.PatronCopies.Add(pc);
      _db.SaveChanges();
      return RedirectToAction("Details", "Patrons", new { id = p.Id });
    }
  }
}