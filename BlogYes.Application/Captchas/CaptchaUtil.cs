using SkiaSharp;

namespace BlogYes.Application.Captchas
{
    public static class CaptchaUtil
    {
        public static byte[] GetImage(CapchaGenOptions options, char[] text)
        {
            var random = new Random();
            if (options is null) throw new Exception("no options was set");
            using (var image2d = new SKBitmap(options.Width, options.Height, SKColorType.Bgra8888, SKAlphaType.Premul))
            {
                using (var canvas = new SKCanvas(image2d))
                {
                    //  background
                    var background = options.Background ?? SKColor.Empty;
                    canvas.Clear(background);

                    var drawStyle = new SKPaint
                    {
                        IsAntialias = true,
                        TextSize = options.FontSize
                    };
                    for (int i = 0; i < text.Length; i++)
                    {
                        var font = SKTypeface.FromFamilyName(options.FontFamily, SKFontStyleWeight.SemiBold, SKFontStyleWidth.ExtraCondensed, SKFontStyleSlant.Upright);
                        float angle = random.Next(-30, 30);

                        canvas.Translate(12, 12);

                        float px = ((i) * options.FontSize);
                        float py = (options.Height) / 2;

                        canvas.RotateDegrees(angle, px, py);

                        drawStyle.Typeface = font;
                        drawStyle.Color = Colors[random.Next(0, Colors.Length - 1)];

                        canvas.DrawText(text[i].ToString(), px, py, drawStyle);

                        canvas.RotateDegrees(-angle, px, py);
                        canvas.Translate(-12, -12);
                    }

                    var linecount = random.Next(0, 3);
                    using (var disturbStyle = new SKPaint())
                    {
                        for (int i = 0; i < linecount; i++)
                        {
                            disturbStyle.Color = Colors[random.Next(0, Colors.Length - 1)];
                            disturbStyle.StrokeWidth = 1;
                            canvas.DrawLine(random.Next(0, options.Width), random.Next(0, options.Height), random.Next(0, options.Width), random.Next(0, options.Height), disturbStyle);
                        }
                    }

                    using var img = SKImage.FromBitmap(image2d);
                    using var data = img.Encode(SKEncodedImageFormat.Png, 100);
                    return data.ToArray();
                }
            }
        }

        public static char[] GenCapchaText(int length)
        {
            var random = new Random();
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                var index = random.Next(0, Chars.Length - 1);
                result[i] = (char)Chars[index];
            }
            return result;
        }

        public static readonly SKColor[] Colors = new SKColor[]
        {
            SKColors.AliceBlue, SKColors.Purple, SKColors.Violet, SKColors.DimGray,
            SKColors.AntiqueWhite, SKColors.Green, SKColors.Orange, SKColors.Orchid,
            SKColors.Yellow, SKColors.Cyan, SKColors.White, SKColors.Chocolate,
            SKColors.HotPink, SKColors.Silver, SKColors.Cornsilk, SKColors.Chocolate
        };

        public static readonly int[] Chars = Enumerable.Range(48, 57).Concat(Enumerable.Range(65, 90)).Concat(Enumerable.Range(97, 122)).ToArray();
    }
}
