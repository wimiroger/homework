using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using webdd.Models;

using CsvHelper;
using System.IO;
using CsvHelper.Configuration;
using System.Text;

namespace webdd.Controllers
{
    public class GovData_CashAdvanceController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public GovData_CashAdvanceController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        //public async Task<IActionResult> Index()
        //{
        //    var client = _clientFactory.CreateClient();
        //    var jsonResult = await client.GetStringAsync("https://data.gov.tw/api/v2/rest/dataset/11698");
        //    var jsonObject = JObject.Parse(jsonResult);
        //    var csvUrl = "https://www.nccc.com.tw/dataDownload/04_ATM_Cash_Advance_Transaction_in_NCCNET.csv"; //jsonObject["result"]["distribution"]["resourceDownloadUrl"].ToString();

        //    var csvData = await client.GetStringAsync(csvUrl);

        //    var csvRecords = ParseCsvData(csvData);

        //    return View(csvRecords);
        //}


        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var jsonResult = await client.GetStringAsync("https://data.gov.tw/api/v2/rest/dataset/11698");
            var jsonObject = JObject.Parse(jsonResult);
            var csvUrl = (string)jsonObject["result"]["distribution"][0]["resourceDownloadUrl"];
            var txtCode = (string)jsonObject["result"]["distribution"][0]["resourceCharacterEncoding"];

            var response = await client.GetStreamAsync(csvUrl);
           

            // 這裡不再會拋出異常，因為BIG5編碼現在已經被註冊了
            using (var reader = new StreamReader(response, Encoding.GetEncoding(txtCode)))
            {
                var csvData = reader.ReadToEnd();
                var records = ParseCsvData(csvData, txtCode);
                return View(records);
            }
        }


        private List<CashAdvanceData> ParseCsvData(string csvData,string txtCode)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Encoding = Encoding.GetEncoding("BIG5"), // 指定 CSV 讀取時使用 BIG5 編碼
                                                         // 其他配置...
            };

            using (var reader = new StringReader(csvData))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<CashAdvanceDataMap>();
                var records = csv.GetRecords<CashAdvanceData>().ToList();
                return records;
            }
        }


    }

    //public class CashAdvanceData
    //{
    //    public string Year { get; set; }
    //    public string Amount { get; set; }
    //    public string Transactions { get; set; }
    //}

    public class CashAdvanceDataMap : ClassMap<CashAdvanceData>
    {
        public CashAdvanceDataMap()
        {
            Map(m => m.Year).Name("西元年");
            Map(m => m.Amount).Name("信用卡預借現金金額[新臺幣，億元]");
            Map(m => m.Transactions).Name("信用卡預借現金筆數[千筆]");
        }
    }
}
