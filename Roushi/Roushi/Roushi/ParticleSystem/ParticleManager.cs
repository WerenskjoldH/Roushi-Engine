using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roushi
{
    public class ParticleManager<T>
    {
        public class Particle
        {
            public Texture2D Texture;
            public Vector2 Position;
            public float Orientation;

            public float Scale = 1;

            public Color Tint;
            public float Duration;
            public float PercentLife = 1f;
            public T State;
        }

        private class CircularParticleArray
        {
            private int start;
            public int Start // set where index 0 is in the array
            {
                get { return start; }
                set { start = value % list.Length; }
            }

            public int Count { get; set; } // how may active particles in the list
            public int Capacity { get { return list.Length; } } // get array size
            private Particle[] list; // array of particles

            public CircularParticleArray(int capacity)
            {
                list = new Particle[capacity];
            }

            public Particle this[int i] // getter setter for when CircularParticleArray[index] is used
            {
                get { return list[(start + i) % list.Length]; } // CircularParticleArray[oldest + i] will get the "oldest" particle in the array
                set { list[(start + i) % list.Length] = value; } // CircularParticleArray[oldest + i] will set the "oldest" particle in the array
            }
        }

        // updateParticle is a custom method that updates the particles with the "desired effect" this way you can have multiple particle managers that update differently
        private Action<Particle> updateParticle; // Delegate that will be called for each particle
        private CircularParticleArray particleList;

        public ParticleManager(int capacity, Action<Particle> updateParticle) // Constructor is the only place this class alocates memory
        {
            this.updateParticle = updateParticle;
            particleList = new CircularParticleArray(capacity);

            // fill the list with empty particle's so that they can be easily reused
            for (int i = 0; i < capacity; i++)
                particleList[i] = new Particle();
        }

        public void CreateParticle(Texture2D texture, Vector2 position, Color tint, float duration, float scale, T state, float theta = 0)
        {
            Particle particle;
            // This whole section of code decides what particle in the array is going to be tampered with, then it is assigned to the temporary variable particle and then altered through that by reference
            if (particleList.Count == particleList.Capacity)
            {
                //"if the list is full, overwrite the oldest particle, and rotate the circular list by one"
                particle = particleList[0];
                particleList.Start++; // increases the index of the array by one so now the oldest pixel is the 2nd one in the array            
            }
            else
            {
                particle = particleList[particleList.Count];
                particleList.Count++;
            }

            particle.Texture = texture;
            particle.Position = position;
            particle.Tint = tint;

            particle.Duration = duration;
            particle.PercentLife = 1f;
            particle.Scale = scale;
            particle.Orientation = theta;
            particle.State = state;
        }

        public void Update()
        {
            int removalCount = 0;
            for (int i = 0; i < particleList.Count; i++)
            {
                var particle = particleList[i];
                updateParticle(particle);
                particle.PercentLife -= 1f / particle.Duration; // lower the percent of life by the decimal value of the duration 

                // sift deleted particles to the end of the list
                Swap(particleList, i - removalCount, i);

                if (particle.PercentLife < 0) // if this particle is past its life then remove it
                    removalCount++;
            }

            particleList.Count -= removalCount;
        }

        private static void Swap(CircularParticleArray list, int index1, int index2)
        {
            var temp = list[index1];
            list[index1] = list[index2];
            list[index2] = temp;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < particleList.Count; i++)
            {
                var particle = particleList[i];

                float textureBottom; // Finds the lowest point of the texture when it rotates
                Vector2 bottom;
                bottom.X = Math.Abs((((particle.Texture.Width/2)*particle.Scale)*(float)Math.Sin(particle.Orientation)));
                bottom.Y = Math.Abs((((particle.Texture.Height/2)*particle.Scale) * (float)Math.Sin(particle.Orientation)));

                if (bottom.X < bottom.Y)
                    textureBottom = bottom.Y;
                else
                    textureBottom = bottom.X;
                //Vector3 vector = new Vector3(particle.Texture.Width, particle.Texture.Height, 0);
                //var rotation = Matrix.CreateRotationZ(particle.Orientation);
                //var translateTo = Matrix.CreateTranslation(vector);
                //var translateBack = Matrix.CreateTranslation(-vector);
                //var combined = translateTo * rotation * translateBack;
                //Vector3 rotatedVector = Vector3.Transform(vector, combined);

                spriteBatch.Draw(particle.Texture, particle.Position, null, particle.Tint, particle.Orientation, new Vector2(particle.Texture.Width/2, particle.Texture.Height/2), particle.Scale, 0, (1/(textureBottom+particle.Position.Y)));

            }
        }

    }
}
