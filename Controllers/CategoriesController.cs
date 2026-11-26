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
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Categories/Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
                return NotFound();

            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.categ_num == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: Categories/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            string id,
            string categ_num,
            string? categ_name,
            decimal? price,
            string? qntt,
            string? qnty,
            int? group_num)
        {
            if (id == null)
                return NotFound();

            // الرقم الجديد موجود مسبقاً؟
            if (id != categ_num)
            {
                bool exists = await _context.Categories
                    .AnyAsync(x => x.categ_num == categ_num);

                if (exists)
                {
                    ModelState.AddModelError(
                        "categ_num",
                        "رقم المنتج موجود مسبقاً."
                    );

                    var oldCategory = await _context.Categories
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.categ_num == id);

                    if (oldCategory != null)
                    {
                        oldCategory.categ_num = categ_num;
                        oldCategory.categ_name = categ_name;
                        oldCategory.price = price;
                        oldCategory.qntt = qntt;
                        oldCategory.qnty = qnty;
                        oldCategory.group_num = group_num;
                    }

                    return View(oldCategory);
                }
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.categ_num == id);

            if (category == null)
                return NotFound();

            category.categ_num = categ_num;
            category.categ_name = categ_name;
            category.price = price;
            category.qntt = qntt;
            category.qnty = qnty;
            category.group_num = group_num;

            if (ModelState.IsValid)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // GET: Categories/Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
                return NotFound();

            var category = await _context.Categories
                .FirstOrDefaultAsync(x => x.categ_num == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: Categories/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
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