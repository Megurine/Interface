using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Interface.GUI
{
    class uiPictureBox : uiControl
    {
        public uiPictureBox()
        {}

        public uiPictureBox(Texture2D texture2D, float opacity, Rectangle hitbox, uiControl parent, BackgroundImagemode backgroundImageMode)
            : base(Color.Transparent, texture2D, opacity, hitbox, null, parent, false, backgroundImageMode)
        {}

        public override void Update(MouseState Mouse, KeyboardState keyboard, GameTime gametime, int x, int y)
        {
            base.Update(Mouse, keyboard, gametime,x , y);
        }

        public override void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            base.Draw(spriteBatch, x, y);
        }
    }
}
