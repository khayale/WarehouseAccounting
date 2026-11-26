using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
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

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .ToListAsync();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Category category)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            if (!ModelState.IsValid)
                return View(category);

            // التأكد من أن الرقم الجديد غير مستخدم
            if (id != category.Id)
            {
                bool exists = await _context.Categories
                    .AsNoTracking()
                    .AnyAsync(x => x.Id == category.Id);

                if (exists)
                {
                    ModelState.AddModelError(
                        "Id",
                        "رقم المنتج الجديد مستخدم بالفعل.");

                    return View(category);
                }
            }

            try
            {
                // تحديث الرقم والاسم مباشرة في SQL Server
                string sql = @"
                    UPDATE dbo.categories
                    SET categ_num = @newId,
                        categ_name = @name
                    WHERE categ_num = @oldId";

                var result = await _context.Database.ExecuteSqlRawAsync(
                    sql,
                    new SqlParameter("@newId", category.Id),
                    new SqlParameter("@name",
                        (object?)category.Name ?? DBNull.Value),
                    new SqlParameter("@oldId", id)
                );

                if (result == 0)
                {
                    ModelState.AddModelError(
                        "",
                        "لم يتم العثور على السجل القديم.");

                    return View(category);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    "خطأ أثناء حفظ الرقم: " + ex.Message);

                return View(category);
            }
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}