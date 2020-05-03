using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar.gameObject.component.ItemComponent {
   public class ItemPhysicsComponent : PhysicsComponent {
        int health = 1, walk = 2, power = 1;

        public ItemPhysicsComponent(Game currentScene) : base(currentScene) {
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

            Console.WriteLine(parent.Position);
            foreach (GameObject g in gameObjects) {
                if (g.Name.Equals("Player1") || g.Name.Equals("Player2")) {
                    Action(parent, g);
                }
            }

        }

        public void Action(GameObject obj1, GameObject obj2) {
           
                if (obj1.getRect.Intersects(obj2.getRect)) {
                     if (obj1.Name.Equals("health")) {
                        obj2.Health += health;
                   
                    }
                else if(obj1.Name.Equals("walk")) {
                    obj2.WalkSlot += walk;
                }
                else if(obj1.Name.Equals("power")) {
                    obj2.Power += power;
                }

                obj1.IsActive = false;

            }





        }
    }
}
