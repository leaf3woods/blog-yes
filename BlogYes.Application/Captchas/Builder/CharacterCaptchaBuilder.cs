
namespace BlogYes.Application.Captchas.Builder
{
    public class CharacterCaptchaBuilder : CaptchaBuilder
    {
        private IEnumerable<int> chars = CaptchaUtil.NumChars;
        public int Length { get; private set; } = 4;

        public override Captcha Build()
        {
            if (CaptchaGenOptions is null) throw new ArgumentNullException("captcha generate options was not set");
            var captcha = new Captcha()
            {
                Type = CaptchaType.Character,
                Image = CaptchaUtil.GenerateImage(CaptchaGenOptions, chars.GenCharacterText(Length), Nosie, GenLines),
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
        public CharacterCaptchaBuilder WithLowerCase()
        {
            chars = chars.Concat(CaptchaUtil.LowerChars);
            return this;
        }

        public CharacterCaptchaBuilder WithUpperCase()
        {
            chars = chars.Concat(CaptchaUtil.UpperChars);
            return this;
        }
    }
}
