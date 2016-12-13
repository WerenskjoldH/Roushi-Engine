using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;

namespace Roushi
{
    class CollisionManager
    {
        EntityManager entityManager;
        Boolean perPixelCollision;

        public CollisionManager(EntityManager EntityManager)
        {
            entityManager = EntityManager;
        }

        public Boolean PerPixelCollision // Toggle on and off PerPixelCollisions
        {
            get { return perPixelCollision;  }
            set { perPixelCollision = value; }
        }

        public void Update(GameTime gameTime)
        {
            ArrayList entities = entityManager.GetAll();

            for (int x = 0; x < entities.Count; x++)
            {

                Entity self = (Entity)entities[x];
                if (!self.Has<CollisionPart>())
                    return;

                if (x + 1 < entities.Count)
                {
                    for (int y = x + 1; y < entities.Count; y++)
                    {
                        Entity target = (Entity)entities[y];
                        if (!target.Has<CollisionPart>())
                            return;

                        if (CheckCollision(self, target))
                        {
                            self.Get<CollisionPart>().Collision(target);
                            target.Get<CollisionPart>().Collision(self);
                        }
                        else if(CollisionPart.CheckArrayForEntity(target.Get<CollisionPart>().CollidingWith, self))
                        {
                            target.Get<CollisionPart>().EndCollision(self);
                            target.Get<CollisionPart>().CollidingWith.Remove(self);
                        }
                    }
                }
            }
        }

        // Add Support For Rotated Entities
        private Boolean CheckCollision(Entity self, Entity target)
        {
            Rectangle selfBounds = self.Get<CollisionPart>().GetBoundingBox();
            Rectangle targetBounds = target.Get<CollisionPart>().GetBoundingBox();
            //if (ScreenManager.Instance.GetGameStyle == GameStyle.TopDown)
            //{
            //    if (selfBounds.Intersects(targetBounds))
            //        return true;
            //    else
            //        return false;
            //}
            //else if (ScreenManager.Instance.GetGameStyle == GameStyle.SideView)
            //{
            //    if (selfBounds.Intersects(targetBounds))
            //        return true;
            //    else
            //        return false;
            //}
            //else if (ScreenManager.Instance.GetGameStyle == GameStyle.TwoThirds)
            //{
            //    if (selfBounds.Intersects(targetBounds))
            //        return true;
            //    else
            //        return false;
            //}
            //else
            //{
                if (selfBounds.Intersects(targetBounds))
                    return true;
                else
                    return false;
            //}

        }
    }
}
