using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections;

namespace Roushi
{
    public delegate void CollisionEventHandler(Entity target);

    class CollisionPart : Part
    {
        public event CollisionEventHandler Collided;
        public event CollisionEventHandler Colliding;
        public event CollisionEventHandler EndedCollision;

        Boolean overrideCollisionBox;
        public Rectangle overridenCollisionBox;
        Rectangle offsetCollisionBox;

        public ArrayList CollidingWith;
        private ArrayList prevCollidingWith;

        public void OverrideCollisionBox(Rectangle rectangle)
        {
            overrideCollisionBox = true;
            overridenCollisionBox = rectangle;
        }

        public void OffsetCollisionBox(Rectangle rectangle)
        {
            offsetCollisionBox = rectangle;
        }

        public override void Initialize()
        {
            CollidingWith = new ArrayList();
            prevCollidingWith = new ArrayList();
            entity.Dying += new EntityDyingEventHandler(EndAllCollisions);
        }

        public Rectangle GetBoundingBox()
        {
            if (overrideCollisionBox)
                return overridenCollisionBox;
            else
                if (entity.Has<DrawablePart>())
                {
                    Rectangle rectangle = entity.Get<DrawablePart>().GetBoundingBox();
                    rectangle.X += offsetCollisionBox.X;
                    rectangle.Y += offsetCollisionBox.Y;
                    rectangle.Width += offsetCollisionBox.Width;
                    rectangle.Height += offsetCollisionBox.Height;
                    return rectangle;
                }

            return Rectangle.Empty;
        }

        public void Collision(Entity target)
        {
            if (CheckArrayForEntity(prevCollidingWith, target))
            {
                if (Colliding != null)
                    Colliding(target);
            }
            else
            {
                CollidingWith.Add(target);
                if (Collided != null)
                    Collided(target);
            }

            prevCollidingWith = CollidingWith; // incase we do any dynamic event removal we want this to equal zero so everything is unloaded or at least set to 0
        }

        public Boolean IsCollidingWithEntity(string name) // Finds entity by unique name
        {
            foreach (Entity temp in CollidingWith)
            {
                if (temp.uniqueID == name)
                    return true;
            }

            return false;
        }

        public Entity GetCollidingEntity(string name) // Finds entity by unique name
        {
            foreach (Entity temp in CollidingWith)
            {
                if (temp.uniqueID == name)
                    return temp;
            }

            throw new InvalidOperationException("Entity of name: " + name + " could not be found.");
        }

        public void EndAllCollisions() // when entity becomes not alive this should trigger
        {
            if (CollidingWith.Count > 0)
            {
                foreach (Entity e in CollidingWith)
                {
                    EndCollision(e);
                }
            }
        }

        public void EndCollision(Entity target)
        {
            if(EndedCollision != null)
                EndedCollision(target);
        }

        public static Boolean CheckArrayForEntity(ArrayList array, Entity e) // searches array for 
        {
            foreach (Entity temp in array)
            {
                if (temp.id == e.id)
                    return true;
            }
            return false;
        }

    }
}
