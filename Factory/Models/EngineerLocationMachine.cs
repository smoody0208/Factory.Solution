namespace Factory.Models
{
  public class EngineerLocationMachine
  {
    public int EngineerLocationMachineId { get; set; }
    public int? EngineerId { get; set; }
    public int? LocationId { get; set; }
    public int? MachineId { get; set; }
    public Engineer Engineer { get; set; }
    public Location Location { get; set; }
    public Machine Machine { get; set; }
  }
}