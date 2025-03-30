using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static Windows.APIs;

namespace Nemesis
{
    public class PayloadsGdi
    {
        static double HueToRGB(double v1, double v2, double vH)
        {
            if (vH < 0) vH += 1;
            if (vH > 1) vH -= 1;
            if ((6 * vH) < 1) return v1 + (v2 - v1) * 6 * vH;
            if ((2 * vH) < 1) return v2;
            if ((3 * vH) < 2) return v1 + (v2 - v1) * ((2.0 / 3) - vH) * 6;
            return v1;
        }
        static void HLS2RGB(double h, double l, double s, out byte r, out byte g, out byte b)
        {
            double v;
            double r1, g1, b1;

            if (s == 0)
            {
                r1 = g1 = b1 = l;
            }
            else
            {
                double m1, m2;

                if (l < 0.5)
                    m2 = l * (1 + s);
                else
                    m2 = (l + s) - (l * s);

                m1 = 2 * l - m2;

                r1 = HueToRGB(m1, m2, h + 1.0 / 3.0);
                g1 = HueToRGB(m1, m2, h);
                b1 = HueToRGB(m1, m2, h - 1.0 / 3.0);
            }

            r = (byte)(r1 * 255);
            g = (byte)(g1 * 255);
            b = (byte)(b1 * 255);
        }

        static int w = SCREEN_WIDTH;
        static int h = SCREEN_HEIGHT;
        public static void Gdi1()
        {
            var dc = GetDC(IntPtr.Zero);
            var dcCopy = CreateCompatibleDC(dc);

            BITMAPINFO bmpi = new BITMAPINFO();
            bmpi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmpi.bmiHeader);
            bmpi.bmiHeader.biWidth = w;
            bmpi.bmiHeader.biHeight = h;
            bmpi.bmiHeader.biPlanes = 1;
            bmpi.bmiHeader.biBitCount = 32;
            bmpi.bmiHeader.biCompression = BI_RGB;

            IntPtr bits;
            var hBitmap = CreateDIBSection(dc, ref bmpi, DIB_RGB_COLORS, out bits, IntPtr.Zero, 0);
            SelectObject(dcCopy, hBitmap);

            while (true)
            {
                StretchBlt(dcCopy, 0, 0, w, h, dc, 0, 0, w, h, SRCCOPY);

                unsafe
                {
                    RGBQUAD* rgbquad = (RGBQUAD*)bits;

                    for (int x = 0; x < w; x++)
                    {
                        for (int y = 0; y < h; y++)
                        {
                            int index = y * w + x;
                            double value = 0.5f + 0.5f * Math.Sin((x + Environment.TickCount * 0.005f) * 0.1f) +
                                          0.5f + 0.5f * Math.Sin((y + Environment.TickCount * 0.005f) * 0.1f);
                            byte color = (byte)(value * 255);
                            rgbquad[index].rgbRed = color;
                            rgbquad[index].rgbGreen = color;
                            rgbquad[index].rgbBlue = color;
                            rgbquad[index].rgbReserved = 100;
                        }
                    }
                }

                AlphaBlend(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, new BLENDFUNCTION(255));

                Thread.Sleep(10);
            }
        }

        public static void Gdi2()
        {
            uint color = 0xFFFFFF;  // White
            while (true)
            {
                var hdc = GetDC(IntPtr.Zero);
                var mhdc = CreateCompatibleDC(hdc);
                var hbit = CreateCompatibleBitmap(hdc, w, h);
                var holdbit = SelectObject(mhdc, hbit);
                BitBlt(mhdc, 0, 0, w, h, hdc, 0, 0, SRCCOPY);

                for (int i = 0; i < 100; i++)
                {
                    int px = rand.Next(w);
                    int py = rand.Next(h);
                    SetPixel(mhdc, px, py, color);

                    for (int j = -5; j <= 5; j++)
                    {
                        for (int k = -5; k <= 5; k++)
                        {
                            if (px + j >= 0 && px + j < w && py + k >= 0 && py + k < h)
                            {
                                SetPixel(mhdc, px + j, py + k, color);
                            }
                        }
                    }
                }

                AlphaBlend(hdc, rand.Next(-4, 4), rand.Next(-4, 4), w, h, mhdc, 0, 0, w, h, new BLENDFUNCTION(70));

                SelectObject(mhdc, holdbit);
                DeleteObject(hbit);
                DeleteObject(mhdc);
                ReleaseDC(IntPtr.Zero, hdc);
            }
        }

