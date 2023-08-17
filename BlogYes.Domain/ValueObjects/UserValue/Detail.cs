using BlogYes.Domain.Entities;

namespace BlogYes.Domain.ValueObjects.UserValue
{
    public class Detail
    {
        public Gender Gender { get; set; } = Gender.Unknow;
        public string? AboutMe { get; set; }

        #region navigation

        public User User { get; set; } = null!;

        #endregion navigation
    }

    public enum Gender
    {
        Male,
        Female,
        Unknow
    }
}