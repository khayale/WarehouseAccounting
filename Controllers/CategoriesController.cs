using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseAccounting.Data;

namespace WarehouseAccounting.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var count = await _context.Categories.CountAsync();
                return Content($"DATABASE OK - Categories count: {count}");
            }
            catch (Exception ex)
            {
                return Content(
                    "DATABASE ERROR\n\n" +
                    ex.GetType().FullName + "\n\n" +
                    ex.Message,
                    "text/plain");
            }
        }
    }
}
