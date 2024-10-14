using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostroDiBiscottiMonoGame
{
    public class GraphicsEquivalent
    {
        public static void DrawThinRectangle(SpriteBatch spriteBatch, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            float length = edge.Length();

            spriteBatch.Draw(Global.pixelTexture,
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)length,
                    Global.thickness),
                null,
                Global.color,
                angle,
                new Vector2(0, 0.5f),
                SpriteEffects.None,
                0);
        }
    }
}
