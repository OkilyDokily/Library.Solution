using System.Collections.Generic;

namespace Library.Models
{
  public class Patron
  {
    public int Id { get; set; }
    public virtual ApplicationUser User { get; set; }
    public string Name { get; set; }
    public virtual IEnumerable<PatronCopy> Copies { get; set; }
    public Patron()
    {
      this.Copies = new HashSet<PatronCopy>();
    }
  }
}