using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Interface.GUI
{
    class uiControl
    {
        public enum BackgroundImagemode { Etirage, Normal, WindowsTile };
        public enum Borderstyle { None , Vide, Croix, Sizable, SizableCroix };

        public Color BackgroundColor;
        public Texture2D BackgroundImage;
        public BackgroundImagemode BackgroundImageMode;
        public bool TextureTitre;
        public bool Enabled;
        public float Opacity;
        public Rectangle Hitbox;
        public bool Visible;
        public string Name;

        public List<uiControl> Controls;
        public uiControl Parent;
        public int id;

        public int hauteurBorder;
        public bool onBarreTitre;

        public string textTest;

        public bool MouseDessus;
        public bool MouseClick;
        public bool LastMouseDessus;
        public bool LastMouseClick;

        public uiControl()
        {
            BackgroundColor = Color.White;
            BackgroundImageMode = BackgroundImagemode.Normal;
            TextureTitre = false;
            Enabled = true;
            Opacity = 1;
            Hitbox = new Rectangle(0, 0, 100, 100);
            Name = ".";
            Parent = null;
            initialize();
        }

        public uiControl(Color backgroundColor, Texture2D texture2D, float opacity, Rectangle hitbox, string name, uiControl parent, bool textureTitre, BackgroundImagemode backgroundImageMode)
        {
            BackgroundColor = backgroundColor;
            BackgroundImage = texture2D;
            BackgroundImageMode = backgroundImageMode;
            TextureTitre = textureTitre;
            Opacity = opacity;
            Hitbox = hitbox;
            Name = name;
            Parent = parent;
            initialize();
        }

        private void initialize()
        {
            id = 0;
            if (Parent != null)
            {
                id = Parent.id;
            }
            textTest = "";
            hauteurBorder = 0;
            onBarreTitre = false;
            Controls = new List<uiControl>();
            Enabled = true;
            Visible = true;
        }

        virtual public void Update(MouseState Mouse, KeyboardState keyboard, GameTime gametime, int x, int y)
        {
            LastMouseClick = MouseClick;
            LastMouseDessus = MouseDessus;

            if (new Rectangle(Hitbox.X + x, Hitbox.Y + y, Hitbox.Width, Hitbox.Height + hauteurBorder).Intersects(new Rectangle(Mouse.X, Mouse.Y, 1, 1)))
            {
                MouseDessus = true;
                if (new Rectangle(Hitbox.X + x, Hitbox.Y + y, Hitbox.Width, hauteurBorder).Intersects(new Rectangle(Mouse.X, Mouse.Y, 1, 1)))
                {
                    onBarreTitre = true;
                }
                else
                {
                    onBarreTitre = false;
                }
            }
            else
            {
                MouseDessus = false;
            }
            if (MouseDessus && Mouse.LeftButton == ButtonState.Pressed) 
            {
                MouseClick = true; 
            }
            else
            {
                if (LastMouseClick)
                {
                    if (Mouse.LeftButton == ButtonState.Pressed)
                    {
                        MouseClick = true; 
                    }
                    else
                    {
                        MouseClick = false;
                    }
                }
                else 
                {
                    MouseClick = false; 
                }
            }
            if (Ressources.idFenetre1erPlan == 0 || Ressources.idFenetre1erPlan == this.id)
            {
                if (LastMouseDessus && MouseDessus && Ressources.activeEtFenetre())
                {
                    Ressources.idFenetre1erPlan = id;
                    this.MouseEventMove(this, Mouse, x, y);
                }
                if (!LastMouseDessus && MouseDessus && Ressources.activeEtFenetre())
                {
                    Ressources.idFenetre1erPlan = id;
                    Console.WriteLine(this.Name + " - " + this.GetType().ToString());
                    this.MouseEventEnter(this, Mouse, x, y);
                }
                if (LastMouseDessus && !MouseDessus && Ressources.activeEtFenetre())
                {
                    Ressources.idFenetre1erPlan = 0;
                    this.MouseEventLeave(this, Mouse, x, y);
                }
            }
            if (Ressources.idFenetreFocused == 0 || Ressources.idFenetreFocused == this.id)
            {
                if (!LastMouseClick && MouseClick && Ressources.activeEtFenetre())
                {
                    Ressources.idFenetreFocused = id;
                    this.MouseEventIn(this, Mouse, x, y);
                }
                if (LastMouseClick && !MouseClick)
                {
                    this.MouseEventOut(this, Mouse, x, y);
                }
            }

            foreach (uiControl control in Controls)
            {
                control.Update(Mouse, keyboard, gametime, Hitbox.X, Hitbox.Y + hauteurBorder);
            }

        }

        virtual public void Draw(SpriteBatch spriteBatch, int x, int y)
        {
            if (BackgroundColor != null && BackgroundColor != Color.Transparent)
            {
                Texture2D t = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                t.SetData(new Color[] { Color.White});
                spriteBatch.Draw(t, new Rectangle(Hitbox.X + x, Hitbox.Y + y + hauteurBorder, Hitbox.Width, Hitbox.Height), Color.White * Opacity);
            }

            if (BackgroundImage != null)
            {
                int hauteurPos = hauteurBorder;
                int hauteurTaille = 0;
                if (TextureTitre)
                {
                    hauteurPos = 0;
                    hauteurTaille = hauteurBorder;
                }
                Rectangle size = new Rectangle(Hitbox.X + x, Hitbox.Y + y + hauteurPos, Hitbox.Width, Hitbox.Height + hauteurTaille);
                switch(BackgroundImageMode)
                {
                    case BackgroundImagemode.Etirage:
                        spriteBatch.Draw(BackgroundImage, size, Color.White * Opacity);
                        break;
                    case BackgroundImagemode.Normal:
                        spriteBatch.Draw(BackgroundImage, new Rectangle(Hitbox.X, Hitbox.Y + hauteurPos, BackgroundImage.Width, BackgroundImage.Height), Color.White * Opacity);
                        break;
                    case BackgroundImagemode.WindowsTile:
                        Point bloc = new Point(BackgroundImage.Width / 3, BackgroundImage.Height / 3);

                        spriteBatch.Draw(BackgroundImage, new Rectangle(size.X + bloc.X, size.Y + bloc.Y, size.Width - (2 * bloc.X), size.Height - (2 * bloc.Y)), new Rectangle(bloc.X, bloc.Y, bloc.X, bloc.Y), Color.White * Opacity);//MIDDLE MIDDLE
                        spriteBatch.Draw(BackgroundImage, new Rectangle(size.X, size.Y + bloc.Y, bloc.X, size.Height - (2 * bloc.Y)), new Rectangle(0, bloc.Y, bloc.X, bloc.Y), Color.White * Opacity);//MIDDLE GAUCHE
                        spriteBatch.Draw(BackgroundImage, new Rectangle(size.X + size.Width - bloc.X, size.Y + bloc.Y, bloc.X, size.Height - (2 * bloc.Y)), new Rectangle(2 * bloc.X, bloc.Y, bloc.X, bloc.Y), Color.White * Opacity);//MIDDLE DROITE
                        spriteBatch.Draw(BackgroundImage, new Rectangle(size.X + bloc.X, size.Y, size.Width - (2*bloc.X), bloc.Y), new Rectangle(bloc.X, 0, bloc.X, bloc.Y), Color.White * Opacity);//TOP MIDDLE
                        spriteBatch.Draw(BackgroundImage, new Rectangle(size.X + bloc.X, size.Y + size.Height - bloc.Y, size.Width - (2 * bloc.X), bloc.Y), new Rectangle(bloc.X, 2 * bloc.Y, bloc.X, bloc.Y), Color.White * Opacity);//BOT MIDDLE
                        spriteBatch.Draw(BackgroundImage, new Rectangle(size.X, size.Y, bloc.X, bloc.Y), new Rectangle(0, 0, bloc.X, bloc.Y), Color.White * Opacity);//TOP GAUCHE
                        spriteBatch.Draw(BackgroundImage, new Rectangle(size.X + size.Width - bloc.X, size.Y, bloc.X, bloc.Y),new Rectangle(2 * bloc.X, 0, bloc.X, bloc.Y), Color.White * Opacity);//TOP DROITE
                        spriteBatch.Draw(BackgroundImage, new Rectangle(size.X, size.Y + size.Height - bloc.Y, bloc.X, bloc.Y), new Rectangle(0, 2 * bloc.Y, bloc.X, bloc.Y), Color.White * Opacity);//BOT GAUCHE
                        spriteBatch.Draw(BackgroundImage, new Rectangle(size.X + size.Width - bloc.X, size.Y + size.Height - bloc.Y, bloc.X, bloc.Y), new Rectangle(2 * bloc.X, 2 * bloc.Y, bloc.X, bloc.Y), Color.White * Opacity);//BOT DROITE
                        break;
                }
            }

            foreach (uiControl control in Controls)
            {
                control.Draw(spriteBatch, this.Hitbox.X + x, this.Hitbox.Y + y + hauteurBorder);
            }

            //textTest = "Name : " + Name + "\nMouseHover : " + MouseDessus.ToString() + "\nMouseClick : " + MouseClick.ToString();

            Ressources.bordureTexte(spriteBatch, Ressources.FontBitmap, textTest, new Vector2(Hitbox.X + 10, Hitbox.Y + hauteurBorder), Opacity, new Color((byte)(106), (byte)(201), (byte)(242)), new Color((byte)(255), (byte)(255), (byte)(255)));
        }

        virtual public void MouseEventMove(object sender, object arg, int x, int y)
        {
            
        }
        virtual public void MouseEventEnter(object sender, object arg, int x, int y)
        {

        }
        virtual public void MouseEventLeave(object sender, object arg, int x, int y)
        {

        }
        virtual public void MouseEventIn(object sender, object arg, int x, int y)
        {

        }
        virtual public void MouseEventOut(object sender, object arg, int x, int y)
        {

        }


    }
}
