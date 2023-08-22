
using BlogYes.Application.Captchas.Builder;

namespace BlogYes.Application.Captchas
{
    public class Captcha
    {
        public CaptchaType Type { get; set; }

        public (int, int) Pixel { get; set; }

        public byte[]? Image { get; set; }

        public override string ToString()
        {
            var base64 = Image is null ?
                string.Empty :
                Convert.ToBase64String(Image);
            return "data:image/png;base64," + base64;
        }
    }
}
