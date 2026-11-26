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
                .ToListAsync();

            return View(categories);
        }

        // =========================
        // CREATE - GET
        // =========================
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
            if (!ModelState.IsValid)
                return View(category);

            category.Id = category.Id?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(category.Id))
            {
                ModelState.AddModelError("Id", "رقم المنتج مطلوب.");
                return View(category);
            }

            bool exists = await _context.Categories
                .AnyAsync(x => x.Id == category.Id);

            if (exists)
            {
                ModelState.AddModelError(
                    "Id",
                    "رقم المنتج موجود مسبقاً.");

                return View(category);
            }

            _context.Categories.Add(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // EDIT - GET
        // =========================
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

        // =========================
        // EDIT - POST
        // =========================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            string oldId,
            Category model)
        {
            if (string.IsNullOrWhiteSpace(oldId))
                return NotFound();

            oldId = oldId.Trim();

            // جلب السجل الأصلي من قاعدة البيانات
            var oldCategory = await _context.Categories
                .FirstOrDefaultAsync(x => x.Id == oldId);

            if (oldCategory == null)
                return NotFound();

            // تنظيف رقم المنتج
            model.Id = model.Id?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(model.Id))
            {
                ModelState.AddModelError(
                    "Id",
                    "رقم المنتج مطلوب.");

                return View(model);
            }

            // ==========================================
            // إذا لم يتغير رقم المنتج
            // ==========================================
            if (oldId == model.Id)
            {
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

            // ==========================================
            // إذا تم تغيير رقم المنتج
            // ==========================================

            bool newIdExists = await _context.Categories
                .AnyAsync(x => x.Id == model.Id);

            if (newIdExists)
            {
                ModelState.AddModelError(
                    "Id",
                    "رقم المنتج الجديد موجود مسبقاً.");

                return View(model);
            }

            // إنشاء السجل الجديد بنفس البيانات
            // مع الرقم الجديد
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

            // حذف الرقم القديم بعد إنشاء الجديد
            _context.Categories.Remove(oldCategory);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // =========================
        // DELETE - GET
        // =========================
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

        // =========================
        // DELETE - POST
        // =========================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return NotFound();

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