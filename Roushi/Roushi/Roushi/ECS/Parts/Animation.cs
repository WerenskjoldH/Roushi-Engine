using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Roushi
{
    class Animation
    {
        // Hold y point on spritesheet
        // Hold x start and x end point
        // all units of x and y are in whole values of the given frameX and frameY. So one frameX equals 
        public string name;
        public int startFrame;
        public int endFrame;
        public Keyframe[] animation; // how long to stay on each frame
        public int yFrame;
        public float defaultFrameTime;

        public Animation(string name,int startFrame, int endFrame, int yFrame, int speed)
        {
            this.name = name;
            this.startFrame = startFrame;
            this.endFrame = endFrame;
            this.yFrame = yFrame;
            this.defaultFrameTime = speed;
            animation = new Keyframe[(endFrame+1) - startFrame];
            for (int x = 0; x < animation.Count(); x++)
            {
                animation[x] = new Keyframe();
                animation[x].time = defaultFrameTime;
            }
        }

        public void AddTimeToFrame(int frame, int time)
        {
            animation[frame].time = time;
        }

        public Keyframe GetKeyframe(int number)
        {
            Keyframe kf = animation[number];
            return kf;
        }

    }
}
