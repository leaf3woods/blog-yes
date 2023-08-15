
namespace BlogYes.Domain.ValueObjects.User
{
    public class Detail
    {
        public Gender Gender { get; set; } = Gender.Unknow;
        public string? AboutMe { get; set; }
    }
    public enum Gender
    {
        Male,
        Female,
        Unknow
    }
}
