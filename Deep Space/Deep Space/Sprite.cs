using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Deep_Space
{
    class Sprite : Movement
    {
        public Texture2D textures; // Sprite texture, read-only property



        public Sprite(Texture2D newTexture, Vector2 newPosition, Vector2 newSize, int ScreenWidth, int ScreenHeight, int newWrenchCount, int newPresent, int newRotation)
        {
            textures = newTexture;
            position = newPosition;
            size = newSize;
            screenSize = new Vector2(ScreenWidth, ScreenHeight);
            wrenchCount = newWrenchCount;
            present = newPresent;
            rotation = newRotation;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(textures, position, Color.White);
            spriteBatch.Draw(textures, position, null, Color.White, rotation, size/2, 1, SpriteEffects.None, 0);
        }





    }
}
