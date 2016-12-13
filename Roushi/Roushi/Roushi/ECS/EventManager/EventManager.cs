using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roushi
{
    class EntityCreatedEventArgs : EventArgs
    {
        public Entity Entity { get; set; }
    }

    class EventManager
    {
        public event EventHandler<EntityCreatedEventArgs> EntityCreate;

        private void FireCreateEvent(Entity entity)
        {
            //if (this.EntityCreate)
            //{
            //    this.EntityCreate(this, new EntityCreatedEventArgs { Entity = entity });
            //}

        }

    }
}
