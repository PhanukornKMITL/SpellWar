using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellWar {
    class Player {
        Vector2 position= Vector2.Zero;
        private int health = 5;
        Rectangle hitbox;
        Texture2D texture;

        public Player(Texture2D texture) {
            this.texture = texture;
            hitbox = new Rectangle((int)position.X,(int)position.Y,texture.Width, texture.Height);
            
        }

        public Vector2 Position {
            get { return position; }
            set { position = value; hitBoxPosition(position); }
        }

        public void  hitBoxPosition(Vector2 position) {
            hitbox.X = (int) position.X;
            hitbox.Y = (int)position.Y;
        }

       public Rectangle getRect {
            get { return hitbox; }
        }

        public int Health {
            get { return health; }
            set { health = value;}
        }


    }
}
