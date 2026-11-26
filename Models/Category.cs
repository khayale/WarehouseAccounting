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
                return View(category);

            bool exists = await _context.Categories
                .AnyAsync(x => x.Id == category.Id);

            if (exists)
            {
                ModelState.AddModelError("Id", "رقم المنتج موجود مسبقاً.");
                return View(category);
            }

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: Categories/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            string id,
            Category model)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            if (!ModelState.IsValid)
                return View(model);

            var oldCategory = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == id);

            if (oldCategory == null)
                return NotFound();

            // إذا تم تغيير رقم المنتج
            if (id != model.Id)
            {
                bool newIdExists = await _context.Categories
                    .AnyAsync(x => x.Id == model.Id);

                if (newIdExists)
                {
                    ModelState.AddModelError(
                        "Id",
                        "رقم المنتج الجديد موجود مسبقاً.");

                    return View(model);
                }

                // إنشاء سجل جديد بالرقم الجديد
                var newCategory = new Category
                {
                    Id = model.Id,
                    Name = model.Name,
                    Price = model.Price,
                    Qntt = model.Qntt,
                    Qnty = model.Qnty,
                    QntyType = model.QntyType,
                    Size = model.Size,
                    Color = model.Color,
                    Weight = model.Weight,
                    Model = model.Model,
                    Company = model.Company,
                    ProductionDate = model.ProductionDate,
                    ExpiryDate = model.ExpiryDate,
                    GroupName = model.GroupName,
                    GroupNum = model.GroupNum
                };

                _context.Categories.Add(newCategory);

                // حذف السجل القديم
                _context.Categories.Remove(oldCategory);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // الرقم لم يتغير، نعدل بقية البيانات
            oldCategory.Name = model.Name;
            oldCategory.Price = model.Price;
            oldCategory.Qntt = model.Qntt;
            oldCategory.Qnty = model.Qnty;
            oldCategory.QntyType = model.QntyType;
            oldCategory.Size = model.Size;
            oldCategory.Color = model.Color;
            oldCategory.Weight = model.Weight;
            oldCategory.Model = model.Model;
            oldCategory.Company = model.Company;
            oldCategory.ProductionDate = model.ProductionDate;
            oldCategory.ExpiryDate = model.ExpiryDate;
            oldCategory.GroupName = model.GroupName;
            oldCategory.GroupNum = model.GroupNum;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Delete
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

            var category = await _context.Categories
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

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
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}