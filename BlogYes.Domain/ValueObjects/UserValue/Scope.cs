using BlogYes.Domain.Entities;

namespace BlogYes.Domain.ValueObjects.UserValue
{
    public class Scope
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        #region navigation

        public virtual Role Role { get; set; } = null!;

        #endregion navigation

        public static Scope FromString(string scopeName) => new Scope { Name = scopeName };

        public static Scope[] InitItems => new Scope[]
        {
            new Scope { Name = "default", Description = "default" },
        };
    }

    public enum ScopeType
    {
        Activated,
        Supported
    }
}