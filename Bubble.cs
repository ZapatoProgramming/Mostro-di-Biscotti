using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostroDiBiscottiMonoGame
{
    public class Bubble
    {
        public VPoint punto;
        public int Id;
        public Bubble(float x, float y, int Id, int width, int height, int type = 0)
        {
            punto = new VPoint(x, y, Id, width, height,30);
            Color c = Color.Blue;
            punto.IsBubble = true;
            if (type == 0) {
                punto.IsPinned = true;
                punto.gravity = new Vec2(0, -1);
                punto.friction = 0;
            }
            
            if (type == 1) {
                punto.IsLBubble = true;
                punto.IsPinned = true;
                punto.gravity = new Vec2(-1, 0);
                punto.friction = 0.5f;
            }
            if (type == 2)
            {
                punto.IsRBubble = true;
                punto.IsPinned = true;
                punto.gravity = new Vec2(1, 0);
                punto.friction = 0.5f;
            }
        }
    }
}
