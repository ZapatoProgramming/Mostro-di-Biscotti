using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostroDiBiscottiMonoGame
{
    public class BubbleManager
    {
        public static void ExplotarBurbuja()
        {
            for (int i = 0; i < Global.Verlets.Points.Count; i++)
            {
                float dis = Global.worldMousePosition.Distance(Global.Verlets.Points[i].pos);
                if (dis < (Global.Verlets.Points[i].radius) && Global.Verlets.Points[i].IsBubble)
                {
                    if (Global.Verlets.Points[i].tienePelota)
                    {
                        SoundManager.StopEffectBubble();
                        SoundManager.PlayEffectBubble();
                        VPoint temp2 = Global.Verlets.Points[i];
                        Caramelo temp = new Caramelo(Global.Verlets.Points[i].pelotadentro.pos.X,
                            Global.Verlets.Points[i].pelotadentro.pos.Y, Global.Verlets.Points[i].pelotadentro.Id,
                            2 * Global.GraphicsDevice.Viewport.Width, Global.GraphicsDevice.Viewport.Height);

                        Global.Verlets.Points.RemoveAt(i);
                        for (int j = 0; j < Global.Verlets.Points.Count; j++)
                        {
                            if (Global.Verlets.Points[j] == temp2.pelotadentro)
                            {
                                Global.Verlets.Points.RemoveAt(j);
                                break;
                            }
                        }
                        Global.caramelo = temp;
                        Global.Verlets.addPoint(Global.caramelo.punto);
                        break;
                    }
                }
            }
        }


    }
}
