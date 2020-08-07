using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Factory.Models;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private readonly FactoryContext _db;
    
    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index(string name)
    {
      IQueryable<Machine> machineQuery = _db.Machines;
      if (!string.IsNullOrEmpty(name))
      {
        Regex search = new Regex(name, RegexOptions.IgnoreCase);
        machineQuery = machineQuery.Where(machines => search.IsMatch(machines.Name));
      }
      IEnumerable<Machine> machineList = machineQuery.ToList().OrderBy(machines => machines.Name);
      return View(machineList);
    }

    public ActionResult Create()
    {
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "Name");
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machines, int EngineerId, int LocationId)
    {
      _db.Machines.Add(machines);
      if (EngineerId != 0)
      {
        _db.EngineerLocationMachine.Add(new EngineerLocationMachine() { EngineerId = EngineerId, MachineId = machines.MachineId });
      }
      if (LocationId != 0)
      {
        _db.EngineerLocationMachine.Add(new EngineerLocationMachine() { LocationId = LocationId, MachineId = machines.MachineId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = machines.MachineId });
    }

    public ActionResult Details(int id)
    {
      Machine machine = _db.Machines
        .Include(machines => machines.EngineersLocations)
        .ThenInclude(join => join.Location)
        .Include(machines => machines.EngineersLocations)
        .ThenInclude(join => join.Engineer)
        .First(machines => machines.MachineId == id);
      ViewBag.LocationCount = _db.EngineerLocationMachine.Where(join => join.MachineId == id).Where(join => join.LocationId != null).Count();
      ViewBag.EngineerCount = _db.EngineerLocationMachine.Where(join => join.MachineId == id).Where(join => join.EngineerId != null).Count();
      return View(machine);  
    }

    public ActionResult Edit(int id)
    {
      Machine model = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine)
    {
      _db.Entry(machine).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = machine.MachineId });
    }

    public ActionResult Delete(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      return View (thisMachine);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Machine thisMachine = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      _db.Machines.Remove(thisMachine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddEngineer(int id)
    {
      Machine model = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(model);
    }
    
    [HttpPost]
    public ActionResult AddEngineer(Machine machine, int engineerId)
    {
      _db.EngineerLocationMachine.Add(new EngineerLocationMachine(){MachineId = machine.MachineId, EngineerId = engineerId});
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = machine.MachineId});
    }
    
    public ActionResult AddLocation(int id)
    {
      Machine model = _db.Machines.FirstOrDefault(machine => machine.MachineId == id);
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "Name");
      return View(model);
    }
    
    [HttpPost]
    public ActionResult AddLocation(Machine machine, int locationId)
    {
      _db.EngineerLocationMachine.Add(new EngineerLocationMachine(){MachineId = machine.MachineId, LocationId = locationId});
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = machine.MachineId});
    }
    
  }
}
  