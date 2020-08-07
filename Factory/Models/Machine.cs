using System.Collections.Generic;

namespace Factory.Models 
{
  public class Machine
  {
    public Machine()
    {
      this.EngineersLocations = new HashSet<EngineerLocationMachine>();
    }

    public int MachineId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual ICollection<EngineerLocationMachine> EngineersLocations { get; set; }
  }
}