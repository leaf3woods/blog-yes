using BlogYes.Domain.Entities;

namespace BlogYes.Application.Captchas.Builder
{
    public abstract class CaptchaBuilder
    {
        protected bool GenLines { get; set; } = false;
        protected bool Nosie { get; set; } = false;
        protected CaptchaGenOptions? CaptchaGenOptions { get; set; }

        public abstract CaptchaBuilder WithNoise();

        public abstract CaptchaBuilder WithLines();

        public abstract CaptchaBuilder WithGenOption(CaptchaGenOptions options);

        public static TBuilder Create<TBuilder>() where TBuilder : CaptchaBuilder, new()
        {
            return new TBuilder();
        }

        public abstract Captcha Build();
    }
}
