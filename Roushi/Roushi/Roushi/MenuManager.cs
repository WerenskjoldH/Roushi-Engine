using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roushi
{
    public class MenuManager
    {
        Menu menu;

        public MenuManager()
        {
            menu = new Menu();
            menu.OnMenuChange += new EventHandler(menu_OnMenuChange);
        }

        void menu_OnMenuChange(object sender, EventArgs e)
        {
            menu.UnloadContent();
            menu = menu.ID;
            menu.LoadContent();
        }

        public void LoadContent(Menu newMenu)
        {
            if (newMenu != null)
                menu.ChangeMenu(newMenu);
        }



        public void UnloadContent()
        {
            menu.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            menu.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }

    }
}
