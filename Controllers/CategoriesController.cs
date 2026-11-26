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
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
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
                return Content(
                    "CREATE ERROR\n\n" +
                    ex.Message +
                    "\n\nINNER ERROR:\n" +
                    (ex.InnerException?.Message ?? "None")
                );
            }
        }

        // GET: Categories/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            try
            {
                var category = await _context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.categ_num == id);

                if (category == null)
                {
                    return NotFound();
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

        // POST: Categories/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            string id,
            Category postedCategory)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            try
            {
                // تحميل السجل الأصلي
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.categ_num == id);

                if (category == null)
                {
                    return NotFound();
                }

                // لا نغيّر categ_num
                // لأنه Primary Key

                category.categ_name =
                    postedCategory.categ_name;

                category.price =
                    postedCategory.price;

                category.qntt =
                    postedCategory.qntt;

                category.qnty =
                    postedCategory.qnty;

                category.qntytype =
                    postedCategory.qntytype;

                category.size =
                    postedCategory.size;

                category.color =
                    postedCategory.color;

                category.weight =
                    postedCategory.weight;

                category.model =
                    postedCategory.model;

                category.company =
                    postedCategory.company;

                category.production_date =
                    postedCategory.production_date;

                category.expiry_date =
                    postedCategory.expiry_date;

                category.group_name =
                    postedCategory.group_name;

                category.group_num =
                    postedCategory.group_num;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    "خطأ أثناء حفظ التعديل: " +
                    ex.Message
                );

                return View(postedCategory);
            }
        }

        // GET: Categories/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            try
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.categ_num == id);

                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
            }
            catch (Exception ex)
            {
                return Content(
                    "DELETE ERROR\n\n" +
                    ex.Message +
                    "\n\nINNER ERROR:\n" +
                    (ex.InnerException?.Message ?? "None")
                );
            }
        }

        // POST: Categories/Delete
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            try
            {
                var category = await _context.Categories
                    .FirstOrDefaultAsync(c => c.categ_num == id);

                if (category != null)
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(
                    "DELETE ERROR\n\n" +
                    ex.Message +
                    "\n\nINNER ERROR:\n" +
                    (ex.InnerException?.Message ?? "None")
                );
            }
        }

        private async Task<bool> CategoryExists(string id)
        {
            return await _context.Categories
                .AnyAsync(c => c.categ_num == id);
        }
    }
}