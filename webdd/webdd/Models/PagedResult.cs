using Microsoft.EntityFrameworkCore;

namespace webdd.Models
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public PagedResult(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
            TotalCount = count;
        }

        public static PagedResult<T> Create(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PagedResult<T>(items, count, pageIndex, pageSize);
        }

        internal static Task<string?> CreateAsync(object value, int v, int pageSize)
        {
            throw new NotImplementedException();
        }

        public static async Task<PagedResult<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync(); // 使用 Entity Framework Core 來計算總數量
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(); // 獲取當前頁面的項目
            return new PagedResult<T>(items, count, pageIndex, pageSize); // 創建分頁結果
        }
    }
}
