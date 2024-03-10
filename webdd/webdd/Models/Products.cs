using System.ComponentModel.DataAnnotations;

namespace webdd.Models
{
    public class Products
    {
        public int Id { get; set; } 

        [Required(ErrorMessage = "產品是必填的")] // 使欄位成為必填
        [StringLength(30, ErrorMessage = "產品不能超過30個字")] // 限制字符串長度
        public string ProductName { get; set; }

        [Required(ErrorMessage = "品牌是必填的")] // 使欄位成為必填
        [StringLength(30, ErrorMessage = "品牌不能超過30個字")] // 限制字符串長度
        public string Brand { get; set; }

        [Required(ErrorMessage = "狀態是必填的")]
        public string Status { get; set; }

        // 這裡沒有對Category使用[Required]，假設它不是必填項目
        [StringLength(20, ErrorMessage = "類別不能超過20個字")]
        public string Category { get; set; }

        [Range(0, 880000, ErrorMessage = "價格必須在0到880000之間")] // 限制範圍
        public int Price { get; set; }


        [StringLength(1000, ErrorMessage = "圖片URL的長度不能超過1000個字符")]
        public string ImageUrl { get; set; } // 增加ImageUrl欄位


        public DateTime? CreateAt { get; set; }  // 增加CreateAt欄位，使用可空的DateTime型別

        public Products()
        {
            // 在構造函數中設置預設值
            CreateAt = DateTime.Now;
        }
    }

}
