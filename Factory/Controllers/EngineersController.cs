using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RecordCollection.Models;

namespace Factory.Controllers
{
  public class EngineersController : Controllers
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
  }
}