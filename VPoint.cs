using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MostroDiBiscottiMonoGame
{
    public class VPoint
    {
        //public Image image;
        public Vec2 pos, old, vel, gravity;
        public VPoint pelotadentro;
        public float Mass;
        public int Id, width, height, startWidth;
        public bool IsVisible;
        public bool IsPinned;
        public bool IsEstrella;
        public bool IsLBubble;
        public bool IsRBubble;
        public bool IsBubble;
        public bool IsPartOfTheRope;
        public bool IsPartOfSoftBody;
        public bool isInRope;
        public bool IsCaramelo;
        public bool isPin;
        public bool IsInBubble;
        public bool isInLBubble;
        public bool isInRBubble;
        public bool IsMegaEstrella;
        public bool tienePelota;
        public float radius, diameter, m, friction;
        public float groundFriction;
        public Color c;
        //public SolidBrush brush;

        public VPoint(float x, float y, int Id, int width, int height, int radius = 20)
        {
            Init(x, y, Id, width, height, radius);
        }
        public void Init(float x, float y, int Id, int width, int height, int radius)
        {
            startWidth = 0;
            this.width = width;
            this.height = height;
            this.Id = Id;
            IsVisible = true;
            IsPinned = false;
            IsEstrella = false;
            IsPartOfTheRope = false;
            IsCaramelo = false;
            IsPartOfSoftBody = false;
            IsBubble = false;
            IsLBubble = false;
            IsInBubble = false;
            isInLBubble = false;
            isInRBubble = false;
            tienePelota = false;
            IsRBubble = false;
            IsMegaEstrella = false;
            pos = new Vec2(x, y);
            old = new Vec2(x, y);
            friction = 1f;
            groundFriction = 0.99f;
            gravity = new Vec2(0, 0.1);
            this.radius = radius;
            c = Color.Orange;
            //brush = new SolidBrush(c);
            Mass = 1f;
            diameter = 2 * this.radius;
        }
        public void PinPoint() {
            IsPinned = true;
        }
        public void MakeInvisible()
        {
            IsVisible = true;
        }
        public void Update()
        {
            if (!IsPinned && !IsInBubble) {
                vel = (pos - old) * friction;
                if (pos.Y >= height - radius && vel.MagSqr() > 0.000001)
                {
                    m = vel.Length();
                    vel /= m;
                    vel *= (m * groundFriction);
                }
                old = pos;
                pos += vel + gravity;
            }
        }
        public void Constraints() {
            if (pos.X > width - radius) pos.X = width - radius;
            if (pos.X < radius + startWidth) pos.X = radius + startWidth;
            if (pos.Y > height - radius) {
                pos.Y = height - radius;
                if (IsCaramelo) Global.lost = true;
            }
            if (pos.Y < radius) pos.Y = radius;
        }
    
        }
    }

            
  
