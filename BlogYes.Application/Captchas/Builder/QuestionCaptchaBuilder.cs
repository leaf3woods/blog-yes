
namespace BlogYes.Application.Captchas.Builder
{
    public class QuestionCaptchaBuilder : CaptchaBuilder
    {
        public override Captcha Build()
        {
            if (CapchaGenOptions is null) throw new ArgumentNullException("CapchaGenOptions was not set");
            var captcha = new Captcha()
            {
                Type = CaptchaType.Question,
                Pixel = new (CapchaGenOptions.Width, CapchaGenOptions.Height)
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
