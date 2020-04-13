using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellWar.gameObject{
  public  class Player : GameObject {
        Vector2 position= Vector2.Zero;
        Texture2D texture, heart;
        Rectangle hitBox;
        
       

        public Player(Texture2D texture, Texture2D heart) : base(texture) {
            this.texture = texture;
            this.heart = heart;
           
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects) {
            


           


           
        }

        public override void Draw(SpriteBatch spriteBatch) {

            spriteBatch.Draw(texture, this.Position, Color.White);
            //Draw Heart
            if (this.Name.Equals("Player1")) {
                for (int i = 0; i < this.Health; i++) {
                    spriteBatch.Draw(heart, new Vector2(3 + i * heart.Width, 3), Color.White);
                }
            }


            else {
                for (int i = 0; i < this.Health; i++) {
                    spriteBatch.Draw(heart, new Vector2((1600 - heart.Width) - (i * heart.Width), 3), Color.White);
                }

            }
           
        }

        public override void Reset() {

            if (this.Name.Equals("Player1")) {
                //Left Player
               this.Position = new Vector2(Singleton.Instance.leftArea[2], 920 - 170);
            }
            else {
                //Right Player
                this.Position = new Vector2(Singleton.Instance.rightArea[2], 920 - 170);
            }
           
          


        }







    }
}
