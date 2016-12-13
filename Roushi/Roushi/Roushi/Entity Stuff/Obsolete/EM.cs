//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;

///**
// * By: Hunter Werenskjold
// */

//namespace Roushi
//{
//    class EM
//    {
//        List<Entity> entities;

//        public List<Entity> Entities
//        {
//            get { return entities; }
//        }

//        public void LoadContent() // load the content for the entity
//        {
//            entities = new List<Entity>();
//        }

//        public void UnloadContent()
//        {
//            for (int i = 0; i < entities.Count; i++)
//                entities[i].UnloadContent();
//        }

//        public void Update(GameTime gameTime)
//        {
//            for (int i = 0; i < entities.Count; i++) // update each entity
//                entities[i].Update(gameTime);

//            for (int x = 0; x < entities.Count; x++ ) // check collision between each entity
//            {
//                Entity e = entities[x];
//                if (e.HasBounds && e.IsSolid || !e.entityType.Equals(EntityType.ScreenItem))
//                    for (int y = 0; y < entities.Count; y++) // check collision between each entity
//                    {
//                        if (x != y)
//                        {
//                            Entity i = entities[y];
//                            if (i.HasBounds && i.IsSolid || !i.entityType.Equals(EntityType.ScreenItem))
//                                e.InteractionCheck(i); // check entity e vs i
//                        }
//                    }
//            }
//        }

//        public void Draw(SpriteBatch spriteBatch)
//        {
//            for (int i = 0; i < entities.Count; i++) // draws each entity
//                entities[i].Draw(spriteBatch);

//        }

//        T CreateType<T>() where T : new()
//        {
//            return new T();
//        }

//        public void AddEntity(Entity entity) // add entity :|
//        {
//            //entity = (Entity)Activator.CreateInstance<Entity>();
//            entity.LoadContent();
//            entities.Add(entity);
//        }
//    }
//}
