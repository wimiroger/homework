namespace webdd.Models
{
    public class ProductQueryViewModel
    {
        public IEnumerable<ProductModel> Products { get; set; }
        public IEnumerable<Menu> Menus { get; set; }
    }
}
