using System.Collections.Generic;

namespace Factory.Models 
{
  public class Engineer
  {
    public Engineer()
    {
      this.LocationsMachines = new HashSet<EngineerLocationMachine>();
    }

    public int EngineerId { get; set; }
    public string Name { get; set; }
    public virtual ICollection<EngineerLocationMachine> LocationsMachines { get; set; }
  }
}