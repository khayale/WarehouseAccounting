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

        // GET: Categories
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();
                return View(categories);
            }
            catch (Exception ex)
            {
                return Content(
                    "INDEX ERROR\n\n" +
                    ex.Message +
                    "\n\nINNER ERROR:\n" +
                    (ex.InnerException?.Message ?? "None")
                );
            }
        }

        // GET: Categories/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
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
                ModelState.AddModelError(
                    "",
                    "خطأ أثناء إضافة التصنيف: " +
                    ex.InnerException?.Message ??
                    ex.Message
                );

                return View(category);
            }
        }

        // =========================================================
        // GET: Categories/Edit
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Content("EDIT ERROR: categ_num is empty.");
            }

            try
            {
                var category = await _context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.categ_num == id);

                if (category == null)
                {
                    return Content(
                        "EDIT ERROR: Category not found.\n\n" +
                        "categ_num = " + id
                    );
                }

                return View(category);
            }
            catch (Exception ex)
            {
                return Content(
                    "EDIT ERROR\n\n" +
                    "Message:\n" +
                    ex.Message +
                    "\n\nInner Error:\n" +
                    (ex.InnerException?.Message ?? "None") +
                    "\n\nFull Error:\n" +
                    ex.ToString()
                );
            }
        }

        // =========================================================
        // POST: Categories/Edit
        // =========================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Category postedCategory)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            try
            {
                // تحميل السجل الموجود من قاعدة البيانات
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.categ_num == id);

                if (category == null)
                {
                    return NotFound();
                }

                // =================================================
                // لا نغيّر categ_num لأنه Primary Key
                // =================================================

                category.categ_name = postedCategory.categ_name;
                category.price = postedCategory.price;
                category.qntt = postedCategory.qnt