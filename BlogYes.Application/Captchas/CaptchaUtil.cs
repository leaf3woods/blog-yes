using SkiaSharp;
using System.Drawing;
using System.Reflection;

namespace BlogYes.Application.Captchas
{
    public static class CaptchaUtil
    {
        private const int _angleRange = 45;

        private static (int,int) _lineCountRange = new (3, 6);

        private const int _lineWidth = 2;

        private const int _quality = 100;

        private const double _linePositionLimit = 0.6;

        private const float _noiseSize = (float)1;

        private const int _blank = 10;

        private static (int, int) _circleRadiusRange = new(6, 12);

        private static (int, int) _circleCountRange = new(4, 8);

        public static byte[] GenerateImage(CaptchaGenOptions options, char[] text,
            bool noise = false, bool line = false, bool circle = false)
        {
            var random = new Random();
            if (options is null) throw new Exception("no options was set");
            using var image2d = new SKBitmap(options.Width, options.Height, SKColorType.Bgra8888, SKAlphaType.Premul);
            using var canvas = new SKCanvas(image2d);
            
            var background = options.Background ?? SKColors.WhiteSmoke;
            canvas.Clear(background);
            
            var fontSize = (options.Width - 2 * _blank) / text.Length;
            using var drawStyle = new SKPaint { IsAntialias = true, TextSize = fontSize };

            if (circle)
            {
                var count = random.Next(_circleCountRange.Item1, _circleCountRange.Item2);
                drawStyle.Style = SKPaintStyle.Stroke;
                drawStyle.StrokeWidth = 2;
                for (int i = 0; i < count; i++)
                {
                    drawStyle.Color = Colors[random.Next(0, Colors.Length - 1)];
                    canvas.DrawCircle(random.Next(options.Width), random.Next(options.Height),
                        random.Next(_circleRadiusRange.Item1, _circleRadiusRange.Item2), drawStyle);
                }
                drawStyle.Style = SKPaintStyle.Fill;
                drawStyle.StrokeWidth = 1;
            }

            using var font = SKTypeface.FromFamilyName(options.FontFamily, SKFontStyleWeight.SemiBold, SKFontStyleWidth.ExtraCondensed, SKFontStyleSlant.Upright);
            float py = (options.Height) / 2;
            drawStyle.Typeface = font;
            var offset = fontSize / (float)1.5;
            for (int i = 0; i < text.Length; i++)
            {
                float px = i * fontSize + _blank;
                float angle = random.Next(-_angleRange, _angleRange);
                canvas.Translate(offset, offset);
                canvas.RotateDegrees(angle, px, py);
                drawStyle.Color = Colors[random.Next(0, Colors.Length - 1)];
                canvas.DrawText(text[i].ToString(), px, py, drawStyle);
                canvas.RotateDegrees(-angle, px, py);
                canvas.Translate(-offset, -offset);
            }

            if(noise)
            {
                for (int i = 0; i < options.Width * 2; i++)
                {
                    drawStyle.Color = Colors[random.Next(0, Colors.Length - 1)];
                    canvas.DrawRect(random.Next(options.Width), random.Next(options.Height), _noiseSize, _noiseSize, drawStyle);
                }
            }
            
            if (line)
            {
                var lineCount = random.Next(_lineCountRange.Item1, _lineCountRange.Item2);
                var hs = (int)(options.Height * (1 - _linePositionLimit)) / 2;
                var he = options.Height - hs;
                for (int i = 0; i < lineCount; i++)
                {
                    drawStyle.Color = Colors[random.Next(0, Colors.Length - 1)];
                    drawStyle.StrokeWidth = _lineWidth;
                    canvas.DrawLine(random.Next(0, options.Width), random.Next(hs, he),
                        random.Next(0, options.Width), random.Next(hs, he), drawStyle);
                }
            }
            
            using var img = SKImage.FromBitmap(image2d);
            using var data = img.Encode(SKEncodedImageFormat.Png, _quality);
            return data.ToArray();
        }

        public static char[] GenCharacterText(this IEnumerable<int> chars, int length)
        {
            var random = new Random();
            var result = new char[length];
            var array = chars.ToArray();
            for (int i = 0; i < length; i++)
            {
                var index = random.Next(0, array.Length - 1);
                result[i] = (char)array[index];
            }
            return result;
        }

        public static char[] GenEquation(out int result)
        {
            var random = new Random();
            var first = random.Next(0, 9);
            var next = random.Next(0, 9);
            var @operator = (Operator)random.Next(0, 3);
            string? chars;
            switch (@operator)
            {
                case Operator.Add:
                    chars = $"{first}+{next}=?";
                    result = (first + next);
                break;
                case Operator.Subtract:
                    chars = $"{first}-{next}=?";
                    result = (first - next);
                    break;
                case Operator.Multiply:
                    chars = $"{first}*{next}=?";
                    result = (first * next);
                    break;
                default:
                    throw new ArgumentException("unknow operator");
            }
            return chars.ToCharArray();
        }

        public static readonly SKColor[] Colors = typeof(SKColors)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => (SKColor)f.GetValue(null)!)
            .Where(f => f.Alpha != 0 && ((0.299 * f.Red + 0.587 * f.Green + 0.114 * f.Blue) / 255.0) >= 0.5)
            .ToArray();

        public static readonly IEnumerable<int> NumChars = Enumerable.Range(48, 10);
        public static readonly IEnumerable<int> LowerChars = Enumerable.Range(65, 26);
        public static readonly IEnumerable<int> UpperChars = Enumerable.Range(97, 26);


    }

    public enum Operator
    {
        Add, Subtract, Multiply
    }
}
