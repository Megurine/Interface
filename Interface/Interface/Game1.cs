using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Interface.GUI;
using System.Collections;

namespace Interface
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<uiControl> Controls;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Controls = new List<uiControl>();
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Ressources.LoadContent(Content);
            uiControl uiC = new uiForm(Color.Transparent, Content.Load<Texture2D>("data/GUI/windowsTile2"), 1f, new Rectangle(220, 52, 600, 380), "Fenêtre de test 1", "Fenêtre de test 1", null, Interface.GUI.uiControl.Borderstyle.Sizable, true, Interface.GUI.uiControl.BackgroundImagemode.WindowsTile);
            uiC.Controls.Add(new uiPictureBox(Content.Load<Texture2D>("data/GUI/rougeG"), 1f, new Rectangle(8, 0, 584, 372), uiC, uiControl.BackgroundImagemode.Etirage));
            uiC.Controls.Add(new uiLabel(1f, new Rectangle(417, 350, 0, 0), "String de text #yolo", uiC, true, Color.White, Color.Red));
            uiC.Controls.Add(new uiButton(Content.Load<Texture2D>("data/GUI/windowsTile2"), 1f, new Rectangle(50, 50, 100, 30), "hey", 1, uiC, Content.Load<Texture2D>("data/GUI/windowsTile1"), Content.Load<Texture2D>("data/GUI/rougeG"), Color.Red, Color.White, Interface.GUI.uiControl.BackgroundImagemode.WindowsTile));
            Controls.Add(uiC);
            uiC = new uiForm(Color.Red, Content.Load<Texture2D>("data/GUI/rougeG"), 1f, new Rectangle(9, 54, 200, 100), "Fenêtre de test 2", "Fenêtre de test 2", null, Interface.GUI.uiControl.Borderstyle.Sizable, false, Interface.GUI.uiControl.BackgroundImagemode.Etirage);
            Controls.Add(uiC);
            uiC = new uiForm(Color.Red, Content.Load<Texture2D>("data/GUI/windowsTile2"), 1f, new Rectangle(10, 193, 200, 100), "Fenêtre de test 3", "Fenêtre de test 3", null, Interface.GUI.uiControl.Borderstyle.Sizable, true, Interface.GUI.uiControl.BackgroundImagemode.WindowsTile);
            Controls.Add(uiC);
            uiC = new uiForm(Color.Red, null, 1f, new Rectangle(10, 330, 200, 100), "Fenêtre de test 4", "Fenêtre de test 4", null, Interface.GUI.uiControl.Borderstyle.Sizable, false, Interface.GUI.uiControl.BackgroundImagemode.Etirage);
            Controls.Add(uiC);
            Controls.Sort((t1, t2) => t1.id - t2.id);
        }
        
        protected override void UnloadContent()
        {
            Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            Ressources.isActive = IsActive;
            Ressources.verifierEnFenetre(Window.ClientBounds, Mouse.GetState());
            Ressources.idFenetreFocused = 0;
            Ressources.idFenetre1erPlan = 0;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            foreach (uiControl control in Controls)
            {
                control.Update(Mouse.GetState(), Keyboard.GetState(), gameTime, 0 , 0);
            }

            if (Ressources.idFenetreFocused != 0)
            {
                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i].id == Ressources.idFenetreFocused)
                    {
                        uiControl uiC = Controls[i];
                        Controls.RemoveAt(i);
                        Controls.Insert(0, uiC);
                    }
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            this.graphics.PreferredBackBufferWidth = Ressources.tailleFenetreX;
            this.graphics.PreferredBackBufferHeight = Ressources.tailleFenetreY;
            this.graphics.ApplyChanges();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.GraphicsDevice.SetRenderTarget(null);
            spriteBatch.GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

            string testtext = "";

            for (int i = Controls.Count - 1; i >= 0 ; i--)
            {
                Controls[i].Draw(spriteBatch, 0, 0);
                testtext += "*" + Controls[i].id + " - " + Controls[i].Name + "\n";
            }

            Ressources.bordureTexte(spriteBatch, Ressources.FontBitmap, testtext, new Vector2(3, 1), 1f, new Color((byte)(200), (byte)(0), (byte)(0)), new Color((byte)(255), (byte)(255), (byte)(255)));
            Ressources.bordureTexte(spriteBatch, Ressources.FontBitmap, "Index form : " + Ressources.idFenetreFocused.ToString() + "\nIndex Hover : " + Ressources.idFenetre1erPlan.ToString(), new Vector2(988, 1), 1f, new Color((byte)(200), (byte)(0), (byte)(0)), new Color((byte)(255), (byte)(255), (byte)(255)));
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
