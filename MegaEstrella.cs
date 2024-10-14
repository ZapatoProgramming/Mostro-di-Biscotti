using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostroDiBiscottiMonoGame
{
    public class MegaEstrella
    {
        public VPoint punto;
        public int Id;
        public MegaEstrella(float x, float y, int Id, int width, int height)
        {
            punto = new VPoint(x, y, Id, width, height, 100);
            punto.PinPoint();
            //.image = Resource2.pixil_frame_0__12_;
            //punto.brush = new SolidBrush(c);
            punto.IsMegaEstrella = true;
        }
    }
}
