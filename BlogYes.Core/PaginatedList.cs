
namespace BlogYes.Core
{
    public class PaginatedList<T> where T : new()
    {
        public PaginatedList(IEnumerable<T> content, long itemCount, long pageIndex, int pageSize)
        {
            Content = content;
            ItemCount = itemCount; PageIndex = pageIndex; PageSize = pageSize;
            PageCount = (int)Math.Ceiling(itemCount / (double)pageSize);
        }
        public long ItemCount { get; init; }
        public long PageIndex { get; init; }
        public int PageSize { get; init; }
        public long PageCount { get; init; }
        public IEnumerable<T> Content { get; init; }
    }
}
