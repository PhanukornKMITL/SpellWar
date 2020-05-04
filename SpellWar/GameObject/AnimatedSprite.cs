using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpellWar.gameObject {
    public class AnimatedSprite
    {
        protected Vector2 sPosition;

        protected Texture2D sTexteure;
        private Rectangle[] sRectangles;
        private int frameIndex;

        private double timeElapsed;
        private double timeToUpdate = 0.2f;

        protected Vector2 sDirection = Vector2.Zero;

        protected string currentAnimation;
        public enum myDirection { none, left, right, up };

        protected myDirection currenDir= myDirection.none;


        public AnimatedSprite(Vector2 position,Texture2D texture)
        {
            sPosition = position;
            sTexteure = texture;
        }


        private Dictionary<string, Rectangle[]> sAnimation = new Dictionary<string, Rectangle[]>();



        public void addAnimation (int frames ,int yPos , int xStartFrame,string name,int width ,int height,Vector2 offset)
        {
            
            Rectangle[] Rectangles = new Rectangle[frames];

            for (int i = 0; i < frames; i++)
            {
                Rectangles[i] = new Rectangle((i+ xStartFrame) * width, yPos,width,height);

            }
            sAnimation.Add(name, Rectangles);
        }



        public virtual void Update(GameTime gameTime)
        {
            timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if (timeElapsed>timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (frameIndex < sAnimation[currentAnimation].Length-1)
                {
                    frameIndex++;
                }
                else
                {
                    //AnimationDone(currentAnimation);
                    frameIndex = 0;
                }
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sTexteure, sPosition, sAnimation[currentAnimation][frameIndex], Color.White);      
        }

        public void PlayAnimation (string name)
        {
            if (currentAnimation !=name && currenDir == myDirection.none)
            {
                currentAnimation = name;
                frameIndex = 0;
            }
        }

        //public void AnimationDone(string animation) {
            //if (animation.Contains("Attack"))
           // {
             //   attacking = false;
           // }
        //}
        
    }
}