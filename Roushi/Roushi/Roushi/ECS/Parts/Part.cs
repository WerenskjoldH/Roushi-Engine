using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roushi
{
    public abstract class Part
    {
        private Boolean isActive = true;
        protected Entity entity;

        // If the part should be updated
        public Boolean IsActive() // The first three functions should be "public sealed returnType name"
        {
            return isActive;
        }
        
        // The entity this part is attached to
        public Entity GetAttachedEntity()
        {
            return entity;
        }

        // Set the entity this part is attached to
        public void SetEntity(Entity inEntity)
        {
            entity = inEntity;
        }

        // What happens when the part initializes
        public virtual void Initialize() {  }

        // What happens when the part cleans up
        public virtual void CleanUp() {        }

        // Update logic
        public virtual void Update(GameTime gameTime)
        {       }

        public virtual void Draw(SpriteBatch spriteBatch)
        {       }
        
    }
}
