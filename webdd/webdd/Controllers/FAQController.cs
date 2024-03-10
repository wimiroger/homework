using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using webdd.Models;

namespace webdd.Controllers
{
    public class FAQController : Controller
    {

        private readonly IHttpClientFactory _clientFactory;

        public FAQController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async Task<IActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:8080/FAQAll");

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

                    //var faqs = await System.Text.Json.JsonSerializer.DeserializeAsync<FAQModel>(responseStream, options);

                    //var model = new List<FAQModel>
                    //{
                    //    new FAQModel
                    //    {
                    //        Id=faqs.Id,
                    //        Question =faqs.Question,
                    //        Answer = faqs.Answer,
                    //    },                        
                    //};

                    //return View(model); // 將包含產品列表的ViewModel傳遞給視圖

                    // 修改此行，將FAQModel改為FAQModel[]或List<FAQModel>
                    var faqs = await System.Text.Json.JsonSerializer.DeserializeAsync<List<FAQModel>>(responseStream, options);

                    //foreach (var faq in faqs)
                    //{
                    //    faq.Question = WebUtility.HtmlEncode(faq.Question);
                    //    faq.Answer = WebUtility.HtmlEncode(faq.Answer);
                    //}

                    // 由於faqs已經是List<FAQModel>，所以你可以直接將它傳遞給視圖，而不需要再建立新的List
                    return View(faqs); // 直接將faqs傳遞給視圖
                }
            }
            else
            {
                return NotFound();
            }
        }


       




        //public IActionResult Index()
        //{
        //    var model = new List<FAQModel>
        //    {
        //      new FAQModel
        //      {
        //        Question = "這個計畫是誰在執行?研究團隊有誰?",
        //        Answer = "這個計畫是由國立臺灣大學公共衛生學院兒童健康研究中心執行。研究團隊包括：\n\n* 林奏延教授 (計畫主持人)\n* 邱淑媞教授\n* 陳秀熙教授\n* 詹長權教授\n* 蔡爾輝教授\n* 許志成教授\n* 翁啟惠教授\n* 楊培珊教授\n* 鄭玉惠教授\n* 賴玉惠教授\n* 蘇益仁教授"
        //      },
        //      new FAQModel
        //      {
        //        Question = "為什麼要做這個研究計畫?計畫的目的是什麼呢?",
        //        Answer = "本研究計畫旨在追蹤臺灣幼兒從出生至八歲的健康發展狀況，以了解臺灣幼兒的健康發展趨勢，並探討影響幼兒健康發展的因素。研究結果可提供政府及民間單位作為制定幼兒健康政策及服務的參考。"
        //      },
        //      // ...
        //    };

        //    return View(model);
        //}



    }
}
