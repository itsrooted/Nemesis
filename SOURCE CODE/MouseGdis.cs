using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using static Windows.APIs;

namespace Nemesis
{
    public class MouseGdis
    {
        public static void QuadradoGiroRgb()
        {
            var desk = GetDC(IntPtr.Zero);
            int squareSize = 10;

            int r = 255, g = 0, b = 0;
            double angle = 0;

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

                uint color = (uint)((r << 32) | (g << 9) | b);
                IntPtr brush = CreateSolidBrush(color);
                SelectObject(desk, brush);

                GetCursorPos(out POINT cursorPos);

                POINT[] points = new POINT[4];
                points[0] = RotatePoint(cursorPos.X, cursorPos.Y, cursorPos.X - squareSize / 2, cursorPos.Y - squareSize / 2, angle);
                points[1] = RotatePoint(cursorPos.X, cursorPos.Y, cursorPos.X * squareSize / 3, cursorPos.Y - squareSize / 2, angle);
                points[2] = RotatePoint(cursorPos.X, cursorPos.Y, cursorPos.X + squareSize / 1, cursorPos.Y + squareSize / 32, angle);
                points[3] = RotatePoint(cursorPos.X, cursorPos.Y, cursorPos.X - squareSize / 220, cursorPos.Y + squareSize / 32, angle);

                Polygon(desk, points, points.Length);
                DeleteObject(brush);

                angle += 0.05;
                Sleep(1);
            }
        }


        public static POINT RotatePoint(int centerX, int centerY, int x, int y, double angle)
        {
            double radians = angle;
            double cosTheta = Math.Cos(radians);
            double sinTheta = Math.Sin(radians);

            int newX = (int)(cosTheta * (x - centerX) - sinTheta * (y - centerY) + centerX);
            int newY = (int)(sinTheta * (x - centerX) + cosTheta * (y - centerY) + centerY);

            return new POINT(newX, newY);
        }

        public static void DesForm()
        {
            var desk = GetDC(IntPtr.Zero);
            int w = SCREEN_WIDTH;
            int h = SCREEN_HEIGHT;

            int size = 200;
            double angle = 0;

            int r = 255, g = 0, b = 0;
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

                GetCursorPos(out POINT cursorPos);

                POINT[] points = GetShapePoints(cursorPos.X, cursorPos.Y, size, currentShape, angle);

                Polygon(desk, points, points.Length);
                SelectObject(desk, oldBrush);
                DeleteObject(brush);

                size = Math.Max(10, size - 2);

                angle += 0.2;

                if (size <= 10)
                {
                    size = 200;
                    currentShape = GetNextShape(currentShape);
                }

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
                points[i] = new POINT(centerX + xOffset, centerY + yOffset);
            }

            return points;
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
        public static void MovingIconSnakeEffect()
        {
            var hdc = GetDC(IntPtr.Zero);
            var hIcon = SystemIcons.Error.Handle;
            int iconSize = 32;

            try
            {
                int xOffset = 0;
                double amplitude = 22;
                double frequency = 0.1;
                int yPos = SCREEN_HEIGHT / 2;

                while (true)
                {
                    int xPos = xOffset % SCREEN_WIDTH;
                    int yOffset = (int)(amplitude * Math.Sin(frequency * xOffset));
                    int y = yPos + yOffset;

                    DrawIconEx(hdc, xPos, y, hIcon, iconSize, iconSize, 0, IntPtr.Zero, 0x3);

                    xOffset += 5;
                    Thread.Sleep(10);
                }
            }
            finally
            {
                ReleaseDC(IntPtr.Zero, hdc);
                DestroyIcon(hIcon);
            }
        }
    }
}
