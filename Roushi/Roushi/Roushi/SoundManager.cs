using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

/**
 * By: Hunter Werenskjold
 */

namespace Roushi
{
    // This class works directly 
    class SoundManager // Singleton
    {
        SoundDictionary soundDirectory;
        public bool DimToStopSound;

        public bool DisableSongs { get { return soundDirectory.DisableSongs; } set { soundDirectory.DisableSongs = value; } }
        public bool DisableSounds { get { return soundDirectory.DisableSounds; } set { soundDirectory.DisableSounds = value; } } // This disables ALL SOUND not just sfx

        private static SoundManager instance;
        public static SoundManager Instance // Pretty much the essential bit that makes this a singleton class
        {
            get
            {
                if (instance == null) // if the instance doesn't exist then create it
                {
                    instance = new SoundManager();
                }

                return instance; // returns itself
            }
        }

        public void DimAndStopSound() // slowly fades out noises then deletes them from the queue
        {
            DimToStopSound = true;
        }

        public SoundManager() // make sure to have all the main methods from this run in Game1
        {
            soundDirectory = new SoundDictionary();
        }

        public void LoadContent()
        {
            DirectoryInfo DI = new DirectoryInfo(ScreenManager.Instance.Content.RootDirectory + "\\Sounds"); // goto this folder
            FileInfo[] files = DI.GetFiles();
            foreach (FileInfo f in files) // automatically adds all sounds in the sounds folder to the sound directory/library
            {
                soundDirectory.AddSound(ScreenManager.Instance.Content.Load<SoundEffect>("Sounds/" + Path.GetFileNameWithoutExtension(f.Name)), Path.GetFileNameWithoutExtension(f.Name));
            }
        }

        public void Update(GameTime gameTime)
        {
            if (DimToStopSound) // if the sound should be dimming and stopped
            {
                soundDirectory.DimToStop();
            }

            soundDirectory.Update(gameTime);
        }

        public void UnloadContent()
        {
            soundDirectory.UnloadContent();
        }

        public void PlaySound(string name) // Plays Given Sound
        {
            soundDirectory.PlaySound(name);
        }

        public void PlaySoundAtVolume(string name, float volume)
        {
            soundDirectory.PlaySoundAtVolume(name, volume);
        }

        public void PlaySoundLooped(string name) // Plays given sound looped
        {
            soundDirectory.PlayLoopedSound(name);
        }

        public void StopAllNoise() // Stops every noise suddenly
        {
            soundDirectory.StopAllSounds();
        }


    }
}
