using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using static Windows.APIs;

namespace Nemesis
{
    public class EffectsGdiSpeciais
    {
        public static void DesForm()
        {
            var desk = GetDC(NULL);
            int w = SCREEN_WIDTH;
            int h = SCREEN_HEIGHT;

            int size = 70;
            int x = w / 2;
            int y = h / 2;
            int xDirection = 1;
            int yDirection = 1;
            int speed = 10;

            int r = 255, g = 0, b = 0;
            double angle = 0;

            Shape currentShape = Shape.Square;

            while (true)
            {
                if (r > 0 && b == 0)
                {
                    r--;
                    g++;
                }
                if (g > 0 && r == 0)
                {
                    g--;
                    b++;
                }
                if (b > 0 && g == 0)
                {
                    b--;
                    r++;
                }

                uint color = (uint)((r << 16) | (g << 8) | b);

                IntPtr brush = CreateSolidBrush(color);
                IntPtr oldBrush = SelectObject(desk, brush);

                POINT[] points = GetShapePoints(x, y, size, currentShape, angle);

                Polygon(desk, points, points.Length);
                SelectObject(desk, oldBrush);
                DeleteObject(brush);

                x += xDirection * speed;
                y += yDirection * speed;

                if (x - size <= 0 || x + size >= w)
                {
                    xDirection *= -1;
                    currentShape = GetNextShape(currentShape);
                }
                if (y - size <= 0 || y + size >= h)
                {
                    yDirection *= -1;
                    currentShape = GetNextShape(currentShape);
                }

                angle += 0.05;
                Sleep(10);
            }

            ReleaseDC(GetDesktopWindow(), desk);
        }

        public static POINT[] GetShapePoints(int centerX, int centerY, int size, Shape shape, double angle)
        {
            int numPoints = GetNumberOfPoints(shape);

            POINT[] points = new POINT[numPoints];
            double step = 2 * Math.PI / numPoints;

            for (int i = 0; i < numPoints; i++)
            {
                double currentAngle = i * step + angle;
                int xOffset = (int)(size * Math.Cos(currentAngle));
                int yOffset = (int)(size * Math.Sin(currentAngle));
                points[i] = RotatePoint(centerX, centerY, centerX + xOffset, centerY + yOffset, angle);
            }

            return points;
        }

        public static Point RotatePoint(int centerX, int centerY, int x, int y, double angle)
        {
            double radians = angle;
            double cosTheta = Math.Cos(radians);
            double sinTheta = Math.Sin(radians);

            int newX = (int)(cosTheta * (x - centerX) - sinTheta * (y - centerY) + centerX);
            int newY = (int)(sinTheta * (x - centerX) + cosTheta * (y - centerY) + centerY);

            return new Point(newX, newY);
        }

        public static int GetNumberOfPoints(Shape shape)
        {
            switch (shape)
            {
                case Shape.Square: return 4;
                case Shape.Triangle: return 3;
                case Shape.Hexagon: return 6;
                case Shape.Pentagon: return 5;
                default: return 4;
            }
        }

        public static Shape GetNextShape(Shape current)
        {
            switch (current)
            {
                case Shape.Square: return Shape.Triangle;
                case Shape.Triangle: return Shape.Hexagon;
                case Shape.Hexagon: return Shape.Pentagon;
                case Shape.Pentagon: return Shape.Square;
                default: return Shape.Square;
            }
        }

        public enum Shape
        {
            Square,
            Triangle,
            Hexagon,
            Pentagon
        }

        public static void DesPart()
        {
            var dc = GetDC(NULL);
            var dcCopy = CreateCompatibleDC(dc);
            int w = SCREEN_WIDTH;
            int h = SCREEN_HEIGHT;

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
                            rgbquad[index].rgbRed = (byte)(128 + 127 * Math.Sin(x * 0.01 + Environment.TickCount * 0.01));
                            rgbquad[index].rgbGreen = (byte)(128 + 127 * Math.Sin(y * 0.01 + Environment.TickCount * 0.01));
                            rgbquad[index].rgbBlue = (byte)(128 + 127 * Math.Sin((x + y) * 0.01 + Environment.TickCount * 0.01));
                            rgbquad[index].rgbReserved = 100;
                        }
                    }
                }

                AlphaBlend(dc, 0, 0, w, h, dcCopy, 0, 0, w, h, new BLENDFUNCTION(255));

                Thread.Sleep(20);
            }
        }

        private const int NUM_LINES = 1000;
        private const int LINE_LENGTH = 50;
        private const int MAX_STEP = 5;
        struct Line
        {
            public int x1, y1, x2, y2;
            public float angle1, angle2;
            public Color color;
        }
        public static void LinesEff()
        {
            var dc = GetDC(IntPtr.Zero);
            var dcCopy = CreateCompatibleDC(dc);
            int w = SCREEN_WIDTH;
            int h = SCREEN_HEIGHT;

            IntPtr hBitmap = CreateCompatibleBitmap(dc, w, h);
            SelectObject(dcCopy, hBitmap);

            Random random = new Random();
            Line[] lines = new Line[NUM_LINES];

            for (int i = 0; i < NUM_LINES; i++)
            {
                lines[i].x1 = random.Next(0, w);
                lines[i].y1 = random.Next(0, h);
                lines[i].x2 = lines[i].x1 + LINE_LENGTH * (random.Next(2) == 0 ? 1 : -1);
                lines[i].y2 = lines[i].y1 + LINE_LENGTH * (random.Next(2) == 0 ? 1 : -1);
                lines[i].angle1 = (float)(random.NextDouble() * 2 * Math.PI);
                lines[i].angle2 = (float)(random.NextDouble() * 2 * Math.PI);
                lines[i].color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            }

            using (Graphics g = Graphics.FromHdc(dc))
            {
                while (true)
                {
                    BitBlt(dcCopy, 0, 0, w, h, IntPtr.Zero, 0, 0, BLACKNESS);

                    for (int i = 0; i < NUM_LINES; i++)
                    {
                        using (Pen pen = new Pen(lines[i].color, 2))
                        {
                            g.DrawLine(pen, lines[i].x1, lines[i].y1, lines[i].x2, lines[i].y2);
                        }

                        lines[i].x1 += (int)(Math.Cos(lines[i].angle1) * MAX_STEP);
                        lines[i].y1 += (int)(Math.Sin(lines[i].angle1) * MAX_STEP);
                        lines[i].x2 += (int)(Math.Cos(lines[i].angle2) * MAX_STEP);
                        lines[i].y2 += (int)(Math.Sin(lines[i].angle2) * MAX_STEP);

                        if (lines[i].x1 < 0 || lines[i].x1 >= w || lines[i].x2 < 0 || lines[i].x2 >= w)
                        {
                            lines[i].angle1 = (float)Math.PI - lines[i].angle1;
                            lines[i].angle2 = (float)Math.PI - lines[i].angle2;
                        }
                        if (lines[i].y1 < 0 || lines[i].y1 >= h || lines[i].y2 < 0 || lines[i].y2 >= h)
                        {
                            lines[i].angle1 = -lines[i].angle1;
                            lines[i].angle2 = -lines[i].angle2;
                        }
                    }

                    Thread.Sleep(10);
                }
            }
        }
    }
}
