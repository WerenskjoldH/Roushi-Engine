using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/**
 * By: Hunter Werenskjold
 **/

// Particle Effects!

namespace Roushi
{
    public class TitleScreen : Screen
    {
        ButtonManager buttonManager;
        Image text;
        Image begText;
        Image background;

        public TitleScreen()
        {
            // Create an instate the buttons and manager for the button
            Image startImage = new Image(4);
            startImage.Text = "Start Game";
            startImage.Position = new Vector2((int)ScreenManager.Instance.Dimensions.X / 2, (int)(ScreenManager.Instance.Dimensions.Y / 1.2f));
            startImage.Scale = new Vector2(1.5f, 1.5f);
            Button startButton;
            startButton = new Button(startImage);
            startButton.OnClick += new ButtonEventHandler(startButton_OnClick);
            startButton.OnSelection += new ButtonEventHandler(SelectionEffect);

            Image optionImage = new Image(4);
            optionImage.Text = "Options";
            optionImage.Position = new Vector2((int)ScreenManager.Instance.Dimensions.X / 2, (int)(ScreenManager.Instance.Dimensions.Y / 1.15f));
            optionImage.Scale = new Vector2(1.5f, 1.5f);
            Button optionButton = new Button(optionImage);
            optionButton.OnClick += new ButtonEventHandler(optionButton_OnClick);
            optionButton.OnSelection += new ButtonEventHandler(SelectionEffect);
            optionButton.Deactivate(); // turn off a button but it will still be visible

            Image exitImage = new Image(4);
            exitImage.Text = "Exit";
            exitImage.Position = new Vector2((int)ScreenManager.Instance.Dimensions.X / 2, (int)(ScreenManager.Instance.Dimensions.Y / 1.10f));
            exitImage.Scale = new Vector2(1.5f,1.5f);
            Button exitButton;
            exitButton = new Button(exitImage);
            exitButton.OnClick += new ButtonEventHandler(exitButton_OnClick);
            exitButton.OnSelection += new ButtonEventHandler(SelectionEffect);

            buttonManager = new ButtonManager(new Vector2(500, 500)); // instantiate the button manager and add button
            buttonManager.AddButton(startButton);
            buttonManager.AddButton(optionButton);
            buttonManager.AddButton(exitButton);

            //text = new Image(1);
            //text.Text = "Music is not owned by me! It is only a template.";
            //text.Position = new Vector2(ScreenManager.Instance.Dimensions.X/2, 10);
            //text.Color = Color.Red;

            begText = new Image(1);
            begText.Text = "Roushi Engine Development Phase - Follow Me @ http://katamarionthecosmos.tumblr.com/";
            begText.Color = Color.LightBlue;
            begText.Position = new Vector2((int)ScreenManager.Instance.Dimensions.X / 2, (int)ScreenManager.Instance.Dimensions.Y - 20);

            background = new Image(10);
            background.Path = "TitleScreen/titlescreen";
            background.TopLeftOrigin = true;
        }

        void SelectionEffect()
        {
            SoundManager.Instance.PlaySoundAtVolume("Tick", 10);
        }

        void optionButton_OnClick()
        {

        }

        void exitButton_OnClick()
        {
            ScreenManager.Instance.Exit();
        }

        void startButton_OnClick()
        {
            ScreenManager.Instance.ChangeScreens("GameplayScreen");
        }

        public override void LoadContent()
        {
            base.LoadContent();
            //SoundManager.Instance.DisableSounds = true; // This turns off ALL SOUND
            //SoundManager.Instance.PlaySoundLooped("Song - Animal Crossing Orchestra"); 
            //text.LoadContent();
            begText.LoadContent();
            background.LoadContent();

            //ScreenManager.Instance.BloomEnabled = true;
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            buttonManager.UnloadContent();
            //text.UnloadContent();
            begText.UnloadContent();
            background.UnloadContent();
            //startButton.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            buttonManager.Update(gameTime);
            begText.Update(gameTime);
            background.Update(gameTime);
            //startButton.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            background.Draw(spriteBatch);
            buttonManager.Draw(spriteBatch);
            //text.Draw(spriteBatch);
            begText.Draw(spriteBatch);
            //startButton.Draw(spriteBatch);
        }

    }
}
