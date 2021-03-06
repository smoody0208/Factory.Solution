using System.Collections.Generic;
using System;

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
    public DateTime InspectionDate { get; set; }
    public bool Complete { get; set; }

    public virtual ICollection<EngineerLocationMachine> EngineersLocations { get; set; }
  }
}