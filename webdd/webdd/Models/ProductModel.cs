using System.Text.Json.Serialization;

namespace webdd.Models
{
    public class ProductModel
    {
        //public int Id { get; set; }
        //public string ProductName { get; set; }       
        //public string Status { get; set; } // 例如：上架、下架

        //public List<ProductDetailModel> Productdetail { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("productName")]
        public string ProductName { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; } // 例如：上架、下架

    }
}