        public static void Gdi3()
        {
            var dc = GetDC(IntPtr.Zero);
            var dcCopy = CreateCompatibleDC(dc);

            BITMAPINFO bmpi = new BITMAPINFO();
            bmpi.bmiHeader.biSize = (uint)Marshal.SizeOf(bmpi.bmiHeader);
            bmpi.bmiHeader.biWidth = w;
            bmpi.bmiHeader.biHeight = h;
            bmpi.bmiHeader.biPlanes = 1;
            bmpi.bmiHeader.biBitCount = 32;
            bmpi.bmiHeader.biCompression = BI_RGB;

            IntPtr bits;
            var hBitmap = CreateDIBSection(dc, ref bmpi, DIB_RGB_COLORS, out bits, IntPtr.Zero, 0);
            SelectObject(dcCopy, hBitmap);

            while (true)
            {
                StretchBlt(dcCopy, 0, 0, w, h, dc, 0, 0, w, h, SRCCOPY);

                unsafe
                {
                    RGBQUAD* rgbquad = (RGBQUAD*)bits;

                    for (int y = 0; y < h; y++)
                    {
                        for (int x = 0; x < w; x++)
                        {
                            int index = y * w + x;

                            int centerX = w / 2;
                            int centerY = h / 2;
                            double distance = Math.Sqrt(Math.Pow(x - centerX, 2) + Math.Pow(y - centerY, 2));

                            double hueShift = distance * 0.05 + Environment.TickCount * 0.02;
                            double newHue = (hueShift % 360) / 360.0;

                            double r = rgbquad[index].rgbRed / 255.0;
                            double g = rgbquad[index].rgbGreen / 255.0;
                            double b = rgbquad[index].rgbBlue / 255.0;

                            double max = Math.Max(r, Math.Max(g, b));
                            double min = Math.Min(r, Math.Min(g, b));
                            double delta = max - min;

                            double hue = 0, saturation = 0, luminance = (max + min) / 2;

                            if (delta > 0)
                            {
                                saturation = luminance < 0.5 ? delta / (max + min) : delta / (2 - max - min);

                                if (r == max)
                                    hue = (g - b) / delta + (g < b ? 6 : 0);
                                else if (g == max)
                                    hue = (b - r) / delta + 2;
                                else
                                    hue = (r - g) / delta + 4;

                                hue /= 6;
                            }

                            hue = (hue + newHue) % 1.0;
                            HLS2RGB(hue, luminance, saturation, out byte red, out byte green, out byte blue);

                            rgbquad[index].rgbRed = red;
                            rgbquad[index].rgbGreen = green;
                            rgbquad[index].rgbBlue = blue;
                            rgbquad[index].rgbReserved = 255;
                        }
                    }
                }

                AlphaBlend(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, new BLENDFUNCTION(255));

                Thread.Sleep(100);
            }
        }

        public static void Gdi4()
        {
            var dc = GetDC(IntPtr.Zero);
            var dcCopy = CreateCompatibleDC(dc);

            BITMAPINFO bmpi = new BITMAPINFO();
            bmpi.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
            bmpi.bmiHeader.biWidth = w;
            bmpi.bmiHeader.biHeight = h;
            bmpi.bmiHeader.biPlanes = 1;
            bmpi.bmiHeader.biBitCount = 32;
            bmpi.bmiHeader.biCompression = 0;

            IntPtr bits;
            var bmp = CreateDIBSection(dc, ref bmpi, 0, out bits, IntPtr.Zero, 0);
            var oldBmp = SelectObject(dcCopy, bmp);

            Random rand = new Random();
            double time = 0;

            while (true)
            {
                unsafe
                {
                    RGBQUAD* rgbquad = (RGBQUAD*)bits;

                    for (int x = 0; x < w; x++)
                    {
                        for (int y = 0; y < h; y++)
                        {
                            int index = y * w + x;

                            double angle = Math.Atan2(y - h / 2, x - w / 2);
                            double radius = Math.Sqrt(Math.Pow(x - w / 2, 2) + Math.Pow(y - h / 2, 2));

                            byte red = (byte)((Math.Sin(angle * 3 + time) * 127 + 128) * (rand.NextDouble() * 0.5 + 0.5));
                            byte green = (byte)((Math.Sin(radius * 0.05 + time) * 127 + 128) * (rand.NextDouble() * 0.5 + 0.5));
                            byte blue = (byte)((Math.Sin(angle * 2 + radius * 0.01 + time) * 127 + 128) * (rand.NextDouble() * 0.5 + 0.5));

                            rgbquad[index].rgbRed = red;
                            rgbquad[index].rgbGreen = green;
                            rgbquad[index].rgbBlue = blue;
                            rgbquad[index].rgbReserved = 0;
                        }
                    }
                }

                StretchBlt(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, SRCCOPY);

                time += 0.1;
                Thread.Sleep(30);
            }
        }

