using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar.gameObject.component.BallComponent {
    class BallPhysicsComponent : PhysicsComponent {


        double vi, t = 0; // vi - initial velocity | t - time
        double g = 520; // pixels per second squared | gravitational acceleration
        int keyState = 0;
        int kState = 0;
        double v = -820, vx, vy, alpha, t2 = 0;
        //----------------------------------------------------------------------//

        public BallPhysicsComponent(Game currentScene) : base(currentScene) {
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

            if (Singleton.Instance.leftChooseShoot) {
                alpha = Singleton.Instance.shootPosRight[Singleton.Instance.rightSideShoot];



                vx = v * Math.Cos(alpha);
                vy = v * Math.Sin(alpha);
            }

            else if (Singleton.Instance.rightChooseShoot) {
                alpha = Singleton.Instance.shootPosLeft[Singleton.Instance.leftSideShoot];
                vx = v * Math.Cos(alpha);
                vy = v * Math.Sin(alpha);
            }

            //Projectile Part
            if (Singleton.Instance.kState == 1) {

                if (parent.Name.Equals("voBall")) {

                    parent.Position = new Vector2((float)((vx * -1) * t2) + gameObjects.Single(s => s.Name.Equals("Player1")).Position.X, (float)(vy * t2 + g * t2 * t2 / 2) + (gameObjects.Single(s => s.Name.Equals("Player2")).Position.Y) - 100);

                    t2 = t2 + gameTime.ElapsedGameTime.TotalSeconds;
                }
                Singleton.Instance.ballVisible = true;
            }

            //Right to left side
            if (Singleton.Instance.kState == 2) {
                if (parent.Name.Equals("wizBall")) {
                    parent.Position = new Vector2((float)((vx) * t2) + gameObjects.Single(s => s.Name.Equals("Player2")).Position.X, (float)(vy * t2 + g * t2 * t2 / 2) + (gameObjects.Single(s => s.Name.Equals("Player1")).Position.Y) - 100);

                    t2 = t2 + gameTime.ElapsedGameTime.TotalSeconds;
                }

                Singleton.Instance.ball2Visible = true;
            }



           


        }
    }
}
