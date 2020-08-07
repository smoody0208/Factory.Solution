using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Factory.Models;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private readonly FactoryContext _db;
    
    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      IQueryable<Engineer> engineerQuery = _db.Engineers;
      IEnumerable<Engineer> engineerList = engineerQuery.ToList().OrderBy(engineers => engineers.Name);
      return View(engineerList);
    }

    public ActionResult Create()
    {
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "Name");
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer engineers, int MachineId, int LocationId)
    {
      _db.Engineers.Add(engineers);
      if (MachineId != 0)
      {
        _db.EngineerLocationMachine.Add(new EngineerLocationMachine() { MachineId = MachineId, EngineerId = engineers.EngineerId });
      }
      if (LocationId != 0)
      {
        _db.EngineerLocationMachine.Add(new EngineerLocationMachine() { LocationId = LocationId, EngineerId = engineers.EngineerId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineers.EngineerId });
    }

    public ActionResult Details(int id)
    {
      Engineer engineer = _db.Engineers
        .Include(engineers => engineers.LocationsMachines)
        .ThenInclude(join => join.Location)
        .Include(engineers => engineers.LocationsMachines)
        .ThenInclude(join => join.Machine)
        .First(engineers => engineers.EngineerId == id);
      ViewBag.LocationCount = _db.EngineerLocationMachine.Where(join => join.EngineerId == id).Where(join => join.LocationId != null).Count();
      ViewBag.MachineCount = _db.EngineerLocationMachine.Where(join => join.EngineerId == id).Where(join => join.MachineId != null).Count();
      return View(engineer);  
    }

    public ActionResult Edit(int id)
    {
      Engineer model = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult Edit(Engineer engineer)
    {
      _db.Entry(engineer).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }

    public ActionResult Delete(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      return View (thisEngineer);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      _db.Engineers.Remove(thisEngineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddMachine(int id)
    {
      Engineer model = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View(model);
    }
    
    [HttpPost]
    public ActionResult AddMachine(Engineer engineer, int machineId)
    {
      _db.EngineerLocationMachine.Add(new EngineerLocationMachine(){EngineerId = engineer.EngineerId, MachineId = machineId});
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = engineer.EngineerId});
    }
    
    public ActionResult AddLocation(int id)
    {
      Engineer model = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
      ViewBag.LocationId = new SelectList(_db.Locations, "LocationId", "Name");
      return View(model);
    }
    
    [HttpPost]
    public ActionResult AddLocation(Engineer engineer, int locationId)
    {
      _db.EngineerLocationMachine.Add(new EngineerLocationMachine(){EngineerId = engineer.EngineerId, LocationId = locationId});
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = engineer.EngineerId});
    }
  }
}
  