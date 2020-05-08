using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar.gameObject.component.PlayerComponent {
    class PlayerPhysicsComponent : PhysicsComponent {
        double vi = -820, t = 0; // vi - initial velocity | t - time
        double g = 520; // pixels per second squared | gravitational acceleration
        int keyState = 0;
        int kState = 0;
        double v = -820, vx, vy, alpha, t2 = 0;
        //----------------------------------------------------------------------//

        public PlayerPhysicsComponent(Game currentScene) : base(currentScene) {
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent) {
            base.Draw(spriteBatch, parent);
        }

        public override void ReceiveMessage(int message, Component sender) {
            base.ReceiveMessage(message, sender);
        }

        public override void Reset() {
            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent) {
            //gravity
            if(parent.Position.Y <  900 - 183) {
                parent.Position = new Vector2(parent.Position.X, (float)(0 * t + g * t * t / 2));
                
            }
          
            
            t += gameTime.ElapsedGameTime.TotalSeconds;
            base.Update(gameTime, gameObjects, parent);
        }
    }
}
