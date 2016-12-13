using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;

namespace Roushi
{
    class Textbox
    {
        Rectangle textBox;
        SpriteFont spriteFont;
        Color fontColor;
        string text;
        string parsedText;
        string typedText;
        double typedTextLength;
        int delayInFrames;
        public bool doneDrawing;


        public Textbox(Rectangle TextBox, string SpriteFont, string Text, Color FontColor) // Only pass the font name not the directory
        {
            textBox = TextBox;
            spriteFont = ScreenManager.Instance.Content.Load<SpriteFont>("Fonts/" + SpriteFont);
            text = Text;
            fontColor = FontColor;

            parsedText = parseText(text);
            delayInFrames = 3;
            doneDrawing = false;
            ctr = 0;
        }

        public void Reset()
        {
            parsedText = parseText(text);
            delayInFrames = 3;
            doneDrawing = false;
            ctr = 0;
            typedText = null;
            typedTextLength = 0;
        }

        int ctr;
        public void Update() // each time a new letter is written have a sound play and modify the pitch by the letters hex value
        {


            if (!doneDrawing)
            {
                if (delayInFrames == 0)
                {
                    typedText = parsedText;
                    doneDrawing = true;
                }
                else if (typedTextLength < parsedText.Length)
                {
                    typedTextLength = typedTextLength + (ctr) / delayInFrames;
                    if (ctr == delayInFrames)
                        ctr = 0;

                    if (typedTextLength >= parsedText.Length)
                    {
                        typedTextLength = parsedText.Length;
                        doneDrawing = true;
                    }

                    typedText = parsedText.Substring(0, (int)typedTextLength);
                }
                ctr++;
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (typedText != null)
                spriteBatch.DrawString(spriteFont, typedText, new Vector2(textBox.X, textBox.Y), fontColor);
        }

        private String parseText(String text)
        {
            String line = String.Empty;
            String returnString = String.Empty;
            String[] wordArray = text.Split(' ');

            foreach (String word in wordArray)
            {
                if (spriteFont.MeasureString(line + word).Length() > textBox.Width)
                {
                    returnString = returnString + line + '\n';
                    line = String.Empty;
                }

                line = line + word + ' ';
            }

            return returnString + line;
        }



    }
}
