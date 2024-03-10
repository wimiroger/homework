namespace webdd.Models
{
    public class Menu
    {
  
        public string Action { get; set; }



        public string Controller { get; set; }

        public string MName { get; set; }

        public List<Menu> Children { get; set; } = new List<Menu>();

        public string Title { get; set; }
        public string Date { get; set; }


    }

    public static class MenuExtensions
    {
        /// <summary>
        /// 檢查選單項目是否有子選單。
        /// </summary>
        /// <param name="menus">選單項目列表。</param>
        /// <returns>如果選單項目有子選單，則返回 true；否則返回 false。</returns>
        public static bool Any(this List<Menu> menus)
        {
            return menus != null && menus.Count > 0;
        }
    }

 


}
