using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/**
 * By: Hunter Werenskjold
 */

namespace Roushi
{
    public class SplashScreen : Screen  // First screen to load, contains logo 'n' stuff
    {
        public Image Image;
        public Image Text;

        public override void LoadContent()
        {
            base.LoadContent();
            Initialize();
        }

        void Initialize()
        {
            Image = new Image(2);
            Image.Path = "SplashScreen/logo";
            Image.Position = new Vector2(ScreenManager.Instance.Dimensions.X/2, ScreenManager.Instance.Dimensions.Y/2);
            Image.LoadContent();

            Text = new Image(1); // Just informs the player of what's going on
            Text.Text = "Press Space To Continue...";
            Text.Position = new Vector2(ScreenManager.Instance.Dimensions.X/2, ScreenManager.Instance.Dimensions.Y - 50);
            Text.Effects = "FadeEffect";
            Text.IsActive = true; // Means it should update because there are effects that effect appearance
            Text.LoadContent();
            Text.FadeEffect.FadeSpeed = .6f; // how fast will it fade in and out
            Text.FadeEffect.Increase = true; // should it start off going darker in this case
            Text.Alpha = 0; // set the image alpha to 0 ( transparent )
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            Image.UnloadContent();
            Text.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Image.Update(gameTime);
            Text.Update(gameTime);



            if (InputManager.Instance.KeyPressed(Keys.Enter, Keys.Space, Keys.J)) // If these keys are pressed, then should move onto the next screen
                ScreenManager.Instance.ChangeScreens("TitleScreen");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            Image.Draw(spriteBatch);
            Text.Draw(spriteBatch);
        }
    }
}
