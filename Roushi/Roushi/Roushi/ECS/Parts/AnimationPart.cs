using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Roushi
{
    class AnimationPart : Part
    {
        //        Animation Part:
        //- Dictionary to hold Animations
        //- Create Animation
        //  +start x, end x
        //  +y location on spritesheet
        //  +animation speed
        //  +hold time on certain frames
        //- Play Animation
        //  +play through whole animation
        //  +
        //- Cancel Animation
        //- Freeze Animation(int timeToFreeze)
        //- GetFrame

        Dictionary<string, Animation> Animations = new Dictionary<string, Animation>();
        Animation currentAnimation;
        Boolean isActive;
        Boolean isPlaying;
        Boolean isLooping;
        Boolean isFrozen;
        int freezeTime;
        public int animationSpeed;

        public Boolean IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        Vector2 frameSize;
        public AnimationPart()
        {
            frameCounter = 0; frame = 0;
            animationSpeed = 100;
            isActive = true;
            isPlaying = false;
            isFrozen = false;
        }


        public void CreateAnimation(Animation animation)
        {
            Animations.Add(animation.name, animation);
        }

        public void PlayAnimation(string name, Boolean looping)
        {
            currentAnimation = Animations[name];
            isLooping = looping;
            isPlaying = true;
            frame = 0;
            frameCounter = 0;
            entity.Get<AnimationPart>().currentAnimation.animation[frame].Trigger();
        }

        public void PlayAnimationIfClear(string name, Boolean looping) // if no animation is playing then it will play; use in cases to prevent spamming
        {
            if (!isPlaying)
            {
                currentAnimation = Animations[name];
                isLooping = looping;
                isPlaying = true;
                frame = 0;
                frameCounter = 0;
                entity.Get<AnimationPart>().currentAnimation.animation[frame].Trigger();
            }
        }



        //Add function to play an animation x times



        public void FreezeAnimation(int time)
        {
            isFrozen = true;
            freezeTime = time;
        }

        public void CancelAnimation()
        {
            isPlaying = false;
            frameCounter = 0;
            frame = 0;
            entity.Get<DrawablePart>().SourceRectangle = new Rectangle(0, 0, (int)frameSize.X, (int)frameSize.Y);
        }

        public Keyframe GetAnimationKeyframe(string name, int frame) // This is zero indexed!
        {
            return Animations[name].animation[frame];
        }

        public Keyframe GetFinalKeyframe(string name)
        {
            return Animations[name].animation[Animations[name].animation.Length-1];
        }

        int frame;
        int frameCounter;
        int freezeCounter;
        public override void Update(GameTime gameTime)
        {
            if (entity.Has<DrawablePart>() && isActive)
            {
                if (isPlaying && !isFrozen)
                {
                    Rectangle sourceRect = entity.Get<DrawablePart>().SourceRectangle;
                    frameSize = entity.Get<DrawablePart>().FrameSize;
                    if (frameCounter >= (currentAnimation.animation[frame].time))
                    {
                        if (frame + 1 > currentAnimation.endFrame)
                        {
                            entity.Get<AnimationPart>().currentAnimation.animation[frame].Trigger();
                            if (!isLooping)
                            {
                                CancelAnimation();
                                return;
                            }
                            else
                                frame = currentAnimation.startFrame;
                        }
                        else
                        {
                            frame++;
                            entity.Get<AnimationPart>().currentAnimation.animation[frame].Trigger();
                        }
                        frameCounter = 0;
                    }
                    else if (isPlaying)
                    { frameCounter++; }


                    entity.Get<DrawablePart>().SourceRectangle = new Rectangle(frame * (int)frameSize.X, currentAnimation.yFrame * (int)frameSize.Y, (int)frameSize.X, (int)frameSize.Y);
                }
                else if (isFrozen)
                {
                    if (freezeCounter >= freezeTime)
                    {
                        isFrozen = false;
                        freezeCounter = 0;
                    }
                    else
                        freezeCounter++;
                }
            }
            base.Update(gameTime);
        }

    }
}
