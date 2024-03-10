namespace webdd.Models
{
    public class ProductDetailModel
    {
        //public string Category { get; set; }
        //public decimal Price { get; set; }
        //public string Brand { get; set; }
        //public string Status { get; set; } // 例如：上架、下架
        //public string ImagePath { get; set; }
        //public string Description { get; set; }
        //public string Specification { get; set; }
        //public string Reviews { get; set; }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Status { get; set; } // 例如：上架、下架
        public string ImagePath { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public string Reviews { get; set; }
    }
}
