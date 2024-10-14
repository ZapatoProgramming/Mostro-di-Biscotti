using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MostroDiBiscottiMonoGame
{
    public class Button
    {
        public Rectangle hitbox;
        Sprite Sprite;
        public Button(Rectangle measures) 
        { 
            hitbox = measures;
        }
        public void setSprite(Sprite Sprite) 
        { 
            this.Sprite = Sprite;
        }
        public Sprite getSprite()
        {
            return this.Sprite;
        }
        public bool buttonWasClicked()
        {
            if (Global.MouseState.LeftButton == ButtonState.Pressed) 
            {
                if (Global.mousePosition.X >= hitbox.X && Global.mousePosition.X <= (hitbox.X + hitbox.Width)
                    && Global.mousePosition.Y >= hitbox.Y && Global.mousePosition.Y <= (hitbox.Y + hitbox.Height))
                    { 
                        return true;     
                    }
            }
            return false;
        }
    }
}
