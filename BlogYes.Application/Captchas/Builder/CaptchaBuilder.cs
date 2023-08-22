namespace BlogYes.Application.Captchas.Builder
{
    public abstract class CaptchaBuilder
    {
        protected bool GenLines { get; set; } = false;
        protected int Nosie { get; set; } = 0;
        protected CapchaGenOptions? CapchaGenOptions { get; set; }

        public abstract CaptchaBuilder WithNoise(int count);

        public abstract CaptchaBuilder WithLines();

        public abstract CaptchaBuilder WithGenOption(CapchaGenOptions options);

        public static CaptchaBuilder CreateType(CaptchaType type)
        {
            CaptchaBuilder builder = type switch
            {
                CaptchaType.Question => new QuestionCaptchaBuilder(),
                CaptchaType.Han => new HanCaptchaBuilder(),
                CaptchaType.Character => new CharacterCaptchaBuilder(),
                _ => throw new ArgumentException("unknow captcha type")
            };
            return builder;
        }

        public abstract Captcha Build();
    }

    public enum CaptchaType
    {
        Question,
        Han,
        Character
    }
}
