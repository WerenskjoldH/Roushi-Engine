using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/**
 * By: Hunter Werenskjold
 */

namespace Roushi
{
    public enum DialogLocation
    {
        Foreground,
        Background
    }

    class DialogManager // Dialog Boxes Of All Sorts Go Here
    {
        public ArrayList Dialogs;
        ArrayList dialogsToRemove;
        Texture2D foreground, background;

        public DialogManager()
        {
            Dialogs = new ArrayList();
            dialogsToRemove = new ArrayList();
            foreground = new Texture2D(ScreenManager.Instance.GraphicsDevice, 1, 1);
            background = new Texture2D(ScreenManager.Instance.GraphicsDevice, 1, 1);
        }

        public void AddDialog(Dialog dialog)
        {
            dialog.Initialize();
            Dialogs.Add(dialog);

            RenderTarget2D RT = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)ScreenManager.Instance.Dimensions.X, (int)ScreenManager.Instance.Dimensions.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(RT);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();

            foreach (Dialog d in Dialogs)
            {
                d.Draw(ScreenManager.Instance.SpriteBatch);
            }

            ScreenManager.Instance.SpriteBatch.End();

            if (dialog.dialogLocation == DialogLocation.Foreground)
                foreground = RT;
            else
                background = RT;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
        }

        public void UnloadContent()
        {
            foreach (Dialog d in Dialogs)
                Remove(d);
        }

        private void Remove(Dialog d)
        {
            d.UnloadContent();
            dialogsToRemove.Add(d);
        }

        public void RemoveDialog(Dialog dialog)
        {
            Dialogs.Remove(dialog);
            dialogsToRemove.RemoveAt(0);

            RenderTarget2D RT = new RenderTarget2D(ScreenManager.Instance.GraphicsDevice, (int)ScreenManager.Instance.Dimensions.X, (int)ScreenManager.Instance.Dimensions.Y);

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(RT);
            ScreenManager.Instance.GraphicsDevice.Clear(Color.Transparent);
            ScreenManager.Instance.SpriteBatch.Begin();

            foreach (Dialog d in Dialogs)
            {
                d.Draw(ScreenManager.Instance.SpriteBatch);
            }

            ScreenManager.Instance.SpriteBatch.End();

            if (dialog.dialogLocation == DialogLocation.Foreground)
                foreground = RT;
            else
                background = RT;

            ScreenManager.Instance.GraphicsDevice.SetRenderTarget(null);
        }

        // create two textures, one for foreground and one for background
        // 0.00000001f - foreground
        // 0.009f - background
        public void Update(GameTime gameTime)
        {
            foreach (Dialog tp in Dialogs)
            {
                if (tp.isAlive)
                    tp.Update(gameTime);
                else
                    dialogsToRemove.Add(tp);
            }

            while (dialogsToRemove.Count > 0)
            {
                RemoveDialog((Dialog)dialogsToRemove[0]);
            }

        }

        public void DrawForeground(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(foreground, Vector2.Zero, new Rectangle(0, 0, foreground.Width, foreground.Height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }
        public void DrawBackground(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, Vector2.Zero, new Rectangle(0, 0, background.Width, background.Height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.09999f);
        }
    }
}
