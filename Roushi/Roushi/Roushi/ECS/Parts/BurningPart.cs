using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Roushi
{
    class BurningPart : Part
    {
        public override void Initialize()
        {
            entity.Get<CollisionPart>().Collided += new CollisionEventHandler(Burn);
            entity.Get<CollisionPart>().EndedCollision += new CollisionEventHandler(EndBurn);
        }

        void EndBurn(Entity target)
        {
            target.Get<DrawablePart>().Color = target.Get<DrawablePart>().DefaultColor;
        }

        private void Burn(Entity target)
        {
            target.Get<DrawablePart>().Color = Color.MediumPurple;

            entity.IsAlive = false;
        }

    }
}
