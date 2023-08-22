using SkiaSharp;
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

        public static byte[] GenerateImage(CaptchaGenOptions options, char[] text, bool noise = false, bool line = false)
        {
            var random = new Random();
            if (options is null) throw new Exception("no options was set");
            using var image2d = new SKBitmap(options.Width, options.Height, SKColorType.Bgra8888, SKAlphaType.Premul);
            using var canvas = new SKCanvas(image2d);
            
            var background = options.Background ?? SKColors.WhiteSmoke;
            canvas.Clear(background);
            
            var fontSize = (options.Width - 2 * _blank) / text.Length;
            using var drawStyle = new SKPaint { IsAntialias = true, TextSize = fontSize };
            for (int i = 0; i < text.Length; i++)
            {
                var font = SKTypeface.FromFamilyName(options.FontFamily, SKFontStyleWeight.SemiBold, SKFontStyleWidth.ExtraCondensed, SKFontStyleSlant.Upright);
                float angle = random.Next(-_angleRange, _angleRange);
                float px = i * fontSize + _blank;
                float py = (options.Height) / 2;
                
                canvas.Translate(12, 12);
                canvas.RotateDegrees(angle, px, py);
                drawStyle.Typeface = font;
                drawStyle.Color = Colors[random.Next(0, Colors.Length - 1)];
                canvas.DrawText(text[i].ToString(), px, py, drawStyle);
                
                canvas.RotateDegrees(-angle, px, py);
                canvas.Translate(-12, -12);
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
                    chars = $"{first}x{next}=?";
                    result = (first * next);
                    break;
                default:
                    throw new ArgumentException("unknow operator");
            }
            return chars.ToCharArray();
        }

        public static readonly SKColor[] Colors = typeof(SKColors)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => !f.Name.Contains("White") && !f.Name.Contains("Light") && 
                        f.Name != "Empty" && f.Name != "Transparent")
            .Select(f => (SKColor)f.GetValue(null)!)
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
