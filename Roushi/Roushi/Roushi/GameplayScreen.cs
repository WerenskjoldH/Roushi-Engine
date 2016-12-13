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
 **/

namespace Roushi
{
    public class GameplayScreen : Screen
    {
        //Entity player;
        //EventBox eventBox;
        EntityManager entityManager;
        DecalManager decalManager;
        DialogManager dialogManager;
        HudManager hudManager;
        DrawManager drawManager;

        //Random randy = new Random();


        public static ParticleManager<ParticleState> ParticleManager { get; private set; }

        public override void Initialize()
        {
            backgroundColor = Color.Black;
            //ScreenManager.Instance.BloomEnabled = true;
            decalManager = new DecalManager();
            hudManager = new HudManager();
            drawManager = new DrawManager();
            dialogManager = new DialogManager();
            entityManager = new EntityManager(decalManager, drawManager);
            //decalManager.AddDecal("Particles/lineparticle", new Vector2(500, 500));
        }

        public override void LoadContent()
        {
            base.LoadContent();

            dialogManager.AddDialog(new DrawableText("Press WASD To Walk and J To Interact", "bitFont", new Vector2((int)ScreenManager.Instance.Dimensions.X / 2, 500), Color.White, DialogLocation.Background));
            dialogManager.AddDialog(new DrawableText("Walk Forward to Proceed", "bitFont", new Vector2((int)ScreenManager.Instance.Dimensions.X / 2, 100), Color.White, DialogLocation.Background));
            
            entityManager.Add(EntityFactory.CreatePlayer(3, 4, 200, new Vector2(ScreenManager.Instance.Dimensions.X/2, 700), "GameObjects/templateMan", new Vector2(32, 32)));
            entityManager.Add(EntityFactory.KindSign("Wooble-dee Dooble-dee Jombulee-doodle-dee. Text Text Text Text Text.", "bitFont", new Vector2(ScreenManager.Instance.Dimensions.X / 2, ScreenManager.Instance.Dimensions.Y / 2), new Vector2(ScreenManager.Instance.Dimensions.X / 2, 700), Color.MintCream, Color.LightBlue, true, "Textbox/defaultTextbox"));
            entityManager.Add(EntityFactory.CreateSprite(new Vector2(300, 600)));
            entityManager.Update(ScreenManager.Instance.GameTime);

            hudManager.AddElement(new HealthBarElement(entityManager.GetByUniqueID("player")));
            hudManager.AddElement(new StaminaBarElement(entityManager.GetByUniqueID("player")));

            ScreenManager.Instance.Camera.Focus = entityManager.GetByUniqueID("player");

            // Dialog Overlay Be A Hud Part?
            
            //entityManager.Add(EntityFactory.PluckablePlant(new Vector2(700, 500), "gameObjects/grass", new Vector2(15, 11)));
            //entityManager.Add(EntityFactory.CreateTestSnowman(new Vector2(500, 200)));

            //entityManager.Add(EntityFactory.ChasingBaddie(new Vector2(300, 400), "gameObjects/Blocky", new Vector2(32, 32)));
            //entityManager.Add(EntityFactory.ChasingBaddie(new Vector2(500, 500), "gameObjects/Blocky", new Vector2(32, 32)));
            //entityManager.Add(EntityFactory.ChasingBaddie(new Vector2(330, 540), "gameObjects/Blocky", new Vector2(32, 32)));
            //entityManager.Add(EntityFactory.FleeingDummy(new Vector2(400, 300), "gameObjects/Blocky", new Vector2(32, 32)));
            
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            entityManager.UnloadContent();

            // Add Particle Manager Unload

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            //if (InputManager.Instance.LeftMousePressed())
            //    entityManager.Add(EntityFactory.ChasingBaddie(new Vector2(InputManager.Instance.MousePosition.X, InputManager.Instance.MousePosition.Y), "GameObjects/Blocky", new Vector2(32, 32)));

            //if (InputManager.Instance.RightMousePressed())
            //    entityManager.GetByUniqueID("target").Get<TransformPart>().GetPosition = InputManager.Instance.MousePosition;

            entityManager.Update(gameTime);
            hudManager.Update(gameTime);
            dialogManager.Update(gameTime);
            //ParticleManager.Update();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            dialogManager.DrawBackground(spriteBatch);
            decalManager.Draw(spriteBatch);
            drawManager.Draw(spriteBatch);
            dialogManager.DrawForeground(spriteBatch);
            hudManager.Draw(spriteBatch);
        }

    }
}
