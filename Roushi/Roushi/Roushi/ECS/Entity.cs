using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roushi
{

    public delegate void EntityDyingEventHandler();

    public class Entity
    {

        public event EntityDyingEventHandler Dying;

	    private Boolean isInitialized = false;
	    private Boolean isActive = false;
        private Boolean isAlive;
	    private Dictionary<Type, Part> parts = new Dictionary<Type, Part>();
	    private ArrayList partsToAdd = new ArrayList();
	    private ArrayList partsToRemove = new ArrayList();

        public int id; // number id
        public double drawOffset;
        public string uniqueID; // the name of something, but there must only be one
        public string categoryID; // "enemy", "item", "grass", etc.

        public EntityManager entityManager;

        // Should this entity be updated?
        public Boolean IsActive()
        { return isActive; }

        public Boolean IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }
        }

        public void CallDyingEvent()
        {
            if (Dying != null)
                Dying();
        }

        // Set if the entity is active or not
        public void SetActive(Boolean isActiveIn)
        { isActive = isActiveIn; }

        // If there is a part of type T in the entity then it will return true else false
        public Boolean Has(Type part) 
        {
            return parts.ContainsKey(part);
        }

        public Boolean Has<T>()
        {
            return parts.ContainsKey(typeof(T));
        }

        // Gets the part of type T attached to the entity
        public T Get<T>() where T : Part
        {
            if (!Has<T>())
            {
                throw new InvalidOperationException("Part " + typeof(T).Name + " could not be found :(");
            }

            return (T)parts[typeof(T)];
        }

        // Adds part to entity
        public void Attach(Part part)
        {
            if (!Has(part.GetType()))
            {    //throw new InvalidOperationException("Part of type " + part.GetType().FullName + " already exists.");

                parts.Add(part.GetType(), part);
                part.SetEntity(this);

                if (isInitialized)
                { part.Initialize(); }
            }
        }

        // Removes the existing part if it exists and adds in a new version
        public void Replace(Part part)
        {
            if (Has(part.GetType()))
                Detach(part);

            if (isInitialized)
                partsToAdd.Add(part);
            else
                Attach(part);
        }

        // Removes a part of type T
        public void Detach<T>(T part)
        {
            if (Has(typeof(T)) && !partsToRemove.Contains(typeof(T)))
            {
                partsToRemove.Add(part);
            }
        }

        // Starts up the entity and makes it active
        public void Initialize(EntityManager em)
        {
            entityManager = em;
            Random randy = new Random();
            isInitialized = true;
            isActive = true;
            isAlive = true;
            drawOffset = randy.NextDouble();
            foreach(Part part in parts.Values)
            {
                part.Initialize();
            }
        }

        public void ChangeEntityManager(EntityManager newEM)
        {
            entityManager = newEM;
        }

        // Deactivates and removes attached parts
        public void CleanUp()
        {
            isActive = false;
            foreach (Part part in parts.Values)
            {
                part.CleanUp();
            }
        }

        // Updates parts, removes parts queue'd to be removed, and adds parts meant to be added
        public void Update(GameTime gameTime)
        {
            foreach (Part part in parts.Values)
            {
                part.Update(gameTime);
            }

            while (!(partsToRemove.Count == 0))
            {
                object t = partsToRemove[0];
                remove(t.GetType());
                partsToRemove.Remove(0);
            }

            while (!(partsToAdd.Count == 0))
            {
                object t = partsToAdd[0];
                remove(t.GetType());
                partsToAdd.Remove(0);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Part part in parts.Values)
            {
                part.Draw(spriteBatch);
            }
        }
        
        // Removes a part
        private void remove<T>(T partClass)
        {
            if(!Has(partClass.GetType()))
                throw new InvalidOperationException("Part of type " + partClass.GetType().FullName + " already exists.");

            parts[partClass.GetType()].CleanUp();
            parts.Remove(partClass.GetType());
        }
    }
}
