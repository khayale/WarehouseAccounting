using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseAccounting.Data;
using WarehouseAccounting.Models;

namespace WarehouseAccounting.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // =========================================================
        // INDEX
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _context.Categories
                    .ToListAsync();

                return View(categories);
            }
            catch (Exception ex)
            {
                return Content(
                    "INDEX ERROR\n\n" +
                    ex.ToString()
                );
            }
        }


        // =========================================================
        // CREATE - GET
        // =========================================================

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // =========================================================
        // CREATE - POST
        // =========================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            try
            {
                _context.Categories.Add(category);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(
                    "CREATE ERROR\n\n" +
                    ex.ToString()
                );
            }
        }


        // =========================================================
        // EDIT - GET
        // =========================================================

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return Content(
                        "EDIT ERROR\n\n" +
                        "The categ_num value is empty."
                    );
                }

                var category = await _context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(
                        c => c