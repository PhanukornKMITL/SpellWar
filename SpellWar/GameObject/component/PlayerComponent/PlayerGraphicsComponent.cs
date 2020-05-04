using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar.gameObject.component.PlayerComponent {
    public class PlayerGraphicsComponent : GraphicsComponent {
        float playerSpeed = 200;
        //bool attacking = false;
        
        

        public PlayerGraphicsComponent(Game currentScene,Texture2D texture, Vector2 postion) : base(currentScene, texture) {
     
            animated = new AnimatedSprite(postion, texture);
            animated.addAnimation(4, 0, 0, "Stand", 183, 183, new Vector2(0, 0));
            animated.addAnimation(4, 183, 0, "Left", 183, 183, new Vector2(0, 0));
            animated.addAnimation(4, 183, 0, "Right", 183, 183, new Vector2(0, 0));
            animated.addAnimation(4, 366, 0, "Attack", 183, 183, new Vector2(0, 0));
           



        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent) {
           animated.Draw(spriteBatch,parent.Position);
           

            
        }

        public override void ReceiveMessage(int message, Component sender) {
            base.ReceiveMessage(message, sender);
        }

        public override void Reset() {
            
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent) {

            if (parent.Name.Equals("Player1")) {
                animated.PlayAnimation("Left");
            }
            else {
                animated.PlayAnimation("Stand");
            }
            
            
            animated.Update(gameTime);
                

        }
    }
}
