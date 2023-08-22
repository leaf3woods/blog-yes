
namespace BlogYes.Application.Captchas.Builder
{
    public class CharacterCaptchaBuilder : CaptchaBuilder
    {
        public int Length { get; private set; } = 4;
        public bool Lowercase { get; private set; } = false;
        public override Captcha Build()
        {
            if (CapchaGenOptions is null) throw new ArgumentNullException("CapchaGenOptions was not set");
            var captcha = new Captcha()
            {
                Type = CaptchaType.Question,
                Image = CaptchaUtil.GetImage(CapchaGenOptions, CaptchaUtil.GenCapchaText(Length)),
                Pixel = new(CapchaGenOptions.Width, CapchaGenOptions.Height)
            };
            return captcha;
        }

        public override CaptchaBuilder WithGenOption(CapchaGenOptions options)
        {
            CapchaGenOptions = options;
            return this;
        }

        public override CaptchaBuilder WithLines()
        {
            GenLines = true;
            return this;
        }

        public override CaptchaBuilder WithNoise(int count)
        {
            Nosie = count;
            return this;
        }
    }
}
