using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

/**
 * By: Hunter Werenskjold
 */

namespace Roushi
{
    public class InputManager
    {
        KeyboardState currentKeyState, prevKeyState;

        MouseState currentMouseState, prevMouseState;

        public Vector2 MousePosition // output the mouse pos as a vector2
        {
            get
            {
                return new Vector2(currentMouseState.X, currentMouseState.Y);
            }
        }

        private static InputManager instance; // internal instance of input manager

        public static InputManager Instance // Definition on a singleton
        {
            get
            {
                if (instance == null)
                    instance = new InputManager(); // didn't exist so making a new one

                return instance; // returns instance of input manager if it happens to be empty a new one will be made and added
            }
        }

        public void Update()
        {
            prevKeyState = currentKeyState; // store the prior keystate as the current keystate
            prevMouseState = currentMouseState; // store the prior mousestate as the current mousestate
            if (!ScreenManager.Instance.IsTransitioning || ScreenManager.Instance.TransitionBeginning) // if there's a transition then the current mouse state and keyboard state then it will update once more
            {
                currentMouseState = Mouse.GetState();
                currentKeyState = Keyboard.GetState();
            }
        }

        public bool LeftMousePressed()
        {
            if (currentMouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released) // if the mouse is down and the prior state was released then return that the mouse has been clicked
                return true;
            else return false;
        }

        public bool LeftMouseReleased() // if the mouse is released after the prior mouse state was pressed then return the mouse state was released
        {
            if (currentMouseState.LeftButton == ButtonState.Released && prevMouseState.LeftButton == ButtonState.Pressed)
                return true;
            else return false;
        }

        public bool RightMousePressed() // please no more just scroll a few lines up, if you're reading this and you're not Hunter M. Were... then you're probably not supposed to be here
        {
            if (currentMouseState.RightButton == ButtonState.Pressed && prevMouseState.RightButton == ButtonState.Released)
                return true;
            else return false;
        }

        public bool RightMouseReleased()
        {
            if (currentMouseState.RightButton == ButtonState.Released && prevMouseState.RightButton == ButtonState.Pressed)
                return true;
            else return false;
        }

        public bool KeyPressed(params Keys[] keys) // if the provided keys are pressed and then released, THEN return true: otherwise false.
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                    return true;
            }
            return false;
        }

        public bool KeyReleased(params Keys[] keys) // if the provided keys are released, then return true: otherwise false;
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

        public bool KeyDown(params Keys[] keys) // if the provided keys are pressed, then return true: otherwise false;
        {
            foreach (Keys key in keys)
            {
                if (currentKeyState.IsKeyDown(key))
                    return true;
            }
            return false;
        }

    }
}
