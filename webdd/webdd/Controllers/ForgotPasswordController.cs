using Microsoft.AspNetCore.Mvc;
using webdd.Models;

namespace webdd.Controllers
{
    public class ForgotPasswordController : Controller
    {
        // GET: Registration
        public IActionResult ForgetPassword()
        {
            return View();
        }

        // POST: Registration
        [HttpPost]
        [ValidateAntiForgeryToken] // 防止CSRF攻擊
        public IActionResult ForgetPassword(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // 在這裡處理您的註冊邏輯，例如保存用戶信息到資料庫
                // 如果註冊成功，重定向到登入頁面或首頁
                return RedirectToAction("ForgetPassword", "ForgetPassword");
            }

            // 如果模型狀態無效，重新顯示表單
            return View(model);
        }
    }
}
