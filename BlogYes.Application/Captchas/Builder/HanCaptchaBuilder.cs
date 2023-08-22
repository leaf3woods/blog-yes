﻿
namespace BlogYes.Application.Captchas.Builder
{
    public class HanCaptchaBuilder : CaptchaBuilder
    {
        public int Length { get; set; }

        public override Captcha Build()
        {
            var captcha = new Captcha()
            {
                Type = CaptchaType.Han,
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
