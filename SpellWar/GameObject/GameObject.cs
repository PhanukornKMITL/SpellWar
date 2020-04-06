using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar.GameObject {
    class GameObject {
       
            Texture2D _texture;
            public Vector2 Position;
            public Color color;
            public float Rotation;
            public Vector2 Scale;
            public Vector2 Velocity;
            public string Name;
            public bool IsActive;
            Rectangle hitBox;


            public GameObject(Texture2D texture) {
                _texture = texture;
                Position = Vector2.Zero;
                Scale = Vector2.One;
                Rotation = 0f;
                IsActive = true;
                hitBox = new Rectangle((int)Position.X, (int)Position.Y, texture.Width, texture.Height);
            }

            public virtual void Update(GameTime gameTime, List<GameObject> gameObjects) {

            }

            public virtual void Draw(SpriteBatch spriteBatch) {

            }

            public virtual void Reset() {

            }
        }
    
}
