using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostroDiBiscottiMonoGame
{
    public class RopeManager
    {
        public static void Cut()
        {
            for (int i = 0; i < Global.Verlets.Cuerdas.Count; i++)
            {
                for (int j = 0; j < Global.Verlets.Cuerdas[i].Poles.Count; j++)
                {
                    if (Global.worldMousePosition.Distance(Global.Verlets.Cuerdas[i].Poles[j].middlepoint) < (50))
                    {
                        SoundManager.StopEffectCuerda();
                        SoundManager.PlayEffectCuerda();
                        Global.Verlets.Cuerdas.RemoveAt(i);
                        break;
                    }
                }
            }
        }
    }
}
