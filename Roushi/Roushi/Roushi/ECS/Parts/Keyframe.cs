using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roushi
{

    public delegate void KeyframeEvent();

    class Keyframe
    {
        public float time;

        public event KeyframeEvent keyframeEvent;

        public void Trigger()
        {
            if(keyframeEvent != null)
                keyframeEvent(); // why is this ALWAYS NULL despite being assigned delegates! lkashjdfoikwj;erf;oiqwjer
        }
    }
}
