using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

namespace Assignment_3
{
    public static class CAPTCHAHelper
    {
        public static Tuple<string, byte[]> GenerateCaptchaImage()
        {
            string captchaText = GenerateRandomText();
            byte[] captchaBytes = GenerateImage(captchaText);
            return Tuple.Create(captchaText, captchaBytes);
        }

        private static string GenerateRandomText()
        {
            Random rand = new Random();
            const string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int length = 6; // Set the desired length of the CAPTCHA text

            char[] captchaChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                captchaChars[i] = characters[rand.Next(0, characters.Length)];
            }

            return new string(captchaChars);
        }

        private static byte[] GenerateImage(string text)
        {
            using (Bitmap bitmap = new Bitmap(180, 60))
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.AntiAlias;
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                graphics.Clear(Color.White);

                using (Font font = new Font("Arial", 24))
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    graphics.DrawString(text, font, brush, 10, 10);
                }

                using (MemoryStream stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Png);
                    return stream.ToArray();
                }
            }
        }
    }
}