using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;
using webdd.Models;

using System.Drawing.Imaging;
using webdd.MyClass;
using Google.Apis.Auth;

namespace webdd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }
        
       

        // POST: Login
        [HttpPost]
        public IActionResult Login(string Username, string Password, string Captcha)
        {
            // 在這裡實現您的登入邏輯
            // 驗證用戶名和密碼，以及驗證碼的正確性
            // 這裡僅為示例，實際應用中應該更加嚴謹

            bool isValidUser = false; // 假設有一個方法來檢查用戶的有效性
            bool isCaptchaValid = false; // 假設有一個方法來驗證驗證碼

            if (isValidUser && isCaptchaValid)
            {
                // 登入成功，重定向到另一個頁面
                return RedirectToAction("Index");
            }
            else
            {
                // 登入失敗，顯示錯誤信息
                ViewBag.ErrorMessage = "登入信息有誤，請重新輸入";
                return View();
            }
        }

        public IActionResult GetCaptchaImage()
        {
            int width = 100;
            int height = 40;
            var cu = new CommonUse();
            var captchaCode = cu.GenerateCaptchaCode(); // 這裡應該是一個生成隨機數的邏輯
            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(bitmap);
            var font = new Font("Arial", 20, FontStyle.Bold);
            var brush = new SolidBrush(Color.Black);

            graphics.Clear(Color.White);
            graphics.DrawString(captchaCode, font, brush, 2, 2);

            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;

            bitmap.Dispose();
            graphics.Dispose();

            return File(stream, "image/jpeg");
        }


    


        public IActionResult Index()
        {
            var menus = new List<Menu>();
            //ViewBag.Menus = new List<Menu>()
            //{
            //    new Menu()
            //    {
            //        MName = "關於我",
            //        Children = new List<Menu>()
            //        {
            //            new Menu()
            //            {
            //                MName = "簡介",
            //                Action = "About",
            //                Controller = "Home"
            //            },
            //            new Menu()
            //            {
            //                MName = "北區特色",
            //                Action = "About",
            //                Controller = "Home"
            //            },
            //            new Menu()
            //            {
            //                MName = "南區特色",
            //                Action = "About",
            //                Controller = "Home"
            //            }
            //        }
            //    },
            //    new Menu()
            //    {
            //        MName = "各單位介紹",
            //        Children = new List<Menu>()
            //        {
            //            new Menu()
            //            {
            //                MName = "行政中心",
            //                Action = "About",
            //                Controller = "Home"
            //            },
            //            new Menu()
            //            {
            //                MName = "資訊中心",
            //                Action = "About",
            //                Controller = "Home"
            //            },
            //            new Menu()
            //            {
            //                MName = "業務單位",
            //                Action = "About",
            //                Controller = "Home"
            //            },
            //            new Menu()
            //            {
            //                MName = "產品研發",
            //                Action = "About",
            //                Controller = "Home"
            //            }
            //        }
            //    },
            //    new Menu()
            //    {
            //        MName = "產品項目",
            //        Children = new List<Menu>()
            //        {
            //            new Menu()
            //            {
            //                MName = "周邊",
            //                Children = new List<Menu>()
            //                {
            //                    new Menu()
            //                    {
            //                        MName = "滑鼠",
            //                        Action = "About",
            //                        Controller = "Home"
            //                    },
            //                    new Menu()
            //                    {
            //                        MName = "鍵盤",
            //                        Action = "About",
            //                        Controller = "Home"
            //                    }
            //                }
            //            },
            //            new Menu()
            //            {
            //                MName = "電池",
            //                Action = "ProductPower",
            //                Controller = "Home"
            //            },
            //            new Menu()
            //            {
            //                MName = "BIKE",
            //                Action = "ProductBikeQuery",
            //                Controller = "Product"
            //            }
            //        }
            //    },
            //    new Menu()
            //        {
            //            MName = "徵才",
            //            Action = "About",
            //            Controller = "Home"
            //        },
            //        new Menu()
            //        {
            //            MName = "新聞活動",
            //            Action = "About",
            //            Controller = "Home"
            //        }
            //    };

                return View(menus);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public IActionResult ValidGoogleLogin()
        {
            string? formCredential = Request.Form["credential"]; //回傳憑證
            string? formToken = Request.Form["g_csrf_token"]; //回傳令牌
            string? cookiesToken = Request.Cookies["g_csrf_token"]; //Cookie 令牌

            // 驗證 Google Token
            GoogleJsonWebSignature.Payload? payload = VerifyGoogleToken(formCredential, formToken, cookiesToken).Result;
            if (payload == null)
            {
                // 驗證失敗
                ViewData["Msg"] = "驗證 Google 授權失敗";
            }
            else
            {
                //驗證成功，取使用者資訊內容
                ViewData["Msg"] = "驗證 Google 授權成功" + "<br>";
                ViewData["Msg"] += "Email:" + payload.Email + "<br>";
                ViewData["Msg"] += "Name:" + payload.Name + "<br>";
                //ViewData["Msg"] += "Picture:" + payload.Picture;
                ViewData["Msg"] += "<img src='" + payload.Picture+ "' />";
            }

            return View();
        }

        /// <summary>
        /// 驗證 Google Token
        /// </summary>
        /// <param name="formCredential"></param>
        /// <param name="formToken"></param>
        /// <param name="cookiesToken"></param>
        /// <returns></returns>
        public async Task<GoogleJsonWebSignature.Payload?> VerifyGoogleToken(string? formCredential, string? formToken, string? cookiesToken)
        {
            // 檢查空值
            if (formCredential == null || formToken == null && cookiesToken == null)
            {
                return null;
            }

            GoogleJsonWebSignature.Payload? payload;
            try
            {
                // 驗證 token
                if (formToken != cookiesToken)
                {
                    return null;
                }

                // 驗證憑證
                IConfiguration Config = new ConfigurationBuilder().AddJsonFile("appSettings.json").Build();
                string GoogleApiClientId = Config.GetSection("GoogleApiClientId").Value;
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { GoogleApiClientId }
                };
                payload = await GoogleJsonWebSignature.ValidateAsync(formCredential, settings);
                if (!payload.Issuer.Equals("accounts.google.com") && !payload.Issuer.Equals("https://accounts.google.com"))
                {
                    return null;
                }
                if (payload.ExpirationTimeSeconds == null)
                {
                    return null;
                }
                else
                {
                    DateTime now = DateTime.Now.ToUniversalTime();
                    DateTime expiration = DateTimeOffset.FromUnixTimeSeconds((long)payload.ExpirationTimeSeconds).DateTime;
                    if (now > expiration)
                    {
                        return null;
                    }
                }
            }
            catch
            {
                return null;
            }
            return payload;
        }
    }
}