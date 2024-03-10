using System.Collections.Generic;
using System.Data.Entity;
using webdd.Models;

namespace webdd.Controllers
{
    

    public class DDFDbContext : DbContext
    {
        public DDFDbContext() : base("name=YourConnectionStringName")
        {
            // 如果需要，這裡可以設置一些 DbContext 的配置，比如延遲加載、初始化策略等。
        }

        public DbSet<ProductModel> Products { get; set; }

        public static implicit operator DDFDbContext(ProductController v)
        {
            throw new NotImplementedException();
        }
        // 其他 DbSet 屬性來代表資料庫中的其他表
    }
}
