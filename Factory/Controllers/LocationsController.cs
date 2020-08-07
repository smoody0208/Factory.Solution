using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Factory.Models;

namespace Factory.Controllers
{
  public class LocationsController : Controller
  {
    private readonly FactoryContext _db;
    
    public LocationsController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index(string name)
    {
      IQueryable<Location> locationQuery = _db.Locations;
      if (!string.IsNullOrEmpty(name))
      {
        Regex search = new Regex(name, RegexOptions.IgnoreCase);
        locationQuery = locationQuery.Where(locations => search.IsMatch(locations.Name));
      }
      IEnumerable<Location> locationList = locationQuery.ToList().OrderBy(locations => locations.Name);
      return View(locationList);
    }

    public ActionResult Create()
    {
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Location locations, int MachineId, int EngineerId)
    {
      _db.Locations.Add(locations);
      if (MachineId != 0)
      {
        _db.EngineerLocationMachine.Add(new EngineerLocationMachine() { MachineId = MachineId, LocationId = locations.LocationId });
      }
      if (EngineerId != 0)
      {
        _db.EngineerLocationMachine.Add(new EngineerLocationMachine() { EngineerId = EngineerId, LocationId = locations.LocationId });
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = locations.LocationId });
    }

    public ActionResult Details(int id)
    {
      Location location = _db.Locations
        .Include(locations => locations.EngineersMachines)
        .ThenInclude(join => join.Engineer)
        .Include(locations => locations.EngineersMachines)
        .ThenInclude(join => join.Machine)
        .First(locations => locations.LocationId == id);
      ViewBag.EngineerCount = _db.EngineerLocationMachine.Where(join => join.LocationId == id).Where(join => join.EngineerId != null).Count();
      ViewBag.MachineCount = _db.EngineerLocationMachine.Where(join => join.LocationId == id).Where(join => join.MachineId != null).Count();
      return View(location);  
    }

    public ActionResult Edit(int id)
    {
      Location model = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      return View(model);
    }

    [HttpPost]
    public ActionResult Edit(Location location)
    {
      _db.Entry(location).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = location.LocationId });
    }

    public ActionResult Delete(int id)
    {
      Location thisLocation = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      return View (thisLocation);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Location thisLocation = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      _db.Locations.Remove(thisLocation);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddMachine(int id)
    {
      Location model = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View(model);
    }
    
    [HttpPost]
    public ActionResult AddMachine(Location location, int machineId)
    {
      _db.EngineerLocationMachine.Add(new EngineerLocationMachine(){EngineerId = location.LocationId, MachineId = machineId});
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = location.LocationId});
    }
    
    public ActionResult AddEngineer(int id)
    {
      Location model = _db.Locations.FirstOrDefault(location => location.LocationId == id);
      ViewBag.EngineerId = new SelectList(_db.Engineers, "EngineerId", "Name");
      return View(model);
    }
    
    [HttpPost]
    public ActionResult AddEngineer(Location location, int engineerId)
    {
      _db.EngineerLocationMachine.Add(new EngineerLocationMachine(){LocationId = location.LocationId, EngineerId = engineerId});
      _db.SaveChanges();
      return RedirectToAction("Details", new {id = location.LocationId});
    }
  }
}