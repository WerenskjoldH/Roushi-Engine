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

/**
 * By: Hunter Werenskjold
 **/

namespace Roushi
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Resolution.Init(ref graphics);
            Content.RootDirectory = "Content";
            // Change Virtual Resolution 
            Resolution.SetVirtualResolution(1280, 800);
            Resolution.SetResolution(1280, 800, false);
            IsMouseVisible = true;
            //graphics.IsFullScreen = false; // set to true later on
            graphics.ApplyChanges();
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth = (int)ScreenManager.Instance.Dimensions.X; // Instantiate the screen dimensions from what you put inside ofthe screenmanager one-off class
            graphics.PreferredBackBufferHeight = (int)ScreenManager.Instance.Dimensions.Y;
            //IsFixedTimeStep = false;
            graphics.ApplyChanges();
            ScreenManager.Instance.Initialize(this);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ScreenManager.Instance.GraphicsDevice = GraphicsDevice;
            ScreenManager.Instance.SpriteBatch = spriteBatch;
            ScreenManager.Instance.LoadContent(Content);
            SoundManager.Instance.LoadContent();
        }

        protected override void UnloadContent()
        {
            ScreenManager.Instance.UnloadContent(); // Unload content of screenmanager instance
            SoundManager.Instance.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape) && Keyboard.GetState().IsKeyDown(Keys.OemTilde))
                this.Exit();

            SoundManager.Instance.Update(gameTime); // Sounds need to execute before the game updates as to keep conflictions in the DimAndStopSounds method
            ScreenManager.Instance.Update(gameTime); // update screenmanager singleton

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ScreenManager.Instance.BackgroundColor);
            ScreenManager.Instance.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
