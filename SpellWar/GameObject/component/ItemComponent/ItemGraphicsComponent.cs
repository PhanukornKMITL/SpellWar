using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar.gameObject.component.ItemComponent {
   public class ItemGraphicsComponent : GraphicsComponent {
        Texture2D texture;

        public ItemGraphicsComponent(Game currentScene, Texture2D texture) : base(currentScene, texture) {


            this.texture = texture;

        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent) {
            spriteBatch.Draw(texture, parent.Position, Color.White);
        }

        public override void ReceiveMessage(int message, Component sender) {
            base.ReceiveMessage(message, sender);
        }

        public override void Reset() {
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent) {
            base.Update(gameTime, gameObjects, parent);
        }
    }
}
