using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Roushi
{
    class CrushablePart : Part
    {
        public override void Initialize()
        {
            entity.Get<CollisionPart>().Collided += new CollisionEventHandler(Crush);
        }

        private void Crush(Entity target)
        {
            if (target.categoryID != "insect")
            {
                entity.entityManager.DecalManager.AddDecal("Decals/blood", entity.Get<TransformPart>().GetPosition);
                entity.IsAlive = false;
            }
        }
    }
}
