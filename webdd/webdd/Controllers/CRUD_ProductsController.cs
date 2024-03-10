using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using webdd.Models;
using Microsoft.EntityFrameworkCore;

namespace webdd.Controllers
{
    public class CRUD_ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CRUD_ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 10; // 可以設定每頁顯示的數據條數
            var products = _context.Products; // 這是你的數據集

            // 這裡使用了前面定義的 PagedResult.Create 方法來生成分頁結果
            var pagedResult = await PagedResult<Products>.CreateAsync(
            _context.Products.AsNoTracking(), pageNumber ?? 1, pageSize);

            return View(pagedResult);
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Brand,Status,Category,Price,ImageUrl")] Products product)
        {
            if (ModelState.IsValid)
            {
                product.CreateAt = DateTime.Now; // 在這裡設定當前時間
                _context.Add(product);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            else
            {
                var errors = ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .Select(e => new { e.Key, e.Value.Errors.First().ErrorMessage })
                    .ToArray();

                // Log errors or set a breakpoint here to inspect them
            }
            // 如果無效，返回錯誤訊息或模型錯誤
            return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }



        //// POST: Products/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,ProductName,Brand,Status,Category,Price,ImageUrl")] Products product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(product);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    else
        //    {
        //        var errors = ModelState
        //            .Where(e => e.Value.Errors.Count > 0)
        //            .Select(e => new { e.Key, e.Value.Errors.First().ErrorMessage })
        //            .ToArray();

        //        // Log errors or set a breakpoint here to inspect them
        //    }

        //    return View(product);
        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,ProductName,Brand,Status,Category,Price")] Products product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        product.CreateAt = DateTime.Now; // 在這裡設定當前時間
        //        _context.Add(product);
        //        await _context.SaveChangesAsync();
        //        return Json(new { success = true });
        //    }
        //    else
        //    {
        //        var errors = ModelState
        //            .Where(e => e.Value.Errors.Count > 0)
        //            .Select(e => new { e.Key, e.Value.Errors.First().ErrorMessage })
        //            .ToArray();

        //        // Log errors or set a breakpoint here to inspect them
        //    }
        //    // 如果無效，返回錯誤訊息或模型錯誤
        //    return Json(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        //}



        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Status,Category,Price")] Products product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
