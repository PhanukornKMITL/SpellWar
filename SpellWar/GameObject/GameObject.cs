using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpellWar.gameObject.component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar.gameObject {
   public class GameObject {
       
            Texture2D _texture;
             Vector2 position;
            public Color color;
            public float Rotation;
            public Vector2 Scale;
            public Vector2 Velocity;
            public string Name;
            public bool IsActive;
            Rectangle hitBox;
            private int health;
            private int walkSlot;
            private int power;
            public InputComponent Input;
            public PhysicsComponent Physics;
            public GraphicsComponent Graphics;


        public GameObject(Texture2D texture) {
                _texture = texture;
                position = Vector2.Zero;
                Scale = Vector2.One;
                Rotation = 0f;
                IsActive = true;
                hitBox = new Rectangle((int)position.X, (int)position.Y, texture.Width - 30, texture.Height - 30);
            }


            public GameObject(InputComponent input, PhysicsComponent physics, GraphicsComponent graphics) {
                Input = input;
                Physics = physics;
                Graphics = graphics;
                position = Vector2.Zero;
                Scale = Vector2.One;
                Rotation = 0f;
                IsActive = true;
           
        }

            public virtual void Update(GameTime gameTime, List<GameObject> gameObjects) {
            if (Input != null) {
                Input.Update(gameTime, gameObjects, this);
            }
            if (Physics != null) {
                Physics.Update(gameTime, gameObjects, this);
            }
            if (Graphics != null) {
                Graphics.Update(gameTime, gameObjects, this);
            }
        }

            public virtual void Draw(SpriteBatch spriteBatch) {
            if (Graphics != null) {
                Graphics.Draw(spriteBatch, this);
            }
        }

            public virtual void Reset() {
            if (Input != null) {
                Input.Reset();
            }
            if (Physics != null) {
                Physics.Reset();
            }
            if (Graphics != null) {
                Graphics.Reset();
            }
           }

        public void SendMessage(int message, Component sender) {
            //to broadCast message to all components    
            if (Input != null) {
                Input.ReceiveMessage(message, sender);
            }
            if (Physics != null) {
                Physics.ReceiveMessage(message, sender);
            }
            if (Graphics != null) {
                Graphics.ReceiveMessage(message, sender);
            }
        }

        public virtual void Action(GameObject obj1, GameObject obj2) {

            }

        public Vector2 Position {
            get { return position; }
            set { position = value; hitBoxPosition(position); }
        }

        public void hitBoxPosition(Vector2 position) {
            hitBox.X = (int)position.X;
            hitBox.Y = (int)position.Y;
        }

        public Rectangle getRect {
            get { return hitBox; }
            set { hitBox = value; }
        }
        public int Health {
            get { return health; }
            set { health = value; }
        }
        public int WalkSlot {
            get { return walkSlot; }
            set { walkSlot = value; }

        }

        public int Power {
            get { return power; }
            set { power = value; }

        }


    }

}
