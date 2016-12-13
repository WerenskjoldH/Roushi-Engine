using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework;
using System.Collections;

/**
 * By: Hunter Werenskjold
 */


/**
 *~!IMPORTANT!~*
 *For songs their title must contain "Song - " before the name. This is so that you can loop through the activeSounds and stop all songs.
 **/

namespace Roushi
{
    class SoundDictionary
    {

        public Dictionary<string, SoundEffect>.KeyCollection AudioKeys
        {
            get { return sounds.Keys; }
        }

        Dictionary<string, SoundEffect> sounds = new Dictionary<string, SoundEffect>();
        ArrayList activeSounds;
        ArrayList soundsToRemove;
        bool disableSongs;
        bool disableSounds;


        public bool DisableSongs { get { return disableSongs; } set { disableSongs = value; } }
        public bool DisableSounds { get { return disableSounds; } set { disableSounds = value; } }

        public SoundDictionary()
        {
            sounds = new Dictionary<string,SoundEffect>();

            disableSongs = false;
            activeSounds = new ArrayList();
            soundsToRemove = new ArrayList(); // holds sounds that gotta be removed
        }

        public void AddSound(SoundEffect sound, string name)
        {
            if (!sounds.ContainsKey(name))
            {
                sounds.Add(name, sound);
            }
        }

        public void UnloadContent()
        {
            foreach (SoundEffect s in sounds.Values)
            {
                s.Dispose();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (SoundEffectInstance s in activeSounds)
            {
                if (s.State == SoundState.Stopped)
                    soundsToRemove.Add(s);
            }


            if (soundsToRemove.Count > 0) // This removes any sounds that are stopped and not playing. Helps clear space y'know c:
                foreach (SoundEffectInstance s in soundsToRemove)
                {
                    s.Dispose(); 
                    activeSounds.Remove(s);
                }
        }

        public void PlaySound(string name) // creates an instance of the sound and saves it in the arraylist so it can be modified after being played
        {
            if (sounds.ContainsKey(name) && !disableSounds)
            {
                if ((!(disableSongs && name.Length > 4 && name.Substring(0, 5) == "Song")))
                {
                    SoundEffect soundEff = (SoundEffect)sounds[name];
                    SoundEffectInstance sin = soundEff.CreateInstance();
                    sin.Play();
                    activeSounds.Add(sin);
                }
            }
        }

        public void PlaySoundAtVolume(string name, float volume) // out of 0 to 100
        {
            if (sounds.ContainsKey(name) && !disableSounds)
            {
                if ((!(disableSongs && name.Length > 4 && name.Substring(0, 5) == "Song")))
                {
                    SoundEffect soundEff = (SoundEffect)sounds[name];
                    SoundEffectInstance sin = soundEff.CreateInstance();
                    sin.Volume = volume * .01f;
                    sin.Play();
                    activeSounds.Add(sin);
                }
            }
        }

        public void PlayLoopedSound(string name)
        {
            if (sounds.ContainsKey(name) && !disableSounds)
            {
                if (( !disableSongs && !(name.Length > 4 && name.Substring(0, 5) == "Song")))
                {
                    SoundEffect soundEff = (SoundEffect)sounds[name];
                    SoundEffectInstance sin = soundEff.CreateInstance();
                    sin.IsLooped = true;
                    sin.Play();
                    activeSounds.Add(sin);
                }
            }
        }

        public void StopAllSounds()
        {
            foreach (SoundEffectInstance s in activeSounds)
            {
                s.Stop();
                s.Dispose();
            }
            activeSounds.Clear();
        }

        public void DimToStop() // dims out sounds
        {
            if (soundsToRemove.Count > 0)
                foreach (SoundEffectInstance s in soundsToRemove)
                {
                    s.Dispose();
                    activeSounds.Remove(s);
                }
            if (activeSounds.Count == 0)
            {
                SoundManager.Instance.DimToStopSound = false;
                activeSounds.Clear();
                return;
            }

            foreach (SoundEffectInstance s in activeSounds)
            {
                if (s.Volume - .01f <= 0)
                {
                    s.Stop();
                    soundsToRemove.Add(s);
                }
                else
                {
                    s.Volume -= .006f; // rate to lower volume
                    s.Pitch -= .006f ; // rate to lower pitch
                    s.Pan -= .01f * (-.01f); // makes it bounce back and forth between ears, makes it sound nifty
                }
            }

        }

        public bool ContainsSound(string name)
        {
            return sounds.ContainsKey(name);
        }

    }
}
