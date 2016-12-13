using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

/**
 * By: Hunter Werenskjold
 **/

namespace Roushi
{
    public enum GameStyle
    {
        TopDown,
        TwoThirds,
        SideView
    }

    public class ScreenManager
    {
        private static ScreenManager instance;
        public Vector2 Dimensions { private set; get; }
        public ContentManager Content { private set; get; }

        public Camera2D Camera;

        BloomComponent bloom;

        GameStyle gameStyle;

        Color backgroundColor;

        FrameCounter frameCounter;
        SpriteFont bitFont;
        bool showFPS;

        Screen currentScreen, newScreen;
        public GraphicsDevice GraphicsDevice;
        public SpriteBatch SpriteBatch;
        public GameTime GameTime;

        public Boolean TransitionBeginning;

        public string CurrentScreenTransitionState;

        public GameStyle GetGameStyle // Go back and incorporate this into the engine at some point will only change how drawing is done
        {
            get { return gameStyle; }
            set { gameStyle = value; }
        }

        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }

        public Boolean BloomEnabled
        {
            get { return bloom.Visible; }
            set { bloom.Visible = value; }
        }

        Game1 game;

        public Image Image;
        public bool IsTransitioning { get; private set; }

        public static ScreenManager Instance // Pretty much the essential bit that makes this a singleton class
        {
            get
            {
                if (instance == null) // if the instance doesn't exist then create it
                {
                    instance = new ScreenManager();
                }

                return instance; // returns itself
            }
        }

        private ScreenManager()
        {
            Dimensions = new Vector2(1280, 800); // Set screen resolution

            Image = new Image(0f);
            Image.Path = "ScreenManager/BlackOverlay"; // what the overlay image will be 1x1px
            Image.Scale = new Vector2(Dimensions.X, Dimensions.Y); // the size of the overlay will be scaled by the size of the screen.
            Image.Effects = "FadeEffect"; // add the fade effect to the image

            gameStyle = GameStyle.TopDown;

            showFPS = false;

            backgroundColor = Color.Black;

            currentScreen = new SplashScreen(); // starting screen
        }

        public void ChangeScreens(string screenName)
        {
            SoundManager.Instance.DimAndStopSound();
            newScreen = (Screen)Activator.CreateInstance(Type.GetType("Roushi." + screenName)); // use the activator to create an instance of a class unknown at runtime

            Image.IsActive = true; // the image will update
            Image.FadeEffect.Increase = true; // the fade effect will begin to darken
            Image.Alpha = 0.0f; // make sure the image is transparent

            TransitionBeginning = true; // begins the transition

            IsTransitioning = true; // while there's a transition
        }

        void Transition(GameTime gameTime)
        {
            if (IsTransitioning) // while in transition
            {
                if (TransitionBeginning) // as soon as the transition is done "beginning" it's no longer beginning so like let's stop that 
                { TransitionBeginning = false;}

                Image.Update(gameTime); // update the image that shows during the transition
                if (Image.Alpha == 1.0f) // if the image is fully loaded in and is in no way transparent
                {
                    BloomEnabled = false;
                    currentScreen.UnloadContent(); // unload the old screen
                    currentScreen = newScreen; // load new screen
                    currentScreen.LoadContent(); // load the new screens content
                }
                else if (Image.Alpha == 0.0f) // if the image is back to being completely transparent then deactivate the image, stop transition bool, and bail out of the method
                {
                    Image.IsActive = false;
                    IsTransitioning = false;
                    return;
                }
            }
        }

        public void Initialize(Game1 Game)
        {
            game = Game;
            Camera = new Camera2D(Game);
            Camera.Initialize();
            bloom = new BloomComponent(Game);
            Game.Components.Add(bloom);
            bloom.Settings = new BloomSettings(null, .25f, 2, 2, 1, 1.5f, 1);
            BloomEnabled = false;
        }

        public void Exit() // Closes the game through a reference to the Game1 class 
        {
            game.Exit();
        }

        public void LoadContent(ContentManager content) 
        {
            this.Content = new ContentManager(content.ServiceProvider, "Content"); // create new content pipeline
            currentScreen.LoadContent(); // load the current screens content
            Image.LoadContent(); // load overlay's content

            bitFont = Content.Load<SpriteFont>("Fonts/BitFont");
            frameCounter = new FrameCounter();
        }

        public void UnloadContent() // unload everything
        {
            currentScreen.UnloadContent();
            SoundManager.Instance.UnloadContent();
            Image.UnloadContent();
        }

        public void Update(GameTime gameTime) 
        {
            GameTime = gameTime;
            currentScreen.Update(gameTime);
            Transition(gameTime);
            if(Camera.Focus != null)
                Camera.Update(gameTime);

            if (InputManager.Instance.KeyPressed(Keys.F1))
            {
                if (showFPS == false)
                    showFPS = true;
                else
                    showFPS = false;
            }

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            frameCounter.Update(deltaTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Resolution.BeginDraw();

            if(Camera.Focus == null)
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Resolution.getTransformationMatrix());
            else
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Resolution.getTransformationMatrix() * Camera.Transform);
            
            bloom.BeginDraw();

            currentScreen.Draw(spriteBatch);
            if (IsTransitioning)
                Image.Draw(spriteBatch);

            var fps = string.Format("FPS: {0}", frameCounter.AverageFramesPerSecond);
            if(showFPS)
                spriteBatch.DrawString(bitFont, fps, new Vector2(1, 1), Color.DeepSkyBlue);

            spriteBatch.End();
        }
    }
}
