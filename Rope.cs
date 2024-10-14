using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostroDiBiscottiMonoGame
{
    public class Rope
    {
        public List<VPoint> Points;
        public List<VPole> Poles;
        Caramelo caramelo;

        public Rope(ref Pin pin, ref Caramelo caramelo)
        {
            Points = new List<VPoint>();
            Poles = new List<VPole>();
            this.caramelo = caramelo;
            caramelo.punto.isInRope = true;

            addPoint(pin.punto);

            CalculateIntermediatePoints(ref pin.punto,ref caramelo.punto);

        }

        private void CalculateIntermediatePoints(ref VPoint startPoint, ref VPoint endPoint)
        {
            // Calcular la distancia total entre los dos puntos
            float distanceX = endPoint.pos.X - startPoint.pos.X;
            float distanceY = endPoint.pos.Y - startPoint.pos.Y;

            //Cantidad de nodos
            float nodos = 10;

            // Calcular el paso para cada coordenada (X, Y)
            float stepX = distanceX / nodos;
            float stepY = distanceY / nodos;

            // Agregar los puntos intermedios a la lista de puntos
            for (int i = 1; i < nodos; i++)
            {
                float newX = startPoint.pos.X + stepX * i;
                float newY = startPoint.pos.Y + stepY * i;
                VPoint tempPoint = new VPoint(newX, newY, -1000 + i, startPoint.width, startPoint.height);
                tempPoint.IsPartOfTheRope = true;
                tempPoint.IsVisible = false;
                tempPoint.Mass = 0.1f;
                Points.Add(tempPoint);
            }
            VPole Pole_i;
            for (int i = 0; i < Points.Count; i++)
            {
                if (i + 1 < Points.Count)
                {
                    Pole_i = new VPole(Points[i], Points[i + 1]);
                    addPole(Pole_i);
                }
            }
            Pole_i = new VPole(Points.Last(), caramelo.punto);
            addPole(Pole_i);
        }
        public void addPoint(VPoint point)
        {
            Points.Add(point);
        }
        public void addPole(VPole pole)
        {
            Poles.Add(pole);

        }
    }
    }



