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

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.categ_num == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Category postedCategory)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            // تحميل السجل الأصلي
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.categ_num == id);

            if (category == null)
            {
                return NotFound();
            }

            // رقم التصنيف هو المفتاح الأساسي
            // لذلك لا نغيّره هنا.
            // نبقي category.categ_num كما هو.

            category.categ_name = postedCategory.categ_name;
            category.price = postedCategory.price;
            category.qntt = postedCategory.qntt;
            category.qnty = postedCategory.qnty;
            category.qntytype = postedCategory.qntytype;
            category.size = postedCategory.size;
            category.color = postedCategory.color;
            category.weight = postedCategory.weight;
            category.model = postedCategory.model;
            category.company = postedCategory.company;
            category.production_date = postedCategory.production_date;
            category.expiry_date = postedCategory.expiry_date;
            category.group_name = postedCategory.group_name;
            category.group_num = postedCategory.group_num;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(
                    "",
                    "حدث خطأ أثناء حفظ التعديل: " + ex.InnerException?.Message
                );

                return View(postedCategory);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Delete
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.categ_num == id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
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

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.categ_num == id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CategoryExists(string id)
        {
            return await _context.Categories
                .AnyAsync(c => c.categ_num == id);
        }
    }
}