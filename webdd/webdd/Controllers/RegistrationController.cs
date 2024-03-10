using Microsoft.AspNetCore.Mvc;
using webdd.Models;

namespace webdd.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public IActionResult Index()
        {
            return View();
        }

        // POST: Registration
        [HttpPost]
        [ValidateAntiForgeryToken] // 防止CSRF攻擊
        public IActionResult Index(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 在這裡處理您的註冊邏輯，例如保存用戶信息到資料庫
                // 如果註冊成功，重定向到登入頁面或首頁
                return RedirectToAction("Index", "Home");
            }

            // 如果模型狀態無效，重新顯示表單
            return View(model);
        }
    }
}