        public static void Gdi5()
        {
            var hdc = GetDC(IntPtr.Zero);
            var mhdc = CreateCompatibleDC(hdc);
            var hbit = CreateCompatibleBitmap(hdc, SCREEN_WIDTH, SCREEN_HEIGHT);
            var holdbit = SelectObject(mhdc, hbit);
            try
            {
                while (true)
                {

                    BitBlt(mhdc, 0, 0, SCREEN_WIDTH, SCREEN_HEIGHT, hdc, 0, 0, SRCCOPY);

                    BitBlt(mhdc, 0, 0, SCREEN_WIDTH, SCREEN_HEIGHT, mhdc, 0, 0, NOTSRCCOPY);

                    int xOffset = rand.Next(-4, 4);
                    int yOffset = rand.Next(-4, 4);

                    BitBlt(hdc, xOffset, yOffset, SCREEN_WIDTH, SCREEN_HEIGHT, mhdc, 0, 0, SRCCOPY);

                    Thread.Sleep(10);
                }
            }
            finally
            {
                SelectObject(mhdc, holdbit);
                DeleteObject(hbit);
                DeleteDC(mhdc);
                ReleaseDC(IntPtr.Zero, hdc);
            }
        }

        public static void Gdi6()
        {
            var dc = GetDC(IntPtr.Zero);
            var dcCopy = CreateCompatibleDC(dc);

            BITMAPINFO bmpi = new BITMAPINFO();
            bmpi.bmiHeader.biSize = (uint)Marshal.SizeOf(typeof(BITMAPINFOHEADER));
            bmpi.bmiHeader.biWidth = w;
            bmpi.bmiHeader.biHeight = h;
            bmpi.bmiHeader.biPlanes = 1;
            bmpi.bmiHeader.biBitCount = 32;
            bmpi.bmiHeader.biCompression = 0;

            IntPtr bits;
            var bmp = CreateDIBSection(dc, ref bmpi, 0, out bits, IntPtr.Zero, 0);
            var oldBmp = SelectObject(dcCopy, bmp);

            Random rand = new Random();
            double time = 0;

            while (true)
            {
                unsafe
                {
                    RGBQUAD* rgbquad = (RGBQUAD*)bits;

                    for (int x = 0; x < w; x++)
                    {
                        for (int y = 0; y < h; y++)
                        {
                            int index = y * w + x;

                            byte red = (byte)((Math.Sin(x * 0.05 + time) * 127 + 128) * (rand.NextDouble() * 0.5 + 0.5));
                            byte green = (byte)((Math.Sin(y * 0.05 + time) * 127 + 128) * (rand.NextDouble() * 0.5 + 0.5));
                            byte blue = (byte)((Math.Sin((x + y) * 0.05 + time) * 127 + 128) * (rand.NextDouble() * 0.5 + 0.5));

                            rgbquad[index].rgbRed = red;
                            rgbquad[index].rgbGreen = green;
                            rgbquad[index].rgbBlue = blue;
                            rgbquad[index].rgbReserved = 0;
                        }
                    }
                }

                StretchBlt(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, SRCCOPY);

                time += 0.1;
                Thread.Sleep(30); 
            }
        }

        public static void Gdi7()
        {

        }
    }
}