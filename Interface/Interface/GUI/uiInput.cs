using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Interface.GUI
{
    class uiInput : uiControl
    {
        string Text;
        Color Color;
        Color ColorIn;

        public uiInput():base()
        {
            initialize();
        }

        public uiInput(Texture2D texture2D, float opacity, Rectangle hitbox, string text, uiControl parent, Color color1, Color color2, BackgroundImagemode backgroundImageMode)
            : base(Color.Transparent, texture2D, opacity, hitbox, "", parent, false, uiControl.BackgroundImagemode.Etirage)
        {
            initialize();
            Text = text;
            Color = color1;
            ColorIn = color2;
        }

        private void initialize()
        {
            Text = "";
            Color = Color.Transparent;
            ColorIn = Color.Transparent;
        }

        public override void Update(MouseState Mouse, KeyboardState keyboard, GameTime gametime, int x, int y)
        {
            base.Update(Mouse, keyboard, gametime, x, y);
        }

        public override void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            base.Draw(spriteBatch, x, y);
        }
    }
}
