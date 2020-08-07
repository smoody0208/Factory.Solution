using Microsoft.EntityFrameworkCore;

namespace Factory.Models
{
  public class FactoryContext : DbContext
  {
    public DbSet<Engineer> Engineers { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<EngineerLocationMachine> EngineerLocationMachine { get; set; }

    public FactoryContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Machine>().HasData(
        new Machine
        {
          MachineId = 1,
          Name = "Dreamweaver"
        },

        new Machine
        {
          MachineId = 2,
          Name = "Bubblewrappinator"
        },

        new Machine
        {
          MachineId = 3,
          Name = "Laughbox"
        }
      );
      modelBuilder.Entity<Location>().HasData(
        new Location
        {
          LocationId = 1,
          Name = "Who-Ville"
        },

        new Location
        {
          LocationId = 2,
          Name = "Mullberry"
        },
        
        new Location
        {
          LocationId = 3,
          Name = "The Jungle of Nool"
        }
      );
    }
  }
}  