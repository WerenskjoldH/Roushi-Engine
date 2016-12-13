using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

/**
 * By: Hunter Werenskjold
 */

namespace Roushi
{
    public delegate void ButtonEventHandler(); // Create a delegate for all button events to form to. 

    class Button : EntityObject
    {
        public Boolean IsSelected; // find out if the button is selected
        public Boolean prevSelected; // what it was last update
        public Boolean MouseIntersecting; // is the mouse inside of its hitbox?
        public Boolean IsInactive; // turn off the button

        Color deactivatedColor;

        public Color DeactivatedColor // change this to change the deactivated color
        {
            set
            {
                deactivatedColor = value;
                image.Color = deactivatedColor;
            }
        }
        
        public event ButtonEventHandler OnLeftMouseDown; // Create an event for if the left mouse is pressed down on the button

        public event ButtonEventHandler OnLeftMouseUp; // Create an event for if the left mouse is coming up on the button

        public event ButtonEventHandler OnClick; // Create event for if the button has been clicked

        public event ButtonEventHandler OnEnter; // Mouse passes inside the button

        public event ButtonEventHandler OnLeave; // Mouse leaves the button

        public event ButtonEventHandler OnSelection; // Occurs once once selected and stays until unselected
        
        public Button(Image Image) // Instantiate the button
            :base()
        {
            image = Image; // pass Image to image
            IsSelected = false;
            prevSelected = false;
            IsInactive = false;

            entityType = EntityType.ScreenItem;

            deactivatedColor = Color.LightSlateGray; // default deactivated color

            // tie events to functions inside the class
            OnLeftMouseDown += new ButtonEventHandler(LeftMouseDown); 
            OnLeftMouseUp += new ButtonEventHandler(LeftMouseUp);
            OnEnter += new ButtonEventHandler(Enter);
            OnLeave += new ButtonEventHandler(Leave);
            OnSelection += new ButtonEventHandler(Selection);
        }

        protected virtual void Enter()
        {
            MouseIntersecting = true;
        }

        protected virtual void Leave()
        {
            MouseIntersecting = false;
        }

        protected virtual void LeftMouseDown()
        {
            //image.Color = Color.Red;
        }

        protected virtual void LeftMouseUp()
        {
            //image.Color = Color.White;
        }

        protected virtual void Selection()
        {

        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public void Deactivate() // Deactivate the button from working
        {
            image.Color = deactivatedColor;
            IsInactive = true;
        }

        public void Activate() // Activate the button to work
        {
            image.Color = Color.White;
            IsInactive = false;
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            base.Update(gameTime);
            image.Update(gameTime);

                Rectangle mouseRec = new Rectangle((int)InputManager.Instance.MousePosition.X, (int)InputManager.Instance.MousePosition.Y, (int)1, 1); // Create an event for the mouse

                if (InputManager.Instance.LeftMousePressed() && mouseRec.Intersects(image.CurrentBounds)) // If the left mouse is pressed and the mouse intersects the bounds of the image
                    OnClick(); 
                else if (InputManager.Instance.LeftMouseReleased() && mouseRec.Intersects(image.CurrentBounds)) // If the left mouse has released then trigger the mouse up event and all listeners
                    OnLeftMouseUp(); // on left mouse release

                if (mouseRec.Intersects(image.CurrentBounds) && !MouseIntersecting) // On the mouse intersection execute the following
                {
                    OnEnter();
                    MouseIntersecting = true; // Mouse interecting!
                }
                else if (!mouseRec.Intersects(image.CurrentBounds) && MouseIntersecting) // On the mouse leave execute the following
                {
                    OnLeave();
                    MouseIntersecting = false; // Mouse is not intersecting :c
                }

                if (IsSelected) // if the button is selected, then do the following
                {
                    if (InputManager.Instance.KeyPressed(Keys.Enter, Keys.Space, Keys.J)) // If you press enter while the button is selected
                    {
                        OnClick(); // Activate click
                    }
                    if (prevSelected != IsSelected)
                    {
                        OnSelection();
                    }
                }

             prevSelected = IsSelected;
        }

        public void Deselected() // Deselect the button
        {
            //image.Color = Color.White;
            IsSelected = false;
        }

        public override void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }



    }
}
