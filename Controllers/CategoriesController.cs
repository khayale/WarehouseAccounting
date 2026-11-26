using Microsoft.AspNetCore.Mvc;

namespace WarehouseAccounting.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return Content("CATEGORIES OK");
        }

        public IActionResult Create()
        {
            return Content("CREATE OK");
        }

        public IActionResult Edit(string id)
        {
            return Content("EDIT OK - " + id);
        }

        public IActionResult Delete(string id)
        {
            return Content("DELETE OK - " + id);
        }
    }
}
