
using SkiaSharp;

namespace BlogYes.Application.Captchas
{
    public class CapchaGenOptions
    {
        public int FontSize { get; set; }
        public string FontFamily { get; set; } = string.Empty;
        public SKColor? Background { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
