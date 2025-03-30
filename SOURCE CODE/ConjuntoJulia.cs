using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

public class JuliaFractal : Form
{
    private Bitmap bmp;
    private Graphics gfx;
    private double zoom = 2.0;
    private double moveX = 0, moveY = 0;
    private int maxIterations = 500;
    private double cRe = -0.7, cIm = 0.27015;

    public JuliaFractal()
    {
        this.Width = Screen.PrimaryScreen.Bounds.Width;
        this.Height = Screen.PrimaryScreen.Bounds.Height;
        this.FormBorderStyle = FormBorderStyle.None;
        this.ControlBox = false;
        this.ShowInTaskbar = false;
        this.ShowIcon = false;
        this.TopMost = true;
        this.DoubleBuffered = true;
        this.MouseClick += new MouseEventHandler(this.OnMouseClick);
        this.FormClosing += new FormClosingEventHandler(this.OnFormClosing);
        bmp = new Bitmap(this.Width, this.Height, PixelFormat.Format32bppArgb);
        gfx = Graphics.FromImage(bmp);
        new Thread(RenderJulia).Start();
        this.Opacity = 1;
        this.BackColor = TransparencyKey;
    }


    protected override void OnPaint(PaintEventArgs e)
    {
        lock (bmp)
        {
            e.Graphics.DrawImage(bmp, 0, 0, this.Width, this.Height);
        }
    }

    private void RenderJulia()
    {
        int screenWidth = this.Width;
        int screenHeight = this.Height;

        while (true)
        {
            Bitmap tempBmp = new Bitmap(screenWidth, screenHeight, PixelFormat.Format32bppArgb);

            Parallel.For(0, screenWidth, x =>
            {
                for (int y = 0; y < screenHeight; y++)
                {
                    double zx = 1.5 * (x - screenWidth / 2) / (0.5 * zoom * screenWidth) + moveX;
                    double zy = (y - screenHeight / 2) / (0.5 * zoom * screenHeight) + moveY;
                    double cX = cRe;
                    double cY = cIm;
                    int i = maxIterations;

                    while (zx * zx + zy * zy < 4 && i > 0)
                    {
                        double tmp = zx * zx - zy * zy + cX;
                        zy = 2.0 * zx * zy + cY;
                        zx = tmp;
                        i--;
                    }

                    int red = Math.Min(255, i % 256);
                    int green = Math.Min(255, (i * 2) % 256);
                    int blue = Math.Min(255, (i * 3) % 256);

                    lock (tempBmp)
                    {
                        tempBmp.SetPixel(x, y, Color.FromArgb(red, green, blue));
                    }
                }
            });

            lock (bmp)
            {
                gfx.DrawImage(tempBmp, 0, 0);
            }
            this.Invalidate();
            Thread.Sleep(100);
        }
    }

    private void OnMouseClick(object sender, MouseEventArgs e)
    {
        // Zoom in
        if (e.Button == MouseButtons.Left)
        {
            zoom *= 1.5;
            moveX += (e.X - this.Width / 2.0) / (0.5 * zoom * this.Width);
            moveY += (e.Y - this.Height / 2.0) / (0.5 * zoom * this.Height);
        }

        // Zoom out
        else if (e.Button == MouseButtons.Right)
        {
            zoom /= 1.5;
        }

        new Thread(RenderJulia).Start();
    }


    private void OnFormClosing(object sender, FormClosingEventArgs e)
    {
        e.Cancel = true;
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // JuliaFractal
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.Name = "JuliaFractal";
            this.ResumeLayout(false);

    }
}