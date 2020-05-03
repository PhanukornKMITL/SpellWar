using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar.gameObject.component.PlayerComponent {
    public class PlayerGraphicsComponent : GraphicsComponent {
        
        
        public PlayerGraphicsComponent(Game currentScene,Texture2D texture) : base(currentScene, texture) {
            animated = new AnimatedSprite(texture, 3, 4);
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent) {
            animated.Draw(spriteBatch, parent.Position, 2);
           

            
        }

        public override void ReceiveMessage(int message, Component sender) {
            base.ReceiveMessage(message, sender);
        }

        public override void Reset() {
            
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent) {
           
            

            animated.Update(gameTime);
        }
    }
}
