using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

/**
 * By: Hunter Werenskjold
 */

namespace Roushi
{
    public class EntityObject
    {
        public Type type;
        public string uniqueID;
        public int health;
        public Image image;
        public float moveSpeed;
        public Vector2 position
        {
            get { return image.Position; }
            set { image.Position = position; } 
        }
        public Vector2 velocity;
        public Rectangle bodyBounds;
        public EntityType entityType;
        public Boolean HasBounds;
        public Boolean IsSolid;

        public EntityObject()
        {
            Type tempType = this.GetType().UnderlyingSystemType; // get childs class name
            uniqueID = "undefined"; // change this if you need to have a name attached to the entity
            type = tempType; // Gets the childs class name
            entityType = EntityType.Object; // makes classifying entities easily
            HasBounds = true; // does it have a bound box?
            IsSolid = true; // is it solid?
        }

        public virtual void LoadContent()
        {
            // Set defaults for all variables!
            image.LoadContent();
        }

        public virtual void UnloadContent()
        {
            image.UnloadContent();
        }

        public virtual void Update(GameTime gameTime)
        {
            bodyBounds = image.CurrentBounds;
            image.Update(gameTime);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            image.Draw(spriteBatch);
        }


        public void InteractionCheck(EntityObject e)
        {

            Interact(e);     // Pass data between the two and allow them to "talk"
            CollisionUpdate(e);     // Check collisions with entity e

        }

        protected virtual void Interact(EntityObject e) // Speaks back and forth
        {
            // What it does to entity e or needs to get from entity e, position, velocity, relationship status, etc.
        }

        protected void CollisionUpdate(EntityObject e) // Updates constantly to run and see if there's any collision with all other entities and what should happen if there is
        {
            // Checks collision with other entity
            if (bodyBounds.Intersects(e.bodyBounds))
            {
                Collided(e);
            }
            else
                return; // Not necessary in the least, but looks cool and reminds me there's more than one possible way to end it all<3
        }

        protected virtual void Collided(EntityObject e) 
        {
            // All collision fun happens here to the poor entity on the other end
        }

    }
}
