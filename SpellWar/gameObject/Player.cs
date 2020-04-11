using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellWar.gameObject{
    class Player : GameObject {
        Vector2 position= Vector2.Zero;
        
        Rectangle hitBox;
       

        public Player(Texture2D texture) : base(texture) {
            
           
           
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects) {

           


            base.Update(gameTime, gameObjects);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            base.Draw(spriteBatch);
        }

        public override void Reset() {
            base.Reset();
        }

   

        

       
    }
}
