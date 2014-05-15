using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace Interface.GUI
{
    class uiButton : uiControl
    {
        public string Text;
        public int idAction;
        Texture2D textureBouttonHover;
        Texture2D textureBouttonClick;
        bool hover;
        bool click;
        bool enDeplacement;
        Color Color;
        Color ColorHover;

        public uiButton()
        {
            initialize();
        }

        public uiButton(Texture2D texture2D, float opacity, Rectangle hitbox, string text, int idaction, uiControl parent, Texture2D textureHover, Texture2D textureClick, Color color1, Color color2, BackgroundImagemode backgroundImageMode)
            : base(Color.Transparent, texture2D, opacity, hitbox, null, parent, false, backgroundImageMode)
        {
            initialize();
            Text = text;
            idAction = idaction;
            textureBouttonHover = textureHover;
            textureBouttonClick = textureClick;
            Color = color1;
            ColorHover = color2;
        }

        private void initialize()
        {
            enDeplacement = false;
            Text = "";
            idAction = 0;
            textureBouttonHover = null;
            textureBouttonClick = null;
            hover = false;
            click = false;
            Color = Color.Transparent;
            ColorHover = Color.Transparent;
        }

        public override void Update(MouseState Mouse, KeyboardState keyboard, GameTime gametime, int x, int y)
        {
            base.Update(Mouse, keyboard, gametime, x , y);
        }

        public override void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            Color c = Color;
            Texture2D defaut = BackgroundImage;
            if (click)
            {
                BackgroundImage = textureBouttonClick;
                c = ColorHover;
            }
            else
            {
                if (hover)
                {
                    BackgroundImage = textureBouttonHover;
                    c = ColorHover;
                }
            }

            base.Draw(spriteBatch, x, y);

            Vector2 vecteurText = Ressources.Font.MeasureString(Text);
            vecteurText = new Vector2((Hitbox.Width - vecteurText.X) / 2, (Hitbox.Height - vecteurText.Y) / 2);
            spriteBatch.DrawString(Ressources.Font, Text, new Vector2(Hitbox.X + x + vecteurText.X, Hitbox.Y + y + vecteurText.Y), c * Opacity);

            BackgroundImage = defaut;
        }

        public override void MouseEventIn(object sender, object arg, int x, int y)
        {
            click = true;
            enDeplacement = true;
        }

        public override void MouseEventOut(object sender, object arg, int x, int y)
        {
            click = false;
            if (enDeplacement)
            {
                enDeplacement = false;
                if (new Rectangle(this.Hitbox.X + x, this.Hitbox.Y + y, this.Hitbox.Width, this.Hitbox.Height).Intersects(new Rectangle(((MouseState)arg).X, ((MouseState)arg).Y, 1, 1)))
                {
                    Ressources.faireAction(this);
                }
            }
        }

        public override void MouseEventEnter(object sender, object arg, int x, int y)
        {
            hover = true;
        }
        public override void MouseEventLeave(object sender, object arg, int x, int y)
        {
            hover = false;
        }
    }
}
