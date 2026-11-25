using Microsoft.AspNetCore.Mvc;

namespace WarehouseAccounting.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
