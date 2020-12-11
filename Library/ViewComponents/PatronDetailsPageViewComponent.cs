using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Library.ViewComponents
{
  [ViewComponent]
  public class PatronDetailsPageViewComponent : ViewComponent
  {
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly LibraryContext _db;
    public PatronDetailsPageViewComponent(LibraryContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
      string userId = UserClaimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      if(userId !=null)
      {
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        Patron p = _db.Patrons.Include(x => x.User).FirstOrDefault(x => x.User.Id == currentUser.Id);

        return View(p.Id);
      }
      else
      {
        return View(0);
      }
    }
  }
}