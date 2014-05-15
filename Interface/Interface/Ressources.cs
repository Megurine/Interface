using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Interface.GUI;

namespace Interface
{
    class Ressources
    {
        public static int tailleFenetreX = 1100;
        public static int tailleFenetreY = 600;
        public static int tailleTitreFenetre = 26;
        public static int indexUiForm = 0;

        public static SpriteFont Font;
        public static SpriteFont FontBitmap;

        public static bool isActive;
        public static bool enFenetre;
        public static int idFenetreFocused;
        public static int idFenetre1erPlan;

        public static void LoadContent(ContentManager content)
        {
            Font = content.Load<SpriteFont>("data/font");
            FontBitmap = content.Load<SpriteFont>("data/bitmapfont");
        }

        public static bool verifierEnFenetre(Rectangle clientBounds, MouseState Mouse)
        {
            if (new Rectangle(clientBounds.X, clientBounds.Y, Ressources.tailleFenetreX, Ressources.tailleFenetreY).Intersects(new Rectangle(clientBounds.X + Mouse.X, clientBounds.Y + Mouse.Y, 1, 1)))
            {
                Ressources.enFenetre = true;
            }
            else
            {
                Ressources.enFenetre = false;
            }
            return Ressources.enFenetre;
        }

        public static bool activeEtFenetre()
        {
            if (isActive && enFenetre)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int indexationUiForm()
        {
            indexUiForm++;
            return indexUiForm;
        }

        public static void bordureTexte(SpriteBatch spriteBatch, SpriteFont spriteFont, string textTest, Vector2 vector2, float Opacity, Color color1, Color color2)
        {
            spriteBatch.DrawString(spriteFont, textTest, new Vector2(vector2.X - 1, vector2.Y - 1), color1 * Opacity);
            spriteBatch.DrawString(spriteFont, textTest, new Vector2(vector2.X - 1, vector2.Y), color1 * Opacity);
            spriteBatch.DrawString(spriteFont, textTest, new Vector2(vector2.X - 1, vector2.Y + 1), color1 * Opacity);
            spriteBatch.DrawString(spriteFont, textTest, new Vector2(vector2.X, vector2.Y - 1), color1 * Opacity);
            spriteBatch.DrawString(spriteFont, textTest, new Vector2(vector2.X, vector2.Y + 1), color1 * Opacity);
            spriteBatch.DrawString(spriteFont, textTest, new Vector2(vector2.X + 1, vector2.Y - 1), color1 * Opacity);
            spriteBatch.DrawString(spriteFont, textTest, new Vector2(vector2.X + 1, vector2.Y), color1 * Opacity);
            spriteBatch.DrawString(spriteFont, textTest, new Vector2(vector2.X + 1, vector2.Y + 1), color1 * Opacity);

            spriteBatch.DrawString(spriteFont, textTest, new Vector2(vector2.X, vector2.Y), color2 * Opacity);
        }

        public static void faireAction(uiButton parent)
        {
            switch (parent.idAction)
            {
                case 1:
                    Console.WriteLine("Action 1");
                    break;
            }
        }
    }
}
