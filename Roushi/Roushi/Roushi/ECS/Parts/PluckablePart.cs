using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace Roushi
{
    class PluckablePart : Part
    {
        public override void Initialize()
        {
            entity.Get<CollisionPart>().Colliding += new CollisionEventHandler(PluckablePart_Colliding);

            Keyframe kf = entity.Get<AnimationPart>().GetAnimationKeyframe("plucked", 3);
            kf.keyframeEvent += new KeyframeEvent(DummyControllerPart_keyframeEvent);
        }

        void DummyControllerPart_keyframeEvent()
        {
            entity.IsAlive = false;
        }

        void PluckablePart_Colliding(Entity target)
        {
            if (InputManager.Instance.KeyDown(Keys.J) && target.uniqueID == "player")
            {
                entity.Get<AnimationPart>().PlayAnimationIfClear("plucked", false);
            }
        }


    }
}
