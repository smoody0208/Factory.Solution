  using Microsoft.AspNetCore.Mvc;

namespace Factory.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Index(string searchOption, string searchString)
    {
      if (searchOption == "engineers")
      {
        return RedirectToAction("Index", "Engineers", new {name = searchString});
      }
      else if (searchOption == "locations")
      {
        return RedirectToAction("Index", "Locations", new {name = searchString});
      }
      else
      {
        return RedirectToAction("Index", "Machines", new {name = searchString});
      }
    }
  }
}    