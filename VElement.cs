using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MostroDiBiscottiMonoGame
{
    public class VElement
    {
        public List<VPoint> Points;
        public List<VPole> Poles;
        public List<Rope> Cuerdas;
        public List<SoftBody> bodies;
        public List<Pin> Pines;
 
        int width;
        int height;
        public VElement(int width, int heigth) {
            Init(width, heigth);
        }
        public void Init(int width, int heigth) {
            this.width = width;
            this.height = heigth;
            Points = new List<VPoint>();
            Poles = new List<VPole>();
            Pines = new List<Pin>();
            Cuerdas = new List<Rope>();
            bodies = new List<SoftBody>();
        }
        public void Clear() { 
            Points.Clear(); Poles.Clear(); bodies.Clear(); Cuerdas.Clear(); Pines.Clear();
        }
        public void addPoint(VPoint pointn) {
            Points.Add(pointn);
        }
        public void addPole(VPole pole)
        {
            Poles.Add(pole);
        }
        public void addPin(Pin pin)
        {
            Pines.Add(pin);
        }
        public void AdministratorPoints(int s, int p) {
            VPoint p1 = Points[s];
            VPoint p2 = Points[p];
            //if (p1.IsBubble && p2.IsEstrella) return;
            if (p1.pelotadentro == p2) {
                p2.pos = p1.pos;
                return;
            }
            if (p1.IsCaramelo)
            {
                if (bodies[0].Points[1].pos.Distance(p1.pos) < (300 / 1))
                {
                    if (!Global.won) bodies[0].abreboca = true;
                    else bodies[0].abreboca = false;
                } else bodies[0].abreboca = false;
                if (p1.pos.X >= bodies[0].Points[0].pos.X && p1.pos.X <= (bodies[0].Points[0].pos.X + 33)
                    && p1.pos.Y >= bodies[0].Points[0].pos.Y && p1.pos.Y <= (bodies[0].Points[0].pos.Y + 33))
                {
                    SoundManager.PlayEffectIntro();
                    SoundManager.PlayEffectYomi();
                    Global.won = true;
                    return;
                }
            }
            if (p1.Id == p2.Id) // BY ID
                return;
            if (p1.IsPinned && p2.IsPinned)
                return;
            if (p1.IsPartOfTheRope || p2.IsPartOfTheRope)
                return;
            Vec2 axis = p1.pos - p2.pos; // vector de direccion
            float dis = axis.Length(); // magnitud
            if (dis < (p1.radius + p2.radius))//COLLISION DETECTED
            {
                 if (p2.IsEstrella)
                 {
                    SoundManager.StopEffectEstrella();
                    SoundManager.PlayEffectEstrella();
                     Global.puntaje++;
                     Points.Remove(p2);
                     return;
                 }
                if (p1.IsEstrella)
                {
                    SoundManager.StopEffectEstrella();
                    SoundManager.PlayEffectEstrella();
                    Global.puntaje++;
                    Points.Remove(p1);
                    return;
                }
                if (p2.IsMegaEstrella)
                {
                    SoundManager.PlayEffectIntro();
                    SoundManager.StopEffectEstrella();
                    SoundManager.PlayEffectEstrella();
                    Global.won = true;
                    Points.Remove(p2);
                    return;
                }
                if (p1.IsMegaEstrella)
                {
                    SoundManager.PlayEffectIntro();
                    SoundManager.StopEffectEstrella();
                    SoundManager.PlayEffectEstrella();
                    Global.won = true;
                    Points.Remove(p1);
                    return;
                }
                if (p1.IsBubble && p2.IsBubble) 
                {
                    if(p1.tienePelota) Points.Remove(p2);
                    if (p2.tienePelota) Points.Remove(p1);
                    return;
                }
                if (p1.IsBubble && p2.IsCaramelo)
                {
                    if (p2.isInRope) Cuerdas.Clear();
                    p2.IsInBubble = true;
                    if (p1.IsLBubble) 
                    {
                        SoundManager.StopEffectAvion();
                        SoundManager.PlayEffectAvion();
                        p2.isInLBubble = true;
                    }
                    if (p1.IsRBubble) 
                    {
                        SoundManager.StopEffectAvion();
                        SoundManager.PlayEffectAvion();
                        p2.isInRBubble = true; 
                    }
                    p1.IsPinned = false;
                    p1.Id = p2.Id;
                    p1.pelotadentro = p2;
                    p1.tienePelota = true;
                    //Points.RemoveAt(s);
                    return;
                }
                if (p2.IsPartOfSoftBody && p1.IsCaramelo)
                {
                    SoundManager.PlayEffectIntro();
                    SoundManager.PlayEffectYomi();
                    Global.won = true;
                    return;
                }
                // dividir la fuerza para repartir entre ambas colisiones
                float dif = (dis - (p1.radius + p2.radius)) * 0.2f;
                Vec2 normal = axis / dis; // normalizar la direccion para tener el vector unitario
                Vec2 res = dif * normal; // vector resultante

                    if (!p1.IsPinned)
                        if (p2.IsPinned)
                            p1.pos -= res * 1.2f;
                        else
                            p1.pos -= res;
                    if (!p2.IsPinned)
                        if (p1.IsPinned)
                            p2.pos += res * 1.2f;
                        else
                            p2.pos += res;

            }
        }
        public void Update() {
            for (int s = 0; s < Points.Count; s++) {
                Points[s].Update();
                //AdministratorPoles(s);
                for (int p = 0; p < Points.Count; p++) {
                     AdministratorPoints(s, p);
                }
            }
            for (int i = 0; i < Points.Count; i++)
            {
                if (!Points[i].IsPartOfTheRope)
                {
                    Points[i].Update();
                    Points[i].Constraints();
                }
            }
            for (int i = 0; i < Cuerdas.Count; i++)
            {
                for (int j = 0; j < Cuerdas[i].Poles.Count; j++) Cuerdas[i].Poles[j].Update();
            }

            for (int i = 0; i < bodies.Count; i++)
            {
                for (int j = 0; j < bodies[i].Poles.Count; j++) bodies[i].Poles[j].Update();
            }
        }
        public void Render(SpriteBatch _spriteBatch)
        {
            //MONSTRUO
            if (Global.monstruo.abreboca) _spriteBatch.Draw(Global.monstruoTextureAbre, new Rectangle((int)(Global.monstruo.Points[0].pos.X - Global.cameraMono.Position.X), (int)Global.monstruo.Points[0].pos.Y, 120, 120), Color.White);
            else _spriteBatch.Draw(Global.monstruoTexture, new Rectangle((int)(Global.monstruo.Points[0].pos.X - Global.cameraMono.Position.X), (int)Global.monstruo.Points[0].pos.Y, 120, 120), Color.White);

            for (int i = 0; i < Cuerdas.Count; i++)
            {
                for (int j = 0; j < Cuerdas[i].Poles.Count; j++)
                {
                    Vector2 startPoint = new Vector2(Cuerdas[i].Poles[j].startPoint.pos.X - Global.cameraMono.Position.X,
                        Cuerdas[i].Poles[j].startPoint.pos.Y);
                    Vector2 endPoint = new Vector2(Cuerdas[i].Poles[j].endPoint.pos.X - Global.cameraMono.Position.X,
                        Cuerdas[i].Poles[j].endPoint.pos.Y);
                    GraphicsEquivalent.DrawThinRectangle(_spriteBatch, startPoint, endPoint);
                }
            }

            for (int i = 0; i < Points.Count; i++)
            {
                if (Points[i].isPin)
                {
                    Global.spritePin.UpdateSprite(Points[i].pos.X - Points[i].radius - Global.cameraMono.Position.X,
                        Points[i].pos.Y - Points[i].radius);
                    _spriteBatch.Draw(Global.spritePin.texture, Global.spritePin.rectangle, Color.White);
                }
                if (Points[i].IsBubble && !Points[i].IsLBubble && !Points[i].IsRBubble)
                {
                    Global.spriteBubble.UpdateSprite(Points[i].pos.X - Points[i].radius - Global.cameraMono.Position.X,
                        Points[i].pos.Y - Points[i].radius);
                    _spriteBatch.Draw(Global.spriteBubble.texture, Global.spriteBubble.rectangle, Color.White);
                }
                if (Points[i].IsLBubble && !Points[i].tienePelota)
                {
                    Global.spriteLBubble.UpdateSprite(Points[i].pos.X - Points[i].radius - Global.cameraMono.Position.X,
                        Points[i].pos.Y - Points[i].radius);
                    _spriteBatch.Draw(Global.spriteLBubble.texture, Global.spriteLBubble.rectangle, Color.White);
                }
                if (Points[i].IsLBubble && Points[i].tienePelota)
                {
                    Global.spriteLBubbleA.UpdateSprite(Points[i].pos.X - Points[i].radius - Global.cameraMono.Position.X,
                        Points[i].pos.Y - Points[i].radius);
                    _spriteBatch.Draw(Global.spriteLBubbleA.texture, Global.spriteLBubbleA.rectangle, Color.White);
                }
                if (Points[i].IsRBubble && !Points[i].tienePelota)
                {
                    Global.spriteRBubble.UpdateSprite(Points[i].pos.X - Points[i].radius - Global.cameraMono.Position.X,
                        Points[i].pos.Y - Points[i].radius);
                    _spriteBatch.Draw(Global.spriteRBubble.texture, Global.spriteRBubble.rectangle, Color.White);
                }
                if (Points[i].IsRBubble && Points[i].tienePelota)
                {
                    Global.spriteRBubbleA.UpdateSprite(Points[i].pos.X - Points[i].radius - Global.cameraMono.Position.X,
                        Points[i].pos.Y - Points[i].radius);
                    _spriteBatch.Draw(Global.spriteRBubbleA.texture, Global.spriteRBubbleA.rectangle, Color.White);
                }
                if (Points[i].IsEstrella)
                {
                    Global.spriteEstrella.UpdateSprite(Points[i].pos.X - Points[i].radius - Global.cameraMono.Position.X,
                        Points[i].pos.Y - Points[i].radius);
                    _spriteBatch.Draw(Global.spriteEstrella.texture, Global.spriteEstrella.rectangle, Color.White);
                }
                if (Points[i].IsMegaEstrella)
                {
                    Global.spriteMegaEstrella.UpdateSprite(Points[i].pos.X - Points[i].radius - Global.cameraMono.Position.X,
                        Points[i].pos.Y - Points[i].radius);
                    _spriteBatch.Draw(Global.spriteMegaEstrella.texture, Global.spriteMegaEstrella.rectangle, Color.White);
                }
                if (Points[i].IsCaramelo && !Global.won && !Points[i].isInLBubble && !Points[i].isInRBubble)
                {
                    Global.spriteCaramelo.UpdateSprite(Points[i].pos.X - Points[i].radius - Global.cameraMono.Position.X,
                        Points[i].pos.Y - Points[i].radius);
                    _spriteBatch.Draw(Global.spriteCaramelo.texture, Global.spriteCaramelo.rectangle, Color.White);
                }
            }

        }
    }
}
