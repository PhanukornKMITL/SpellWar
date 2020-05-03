using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar.gameObject.component {
   public class GraphicsComponent : Component{

        protected AnimatedSprite animated;
        protected Texture2D _texture;
        protected GraphicsDeviceManager graphics;
        public GraphicsComponent(Game currentScene,Texture2D texture) {
            _texture = texture;
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent) {
           
            
        }

        public override void ReceiveMessage(int message, Component sender) {
            
        }

        public override void Reset() {
           
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent) {
           
        }
    }
}
