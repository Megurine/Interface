using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Interface.GUI
{
    class uiForm:uiControl
    {
        //public uiControl AcceptButton;
        public Borderstyle BorderStyle;
        public string Text;

        public bool enDeplacement;
        public Point lastPointClient;
        public Point lastPointControl;

        public uiForm():base()
        {
            Text = ".";
            BorderStyle = Borderstyle.None;
            initialize();
        }

        public uiForm(Color backgroundColor, Texture2D texture2D, float opacity, Rectangle hitbox, string text, string name, uiControl parent, Borderstyle borderstyle, bool textureTitre, BackgroundImagemode backgroundImageMode)
            : base(backgroundColor, texture2D, opacity, hitbox, name, parent, textureTitre, backgroundImageMode)
        {
            Text = text;
            BorderStyle = borderstyle;
            initialize();
        }

        private void initialize()
        {
            id = Ressources.indexationUiForm();
            enDeplacement = false;
            if (BorderStyle != Borderstyle.None)
            {
                hauteurBorder = Ressources.tailleTitreFenetre;
            }
            else
            {
                hauteurBorder = 0;
            }
        }

        public override void Update(MouseState Mouse, KeyboardState keyboard, GameTime gametime, int x, int y)
        {
            if (enDeplacement && Ressources.enFenetre)
            {
                MouseEventMove(this, Mouse, x, y);
                Ressources.idFenetreFocused = this.id;
            }
            textTest = this.Hitbox.X.ToString() + " : " + this.Hitbox.Y.ToString();
            base.Update(Mouse, keyboard, gametime, x, y);
        }

        public override void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            base.Draw(spriteBatch, x, y);

            if (BorderStyle != Borderstyle.None)
            {
                if (!TextureTitre)
                {
                    Texture2D t = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                    t.SetData(new Color[] { Color.DeepSkyBlue });
                    spriteBatch.Draw(t, new Rectangle(Hitbox.X, Hitbox.Y, Hitbox.Width, hauteurBorder), Color.White * Opacity);
                }
                spriteBatch.DrawString(Ressources.Font, Text, new Vector2(Hitbox.X + 7, Hitbox.Y + 3), new Color((byte)(0), (byte)(0), (byte)(0)) * Opacity);
            }
        }

        public override void MouseEventMove(object sender, object arg, int x, int y)
        {
            if (enDeplacement)
            {
                int difX = ((MouseState)arg).X - lastPointClient.X;
                int difY = ((MouseState)arg).Y - lastPointClient.Y;
                this.Hitbox = new Rectangle(lastPointControl.X + difX, lastPointControl.Y + difY, this.Hitbox.Width, this.Hitbox.Height);
            }
        }

        public override void MouseEventIn(object sender, object arg, int x, int y)
        {
            if (BorderStyle != Borderstyle.None)
            {
                if (new Rectangle(this.Hitbox.X, this.Hitbox.Y, this.Hitbox.Width, this.hauteurBorder).Intersects(new Rectangle(((MouseState)arg).X, ((MouseState)arg).Y, 1, 1)))
                {
                    enDeplacement = true;
                    lastPointClient = new Point(((MouseState)arg).X, ((MouseState)arg).Y);
                    lastPointControl = new Point(this.Hitbox.X, this.Hitbox.Y);
                }
            }
        }

        public override void MouseEventOut(object sender, object arg, int x, int y)
        {
            if (enDeplacement)
            {
                enDeplacement = false;
            }
        }
    }
}
