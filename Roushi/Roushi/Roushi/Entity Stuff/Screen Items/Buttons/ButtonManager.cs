using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

/**
 * By: Hunter Werenskjold
 */


namespace Roushi
{
    class ButtonManager
    {
        ArrayList buttons;
        int selectedButton;

        Color selectedColor;
        Color deselectedColor;

        public Color Selected // when a button is selected it goes to this color
        {
            set
            {
                selectedColor = value;
            }
        }

        public Color DeselectedColor // when a button is deselected it goes to this color
        {
            set
            {
                deselectedColor = value;
            }
        }

        public ButtonManager(Vector2 position)
        {
            buttons = new ArrayList(); // Instantiate the array list of buttons
            selectedButton = 0; // Set the initial button to the first in the array 

            deselectedColor = Color.White;
            selectedColor = Color.Red;

            
        }

        public void AddButton(Button b) // Add a button into the array
        {
            if(!b.IsInactive) // if the button is active then it's deselectedColor will be changed
                b.image.Color = deselectedColor;
            b.image.Effects = "FadeEffect"; // add fade effect to buttons as they come in
            b.image.LoadContent();
            b.image.FadeEffect.FadeSpeed = .35f;
            b.image.FadeEffect.Increase = true;
            buttons.Add(b); // add the button into the arraylist
        }

        public void UnloadContent()
        {
            foreach (Button b in buttons) // unload all the button content
                b.UnloadContent();
        }

        Button tempButton; // add a temp button so you can test the buttons properties in the button manager class
        public void Update(GameTime gameTime)
        {
            // Takes Care Of Inputs
            if (InputManager.Instance.KeyReleased(Keys.Up, Keys.W))
            {
                selectedButton--; // move upwards in the list of buttons towards the first button in the array, 0.


                if (selectedButton < 0) // if the button is less than 0 then like oh shucks man that button won't exist so we need to goto the bottom of the list of buttons
                    selectedButton = buttons.Count - 1; // goto the last button in the list


                Button buttonPreview = (Button)buttons[selectedButton]; // store the next button to preview its properties


                if (buttonPreview.IsInactive) // if the button is offline then move past it upwards
                    selectedButton--;


                if (selectedButton < 0) // did it go negative? if so then move to the bottom of the list
                    selectedButton = buttons.Count-1;
                
            }
            else if (InputManager.Instance.KeyReleased(Keys.Down, Keys.S))
            {
                selectedButton++; // move down in the array

                if (selectedButton >= buttons.Count) 
                    selectedButton = 0;

                Button buttonPrev = (Button)buttons[selectedButton];

                if (buttonPrev.IsInactive)
                    selectedButton++;

                if (selectedButton >= buttons.Count)
                    selectedButton = 0;
            }


            // Takes Care of If and When Things Are Selected/Deselected
            if (tempButton != buttons[selectedButton] && tempButton != null) // if the temp button is different from the current button and tempButton is not null
            {
                tempButton.image.IsActive = false; // Means it should update because there are effects that effect appearance
                tempButton.image.Alpha = 1;
                tempButton.image.Color = deselectedColor;
                tempButton.Deselected(); // deselect the current button
                tempButton = (Button)buttons[selectedButton]; // set the temp button to the curren
            }
            else if (tempButton == null) // if it actually is null then like
            {
                tempButton = (Button)buttons[selectedButton]; // set dat shet equal to the current selected button
            }

            if (!tempButton.IsSelected) // When the button is initially selected
            {
                tempButton.IsSelected = true;
                tempButton.image.IsActive = true; // Means it should update because there are effects that effect appearance
                tempButton.image.Color = selectedColor;
                tempButton.image.Alpha = 1;
            }

            //Updates Buttons
            if (buttons.Count > 0) 
            {
                int curButton = 0;  
                foreach (Button b in buttons)
                {
                    if (!b.IsInactive) // If the button has been turned off then don't even bother updating it, saves space and time
                    {
                        b.Update(gameTime);
                        if (b.MouseIntersecting) // if the mouse intersects the button then set it to the selected button
                            selectedButton = curButton;
                    }
                    curButton++;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(buttons.Count > 0)
                foreach (Button b in buttons)
                    b.Draw(spriteBatch);
        }

    }
}
