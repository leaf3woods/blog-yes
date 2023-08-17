using BlogYes.Domain.Entities;

namespace BlogYes.Domain.ValueObjects.BlogValue
{
    public class Tag
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        #region navigation
        public Blog Blog { get; set; } = null!;
        #endregion
    }
}