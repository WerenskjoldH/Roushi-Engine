//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;


//namespace Roushi
//{
//    class EventBox : EntityObject
//    {

//        public EventBox() // Constructor - Instantiate all necessary ground variables 'n' stuff
//            :base()
//        {
//            velocity = Vector2.Zero;
//            entityType = EntityType.Object;
//            image = new Image();
//            image.Scale = new Vector2(2, 2);
//            image.Path = "Player/playerholder";
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

//            base.Update(gameTime);
//        }

//        protected override void Collided(Entity e)
//        {
//            if (e.entityType == EntityType.Player)
//            {
//                e.moveSpeed = 50;
//            }
//            base.Collided(e);
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            image.Draw(spriteBatch);
//        }

//    }
//}
