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

        // =========================
        // INDEX
        // =========================
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .ToListAsync();

            return View(categories);
        }


        // =========================
        // CREATE - GET
        // =========================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        // =========================
        // CREATE - POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.categ_num))
            {
                ModelState.AddModelError(
                    "categ_num",
                    "Category Number is required.");
            }

            if (await _context.Categories
                .AnyAsync(x => x.categ_num == category.categ_num))
            {
                ModelState.AddModelError(
                    "categ_num",
                    "Category Number already exists.");
            }

            if (!ModelState.IsValid)
            {
                return View(category);
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // EDIT - GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.categ_num == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        // =========================
        // EDIT - POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            string oldId,
            Category category)
        {
            // الرقم الجديد إلزامي
            if (string.IsNullOrWhiteSpace(category.categ_num))
            {
                ModelState.AddModelError(
                    "categ_num",
                    "Category Number is required.");

                return View(category);
            }

            // هل الرقم الجديد مستخدم في سجل آخر؟
            bool duplicate = await _context.Categories
                .AsNoTracking()
                .AnyAsync(x =>
                    x.categ_num == category.categ_num &&
                    x.categ_num != oldId);

            if (duplicate)
            {
                ModelState.AddModelError(
                    "categ_num",
                    "Category Number already exists.");

                return View(category);
            }

            try
            {
                // تحديث الصف الموجود مباشرة.
                // لا نقوم بتغيير Primary Key لكيان EF المتتبع.
                int affectedRows = await _context.Database
                    .ExecuteSqlInterpolatedAsync($@"
                        UPDATE dbo.categories
                        SET
                            categ_num       = {category.categ_num},
                            categ_name      = {category.categ_name},
                            price           = {category.price},
                            qntt            = {category.qntt},
                            qnty            = {category.qnty},
                            qntytype        = {category.qntytype},
                            size            = {category.size},
                            color           = {category.color},
                            weight          = {category.weight},
                            model           = {category.model},
                            company         = {category.company},
                            production_date = {category.production_date},
                            expiry_date     = {category.expiry_date},
                            group_name      = {category.group_name},
                            group_num       = {category.group_num}
                        WHERE categ_num = {oldId}");

                if (affectedRows == 0)
                {
                    ModelState.AddModelError(
                        "",
                        "The category was not found.");

                    return View(category);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    "Error while saving: " + ex.Message);

                return View(category);
            }

            return RedirectToAction(nameof(Index));
        }


        // =========================
        // DELETE - GET
        // =========================
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.categ_num == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }


        // =========================
        // DELETE - POST
        // =========================
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.categ_num == id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}