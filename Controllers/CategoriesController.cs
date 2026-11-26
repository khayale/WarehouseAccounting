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
                    .AsNoTracking()
                    .ToListAsync();

                return View(categories);
            }
            catch (Exception ex)
            {
                return Content(
                    "INDEX ERROR:\n\n" +
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
            try
            {
                // categ_num مطلوب
                if (string.IsNullOrWhiteSpace(category.categ_num))
                {
                    ModelState.AddModelError(
                        nameof(category.categ_num),
                        "Category Number is required."
                    );
                }

                // منع تكرار categ_num
                if (!string.IsNullOrWhiteSpace(category.categ_num))
                {
                    bool exists = await _context.Categories
                        .AnyAsync(x => x.categ_num == category.categ_num);

                    if (exists)
                    {
                        ModelState.AddModelError(
                            nameof(category.categ_num),
                            "Category Number already exists."
                        );
                    }
                }

                if (!ModelState.IsValid)
                {
                    return View(category);
                }

                _context.Categories.Add(category);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(
                    "CREATE ERROR:\n\n" +
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
            catch (Exception ex)
            {
                return Content(
                    "EDIT GET ERROR:\n\n" +
                    ex.ToString()
                );
            }
        }


        // =========================================================
        // EDIT - POST
        // =========================================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            string oldId,
            Category category)
        {
            try
            {
                // -------------------------------------------------
                // 1. التأكد من الرقم القديم
                // -------------------------------------------------
                if (string.IsNullOrWhiteSpace(oldId))
                {
                    return Content(
                        "EDIT ERROR: oldId is empty."
                    );
                }


                // -------------------------------------------------
                // 2. التأكد من categ_num الجديد
                // -------------------------------------------------
                if (string.IsNullOrWhiteSpace(category.categ_num))
                {
                    ModelState.AddModelError(
                        nameof(category.categ_num),
                        "Category Number is required."
                    );

                    return View(category);
                }


                // -------------------------------------------------
                // 3. التأكد أن السجل المطلوب موجود
                // -------------------------------------------------
                var existingCategory = await _context.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.categ_num == oldId);

                if (existingCategory == null)
                {
                    return Content(
                        "EDIT ERROR: Record not found.\n" +
                        "oldId = " + oldId
                    );
                }


                // -------------------------------------------------
                // 4. منع تكرار categ_num
                // -------------------------------------------------
                bool duplicate = await _context.Categories
                    .AsNoTracking()
                    .AnyAsync(x =>
                        x.categ_num == category.categ_num &&
                        x.categ_num != oldId
                    );

                if (duplicate)
                {
                    ModelState.AddModelError(
                        nameof(category.categ_num),
                        "Category Number already exists."
                    );

                    return View(category);
                }


                // -------------------------------------------------
                // 5. تحديث الصف نفسه في dbo.categories
                //
                // oldId = رقم الصف الموجود حاليًا
                // category.categ_num = الرقم الجديد
                //
                // لا نعمل Add
                // لا نعمل Delete
                // لا نغيّر مفتاح كيان EF المتتبع
                // -------------------------------------------------
                int affectedRows =
                    await _context.Database.ExecuteSqlInterpolatedAsync($@"
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
                        WHERE categ_num = {oldId}
                    ");


                // -------------------------------------------------
                // 6. التأكد أن UPDATE عدّل صفًا واحدًا
                // -------------------------------------------------
                if (affectedRows == 0)
                {
                    return Content(
                        "EDIT ERROR: UPDATE affected 0 rows.\n" +
                        "oldId = " + oldId
                    );
                }


                // -------------------------------------------------
                // 7. نجاح التعديل
                // -------------------------------------------------
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // -------------------------------------------------
                // إظهار الخطأ الحقيقي بدل HTTP 500 الغامض
                // -------------------------------------------------
                return Content(
                    "EDIT POST ERROR:\n\n" +
                    ex.ToString()
                );
            }
        }


        // =========================================================
        // DELETE - GET
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            try
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
            catch (Exception ex)
            {
                return Content(
                    "DELETE GET ERROR:\n\n" +
                    ex.ToString()
                );
            }
        }


        // =========================================================
        // DELETE - POST
        // =========================================================
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id))
                {
                    return NotFound();
                }

                var category = await _context.Categories
                    .FirstOrDefaultAsync(x => x.categ_num == id);

                if (category == null)
                {
                    return NotFound();
                }

                _context.Categories.Remove(category);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(
                    "DELETE ERROR:\n\n" +
                    ex.ToString()
                );
            }
        }
    }
}