//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;

///**
// * By: Hunter Werenskjold
// */

//namespace Roushi
//{
//    public class Player : EntityObject
//    {

//        /*
//         * TODO:
//         * Add acceleration as a factor 
//         */

//        public Player() // Constructor - Instantiate all necessary ground variables 'n' stuff
//            :base()
//        {
//            velocity = Vector2.Zero;
//            entityType = EntityType.Player;
//            uniqueID = "player";
//            image = new Image();
//            image.Scale = new Vector2(2, 2);
//            image.Path = "Player/playerholder";
//            image.Effects = "SpriteSheetEffect";
//            moveSpeed = 200;
//        }

//        public override void LoadContent()
//        {
//            base.LoadContent();

//        }

//        public override void UnloadContent()
//        {
//            base.UnloadContent();

//        }

//        public override void Update(GameTime gameTime)
//        {
//            image.IsActive = true; // Allow the image to update

//            if (InputManager.Instance.KeyDown(Keys.W)) 
//            {
//                // If w is down then move y as a function of time * movement speed. "d = v*t" SO, it keeps the game in step with time. So for how much time has elapsed between this
//                // frame and the last it will get how much the player will have moved and thus keep the character moving properly regardless of stutters. 
//                // With that gotten distance the player should move it's added to velocity and then velocity is added to position and the cycle repeats
//                velocity.Y = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds; 
//                image.SpriteSheetEffect.CurrentFrame.Y = 3;
//            }
//            else if (InputManager.Instance.KeyDown(Keys.S))
//            {
//                velocity.Y = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
//                image.SpriteSheetEffect.CurrentFrame.Y = 0;
//            }
//            else
//                velocity.Y = 0;

//            if (InputManager.Instance.KeyDown(Keys.D))
//            {
//                velocity.X = moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
//                image.SpriteSheetEffect.CurrentFrame.Y = 2;
//            }
//            else if (InputManager.Instance.KeyDown(Keys.A))
//            {
//                velocity.X = -moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
//                image.SpriteSheetEffect.CurrentFrame.Y = 1;
//            }
//            else
//                velocity.X = 0;

//            if (velocity.X == 0 && velocity.Y == 0) // Stop animation
//            {
//                image.IsActive = false;
//            }

//            base.Update(gameTime);
//            image.Position += velocity;
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            image.Draw(spriteBatch);
//        }
//    }
//}
