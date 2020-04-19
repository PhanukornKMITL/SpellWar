using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpellWar.gameObject {
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;

        //slow down fram animation
        private int timeSinceLastFrame = 0;
        private int milliseconPerFrame = 150;

        public AnimatedSprite(Texture2D texture, int rows, int columns)
        {
            this.Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
         
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;

            if (timeSinceLastFrame > milliseconPerFrame)
            {
                timeSinceLastFrame -= milliseconPerFrame;
                if (currentFrame > totalFrames) currentFrame = 1;
                //increment currrent fram
                currentFrame++;
                if (currentFrame == totalFrames)
                    currentFrame = 0;
            }

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location,int row)
        {
            int Row = row;
            //ขนาดความกว้างต่อช่อง
            int width = Texture.Width / Columns;
            //ขนาดความยาวต่อช่อง
            int height = Texture.Height / Rows;

            int SpriteRow = (int)((float)currentFrame / (float)Columns);
            int SpriteColumn = currentFrame % Columns;
            
            //ถ้าอยากเลือกแถวก็ เปลี่ยน spriteRow  เป็นเลขแถวนั้นๆ ตาม action 
            Rectangle sourceRectangle = new Rectangle(width * SpriteColumn, height * Row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}