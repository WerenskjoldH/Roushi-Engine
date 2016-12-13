using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roushi
{
    public class EntityManager
    {
        private ArrayList entities = new ArrayList();
        private ArrayList entitiesToAdd = new ArrayList();
        private ArrayList entitiesToRemove = new ArrayList();
        CollisionManager collisionManager;
        public DecalManager DecalManager;
        public DrawManager DrawManager;

        int totalEntities;

        public EntityManager(DecalManager decalManager, DrawManager drawManager)
        {
            DecalManager = decalManager;
            DrawManager = drawManager;
            collisionManager = new CollisionManager(this);
        }

        public ArrayList GetAll()
        {
            return entities;
        }

        public ArrayList GetByCategory(string id)
        {
            ArrayList foundEntities = new ArrayList();
            foreach (Entity e in entities)
            {
                if (e.categoryID == id)
                    foundEntities.Add(e);

            }
            return foundEntities;
        }

        public Entity GetByUniqueID(string id) // get a specific entity by unique id ex: "player", names, etc. only one of 
        {
            foreach (Entity e in entities)
            {
                if (e.uniqueID == id)
                    return e;

            }
            return null;
        }

        public void Add(Entity entity)
        {
            entity.id = totalEntities++;
            entity.Initialize(this);
            entitiesToAdd.Add(entity);
            DrawManager.AddEntity(entity);
        }

        public void AddAll(ArrayList entityList)
        {
            foreach (Entity e in entityList)
            {
                Add(e);
            }
        }

        public void UnloadContent()
        {
            foreach (Entity e in entities)
                Remove(e);
        }

        public void Remove(Entity entity)
        {
            entity.CleanUp();
            entitiesToRemove.Add(entity);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Entity e in entities)
            {
                if (e.IsActive() && !e.IsAlive)
                    entitiesToRemove.Add(e);
            }

            while (entitiesToAdd.Count > 0)
            {
                entities.Add(entitiesToAdd[0]);
                entitiesToAdd.RemoveAt(0);
            }

            while (entitiesToRemove.Count > 0)
            {
                entities.Remove(entitiesToRemove[0]);
                Entity e = (Entity)entitiesToRemove[0];
                DrawManager.RemoveEntity(e);
                e.CallDyingEvent();
                entitiesToRemove.RemoveAt(0);
            }

            foreach (Entity e in entities)
            {
                e.Update(gameTime);
            }
            collisionManager.Update(gameTime);


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Entity e in entities)
            {
                if (e.Has<DrawablePart>())
                    e.Draw(spriteBatch);
            }
        }
    }
}
