using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using webdd.Models;
using static System.Formats.Asn1.AsnWriter;

namespace webdd.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public ProductController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }



       


        public async Task<IActionResult> ProductBikeQuery()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:8080/api/Product/ProductAll");

            var client = _clientFactory.CreateClient();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.SendAsync(request);


            if (response.IsSuccessStatusCode)
            {

                //var responseContent = await response.Content.ReadAsStringAsync();
                //Console.WriteLine(responseContent);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    //var products = await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<ProductModel>>(responseStream);
                    var products = await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<ProductModel>>(responseStream, options);

                    //var menus = await System.Text.Json.JsonSerializer.DeserializeAsync<IEnumerable<Menu>>(responseStream);
                    var mm = new MenuValue();
                    var menus = mm.Children;

                    //ViewBag.Menus = menus;

                    var viewModel = new ProductQueryViewModel
                    {
                        Products = products,
                        Menus = menus, // 如果您有菜單的數據，也應該在這裡設置
                    };

                    return View(viewModel); // 將包含產品列表的ViewModel傳遞給視圖
                }
            }
            else
            {
                return NotFound();
            }
        }


        // 這個動作用於顯示單個產品的詳情
        public async Task<IActionResult> ProductBikeDetail(int id)
        {
            //var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:8080/api/Product/ProductInfoByID/{id}");
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:8080/api/Product/ProductInfoByID/{id}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseContent);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var productDetail = await System.Text.Json.JsonSerializer.DeserializeAsync<ProductDetailModel>(responseStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });


                    if (productDetail != null)
                    {
                        return View(productDetail);
                    }
                }
            }

            return NotFound(); // 如果找不到產品或請求失敗，返回NotFound結果
        }



        public IActionResult Index()
        {
            var menus = new List<Menu>()
            {
                new Menu()
                {
                    MName = "關於我",
                    Children = new List<Menu>()
                    {
                        new Menu()
                        {
                            MName = "簡介",
                            Action = "About",
                            Controller = "Home"
                        },
                        new Menu()
                        {
                            MName = "北區特色",
                            Action = "About",
                            Controller = "Home"
                        },
                        new Menu()
                        {
                            MName = "南區特色",
                            Action = "About",
                            Controller = "Home"
                        }
                    }
                },
                new Menu()
                {
                    MName = "各單位介紹",
                    Children = new List<Menu>()
                    {
                        new Menu()
                        {
                            MName = "行政中心",
                            Action = "About",
                            Controller = "Home"
                        },
                        new Menu()
                        {
                            MName = "資訊中心",
                            Action = "About",
                            Controller = "Home"
                        },
                        new Menu()
                        {
                            MName = "業務單位",
                            Action = "About",
                            Controller = "Home"
                        },
                        new Menu()
                        {
                            MName = "產品研發",
                            Action = "About",
                            Controller = "Home"
                        }
                    }
                },
                new Menu()
                {
                    MName = "產品項目",
                    Children = new List<Menu>()
                    {
                        new Menu()
                        {
                            MName = "周邊",
                            Children = new List<Menu>()
                            {
                                new Menu()
                                {
                                    MName = "滑鼠",
                                    Action = "About",
                                    Controller = "Home"
                                },
                                new Menu()
                                {
                                    MName = "鍵盤",
                                    Action = "About",
                                    Controller = "Home"
                                }
                            }
                        },
                        new Menu()
                        {
                            MName = "電池",
                            Action = "ProductPower",
                            Controller = "Home"
                        },
                        new Menu()
                        {
                            MName = "BIKE",
                            Action = "ProductBikeQuery",
                            Controller = "Product"
                        }
                    }
                },
                new Menu()
                    {
                        MName = "徵才",
                        Action = "About",
                        Controller = "Home"
                    },
                    new Menu()
                    {
                        MName = "新聞活動",
                        Action = "About",
                        Controller = "Home"
                    }
                };

            return View(menus);

        }
    }
}
