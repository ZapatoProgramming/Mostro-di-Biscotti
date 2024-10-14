using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Color = Microsoft.Xna.Framework.Color;

namespace MostroDiBiscottiMonoGame
{
    public class Title
    {

        public static void DrawSreenTitle(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            //PARALLAX
            Global._spriteBatch.Draw(Global.L1, new Rectangle(-(int)Global.cameraMono.Position.X / 20,
                0, 2 * Global.GraphicsDevice.Viewport.Width, Global.GraphicsDevice.Viewport.Height), Color.White);
            Global._spriteBatch.Draw(Global.L2, new Rectangle(-(int)Global.cameraMono.Position.X / 10,
                0, 2 * Global.GraphicsDevice.Viewport.Width, Global.GraphicsDevice.Viewport.Height), Color.White);
            Global._spriteBatch.Draw(Global.background, new Rectangle(-(int)Global.cameraMono.Position.X,
                0, 2 * Global.GraphicsDevice.Viewport.Width, Global.GraphicsDevice.Viewport.Height), Color.White);
            Global.Verlets.Render(Global._spriteBatch);
            spriteBatch.Draw(Global.StartButton.getSprite().texture,Global.StartButton.hitbox, Color.White);
            spriteBatch.Draw(Global.titleSprite.texture, Global.titleSprite.rectangle, Color.White);
            spriteBatch.End();
        }
    }
}
