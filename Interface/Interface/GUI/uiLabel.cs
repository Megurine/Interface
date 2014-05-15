using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Interface.GUI
{
    class uiLabel : uiControl
    {
        string Text;
        bool Bordure;
        Color Color;
        Color BordureColor;

        public uiLabel():base()
        {
            initialize();
        }

        public uiLabel(float opacity, Rectangle hitbox, string text, uiControl parent, bool bordure, Color color1, Color color2)
            : base(Color.Transparent, null, opacity, hitbox, "", parent, false, uiControl.BackgroundImagemode.Etirage)
        {
            initialize();
            Text = text;
            Bordure = bordure;
            Color = color1;
            BordureColor = color2;
        }

        private void initialize()
        {
            Text = "";
            Bordure = false;
            Color = Color.Transparent;
            BordureColor = Color.Transparent;
        }

        public override void Update(MouseState Mouse, KeyboardState keyboard, GameTime gametime, int x, int y)
        {
            base.Update(Mouse, keyboard, gametime, x, y);
        }

        public override void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            if (Bordure)
            {
                Ressources.bordureTexte(spriteBatch, Ressources.Font, Text, new Vector2(Hitbox.X + x, Hitbox.Y + y), Opacity, Color, BordureColor);
            }
            else
            {
                spriteBatch.DrawString(Ressources.Font, Text, new Vector2(Hitbox.X + x, Hitbox.Y + y), Color * Opacity);
            }
        }
    }
}
