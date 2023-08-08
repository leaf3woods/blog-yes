namespace BlogYes.Domain.ValueObjects.Blog
{
    public class Category
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedTime { get; set; }
    }
}