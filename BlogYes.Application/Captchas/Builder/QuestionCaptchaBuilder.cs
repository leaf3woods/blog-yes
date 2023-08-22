
namespace BlogYes.Application.Captchas.Builder
{
    public class QuestionCaptchaBuilder : CaptchaBuilder
    {
        public override Captcha Build()
        {
            if (CaptchaGenOptions is null) throw new ArgumentNullException("CapchaGenOptions was not set");
            var equation = CaptchaUtil.GenEquation(out var tuple);
            var captcha = new Captcha()
            {
                Type = CaptchaType.Question,
                Image = CaptchaUtil.GenerateImage(CaptchaGenOptions, equation, Nosie, GenLines),
                Pixel = new (CaptchaGenOptions.Width, CaptchaGenOptions.Height)
            };
            return captcha;
        }

        public override CaptchaBuilder WithGenOption(CaptchaGenOptions options)
        {
            CaptchaGenOptions = options;
            return this;
        }

        public override CaptchaBuilder WithLines()
        {
            GenLines = true;
            return this;
        }

        public override CaptchaBuilder WithNoise()
        {
            Nosie = true;
            return this;
        }
    }
}
