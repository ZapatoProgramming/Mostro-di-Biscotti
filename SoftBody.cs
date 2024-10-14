using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace MostroDiBiscottiMonoGame
{
    public class SoftBody
    {
        VPoint position;
        public List<VPole> Poles;
        public List<VPoint> Points;
        public bool abreboca = false;
        VPoint FirstVertex;
        VPoint SecondVertex;
        VPoint ThirdVertex;
        VPoint FourthVertex;
        public SoftBody(VPoint position,int side = 100) { 
            this.position = position;
            Poles = new List<VPole>();
            Points = new List<VPoint>();
            FirstVertex = position;
            FirstVertex.friction = 0.5f;
            FirstVertex.groundFriction = 0.1f;
            SecondVertex = new VPoint(position.pos.X+side,position.pos.Y,position.Id-1,position.width/3,position.height);
            SecondVertex.friction = 0.5f;
            SecondVertex.groundFriction = 0.1f;
            ThirdVertex = new VPoint(position.pos.X + side, position.pos.Y + side, position.Id - 1, position.width/3, position.height);
            ThirdVertex.friction = 0.5f;
            ThirdVertex.groundFriction = 0.1f;
            FourthVertex = new VPoint(position.pos.X, position.pos.Y +side, position.Id - 1, position.width / 3, position.height);
            FourthVertex.friction = 0.5f;
            FourthVertex.groundFriction = 0.1f;

            FirstVertex.IsPartOfSoftBody = true;
            SecondVertex.IsPartOfSoftBody = true;
            ThirdVertex.IsPartOfSoftBody = true;
            FourthVertex.IsPartOfSoftBody = true;

            Points.Add(FirstVertex);
            Points.Add(SecondVertex);
            Points.Add(ThirdVertex);
            Points.Add(FourthVertex);

            Poles.Add(new VPole(FirstVertex,SecondVertex));
            Poles.Add(new VPole(SecondVertex, ThirdVertex));
            Poles.Add(new VPole(ThirdVertex, FourthVertex));
            Poles.Add(new VPole(FourthVertex, FirstVertex));
            Poles.Add(new VPole(FirstVertex, ThirdVertex));
            Poles.Add(new VPole(SecondVertex, FourthVertex));


        }
    }
}
