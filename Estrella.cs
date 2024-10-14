using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostroDiBiscottiMonoGame
{
    public class Estrella
    {
        public VPoint punto;
        public int Id;
        public Estrella(float x, float y, int Id, int width, int height)
        {
            punto = new VPoint(x, y, Id, width, height);
            punto.PinPoint();
            Color c = Color.Yellow;
            //.image = Resource2.pixil_frame_0__12_;
            //punto.brush = new SolidBrush(c);
            punto.IsEstrella = true;
        }
    }
}
