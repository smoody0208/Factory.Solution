using System.Collections.Generic;

namespace Factory.Models 
{
  public class Location
  {
    public Location()
    {
      this.EngineersMachines = new HashSet<EngineerLocationMachine>();
    }

    public int LocationId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<EngineerLocationMachine> EngineersMachines { get; set; }
  }
}