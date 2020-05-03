using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpellWar.gameObject;
using SpellWar.gameObject.component;
using SpellWar.gameObject.component.BallComponent;
using SpellWar.gameObject.component.ItemComponent;
using SpellWar.gameObject.component.PlayerComponent;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace SpellWar {

    public class MainGame : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ball; Vector2 ball2pos = Vector2.Zero; // The position of the ball in 2D space (X,Y)
        Texture2D ball2; Vector2 ball1pos = Vector2.Zero;
        Texture2D wizzard; //Vector2 wizzardPos = Vector2.Zero;
        Texture2D voodoo; //Vector2 voodooPos = Vector2.Zero;
        Texture2D background, heart;
        GameObject player1, player2,item1,item2;
        Vector2 coor, virtualPos;
        Texture2D rect, virtualBox, virtualShoot;
        Random sideRand,itemRand;
        int side;
        KeyboardState kBState;
        SpriteFont gameFont;
        
        
        GameObject voBall, wizBall;
        List<GameObject> gameObjects;
        float[] leftAngle,rightAngle;
        bool  canWalk;
        
        

        

        public MainGame() {
            graphics = new GraphicsDeviceManager(this);
            // Set the window height and width
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 920;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize() {

            gameObjects = new List<GameObject>();
            Singleton.Instance.timer = 0;
            Singleton.Instance.leftSideMove = 2;
            Singleton.Instance.rightSideMove = 2;
            sideRand = new Random();
            itemRand = new Random();
            leftAngle = new float[5];
            rightAngle = new float[5];
          

            rect = new Texture2D(graphics.GraphicsDevice, 30, 450);
            virtualBox = new Texture2D(graphics.GraphicsDevice, 160, 300);
            virtualShoot = new Texture2D(graphics.GraphicsDevice, 160, 300);
            Color[] data = new Color[30 * 450];
            Color[] color = new Color[160 * 300];
            Color[] color2 = new Color[160 * 300];
            for (int i = 0; i < data.Length; ++i) {
                data[i] = Color.Chocolate;

            }
            rect.SetData(data);
            for (int i = 0; i < color.Length; ++i) {
                color[i] = Color.Green;
            }
            virtualBox.SetData(color);

            for (int i = 0; i < color.Length; ++i) {
                color2[i] = Color.Red;
            }
            virtualShoot.SetData(color2);

            coor = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.GraphicsDevice.Viewport.Height - 400);

            //Initialize Partition of Area
            for (int i = 0; i < Singleton.Instance.leftArea.Length; i++) {
                Singleton.Instance.leftArea[i] = ((graphics.PreferredBackBufferWidth / 2) / 5) * i;
                Singleton.Instance.rightArea[i] = (((graphics.PreferredBackBufferWidth / 2) / 5) * (i + 5));
            }
            side = sideRand.Next(0, 2);
            
            

            switch (side) {
                case 0:
                    Singleton.Instance.isLeftTurn = true;
                    break;
                case 1:
                    Singleton.Instance.isRightTurn = true;
                    break;
            }


            

            base.Initialize();
        }


        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ball = Content.Load<Texture2D>("West_ball");
            ball2 = Content.Load<Texture2D>("East_ball");
            gameFont = Content.Load<SpriteFont>("gfont");
            background = Content.Load<Texture2D>("background");

            //อันนี้ตัวอย่างใส่ตัวละครนะ ก็คือ วาดใส่ตรงนี้ได้เลย แทน wizzard
            wizzard = Content.Load<Texture2D>("WizSprite_183");
            voodoo = Content.Load<Texture2D>("VooSprite_183");
            heart = Content.Load<Texture2D>("heart");



            
          /*  player1 = new Player(voodoo, heart) {
                Name = "Player1",
                Health = 3,
                WalkSlot = 2,
                Power = 1,
                getRect = new Rectangle((int)Singleton.Instance.leftArea[2], 920, 183, 183)

        };
          
            player2 = new Player(wizzard, heart) {
                Name = "Player2",
                Health = 3,
                WalkSlot = 2,
                Power = 1,
                getRect = new Rectangle((int)Singleton.Instance.rightArea[2], 920, 183, 183)
            };*/
            

            player1 = new GameObject(new PlayerInputComponent(this), new PlayerPhysicsComponent(this), new PlayerGraphicsComponent(this,voodoo)) {
                Name = "Player1",
                Health = 3,
                WalkSlot = 2,
                Power = 1,
                getRect = new Rectangle((int)Singleton.Instance.leftArea[2], 920, 150, 150)
            };
            player2 = new GameObject(new PlayerInputComponent(this), new PlayerPhysicsComponent(this), new PlayerGraphicsComponent(this, wizzard)) {
                Name = "Player2",
                Health = 3,
                WalkSlot = 2,
                Power = 1,
                getRect = new Rectangle((int)Singleton.Instance.rightArea[2], 920, 150, 150)
            };

            gameObjects.Add(player1);
            gameObjects.Add(player2);

            Reset();
            Singleton.Instance.gameState = Singleton.GameState.ISPLAYING;




        }

        protected override void UnloadContent() {

        }


        double vi, t = 0; // vi - initial velocity | t - time
        double g = 520; // pixels per second squared | gravitational acceleration
        int keyState = 0;
       
        double v, vx, vy, alpha, t2 = 0;
        //----------------------------------------------------------------------//
        protected override void Update(GameTime gameTime) {
            

            if(Singleton.Instance.timer > 0)
                Singleton.Instance.timer -= gameTime.ElapsedGameTime.TotalSeconds;
            if(player1.Health <= 0) {
                Singleton.Instance.gameState = Singleton.GameState.PLAYER2_WIN;     
            }
            if (player2.Health <= 0) {
                Singleton.Instance.gameState = Singleton.GameState.PLAYER1_WIN;
            }

            if (Singleton.Instance.gameState == Singleton.GameState.ISPLAYING) {

                // Allows the game to exit
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    this.Exit();

                foreach(GameObject g in gameObjects) {
                    g.Update(gameTime, gameObjects);

                }
                
                // Check ว่าถ้าบอลตกถึงพื้นก็จะให้ สลับฝั่ง
                if (wizBall.Position.Y > graphics.GraphicsDevice.Viewport.Height - ball.Height) {
                    
                    wizBall.Position = new Vector2(wizBall.Position.X, graphics.GraphicsDevice.Viewport.Height - ball.Height);
                    Singleton.Instance.kState = 0;
                    t2 = 0;
                    Singleton.Instance.isRightTurn = false;
                    Singleton.Instance.isLeftTurn = true;
                    player2.Power = 1;
                    Reset();
                }

                if (voBall.Position.Y > graphics.GraphicsDevice.Viewport.Height - ball2.Height) {
                    
                    voBall.Position = new Vector2( voBall.Position.X, graphics.GraphicsDevice.Viewport.Height - ball2.Height);
                    Singleton.Instance.kState = 0;
                    t2 = 0;
                    Singleton.Instance.isLeftTurn = false;
                    Singleton.Instance.isRightTurn = true;
                    player1.Power = 1;
                    Reset();
                    
                }
                


            }

            if(Singleton.Instance.gameState == Singleton.GameState.PLAYER1_WIN){
               //TODO When Player1 win the game...
            }
            if (Singleton.Instance.gameState == Singleton.GameState.PLAYER2_WIN){
                //TODO When Player2 win the game...
            }


            Singleton.Instance.PreviousKey = Singleton.Instance.CurrentKey;

            gameObjects.RemoveAll(g => g.IsActive == false);

            base.Update(gameTime);
        }

        public void Reset() {

            Singleton.Instance.turnCount++;
            Singleton.Instance.endTurn = false;
            /*voBall = new Ball(ball) {
                Name = "voBall"

            };
            wizBall = new Ball(ball2) {
                Name = "wizBall"

            };*/

            voBall = new GameObject(null, new BallPhysicsComponent(this), new BallGraphicsComponent(this, ball)) {
                Name = "voBall"

            };

            wizBall = new GameObject(null, new BallPhysicsComponent(this), new BallGraphicsComponent(this, ball2)) {
                Name = "wizBall"

            };


            gameObjects.Add(voBall);
            gameObjects.Add(wizBall);

          
            //สุ่มไอเท็ม
            if(Singleton.Instance.turnCount % 2 == 0) {
                int x;
                do {
                    x = itemRand.Next(0, 4);
                } while (x == 2);
                 
            int y = itemRand.Next(0,2);
            int type = itemRand.Next(0,3);
                
                if (y == 0) {
                    switch (type) {
                        case 0:
                            item1 = new GameObject(null, new ItemPhysicsComponent(this), new ItemGraphicsComponent(this, ball)) {
                                Name = "health",
                                Position = new Vector2(Singleton.Instance.leftArea[x], 800),
                                getRect = new Rectangle((int)Singleton.Instance.leftArea[x], 800, 100, 100)
                            };
                            break;
                        case 1:
                           item1 = new GameObject(null, new ItemPhysicsComponent(this), new ItemGraphicsComponent(this, ball)) {
                                Name = "walk",
                                Position = new Vector2(Singleton.Instance.leftArea[x], 800),
                                getRect = new Rectangle((int)Singleton.Instance.leftArea[x], 800, 100, 100)
                           };
                            break;
                        case 2:
                            item1 = new GameObject(null, new ItemPhysicsComponent(this), new ItemGraphicsComponent(this, ball)) {
                                Name = "power",
                                Position = new Vector2(Singleton.Instance.leftArea[x], 800),
                                getRect = new Rectangle((int)Singleton.Instance.leftArea[x], 800, 100, 100)
                            };
                            /*item1 = new powerItem(ball) {
                                Name = "power",
                                 Position = new Vector2(Singleton.Instance.leftArea[x], 800)
                            };*/



                            break;
                       
                    }
                    gameObjects.Add(item1);
                    
                }
                else {
                    switch (type) {
                        case 0:
                            item2 = new GameObject(null, new ItemPhysicsComponent(this), new ItemGraphicsComponent(this, ball)) {
                                Name = "health",
                                Position = new Vector2(Singleton.Instance.rightArea[x], 800),
                                getRect = new Rectangle((int)Singleton.Instance.rightArea[x], 800, 100, 100)
                            };

                       break;
                        case 1:
                            item2 = new GameObject(null, new ItemPhysicsComponent(this), new ItemGraphicsComponent(this, ball)) {
                                Name = "walk",
                                Position = new Vector2(Singleton.Instance.rightArea[x], 800),
                                getRect = new Rectangle((int)Singleton.Instance.rightArea[x], 800, 100, 100)
                            };
                            break;
                        case 2:
                            item2 = new GameObject(null, new ItemPhysicsComponent(this), new ItemGraphicsComponent(this, ball)) {
                                Name = "power",
                                Position = new Vector2(Singleton.Instance.rightArea[x], 800),
                                getRect = new Rectangle((int)Singleton.Instance.rightArea[x], 800, 100, 100)
                            };

                            /*item2 = new powerItem(ball) {
                                Name = "power",
                                 Position = new Vector2(Singleton.Instance.leftArea[x], 800)
                            };*/


                            break;
                       
                    }
                   
                    gameObjects.Add(item2);
                }
            }



            //Left Player
            player1.Position = new Vector2(Singleton.Instance.leftArea[2], 920 - 170);
           
                //Right Player
                player2.Position = new Vector2(Singleton.Instance.rightArea[2], 920 - 170);
            



            virtualPos = Vector2.Zero;
            Singleton.Instance.isRightMove = false;
            Singleton.Instance.isLeftMove = false;
            Singleton.Instance.leftSideMove = 2;
            Singleton.Instance.rightSideMove = 2;
            Singleton.Instance.count = 0;
            Singleton.Instance.timer = 30;
            canWalk = true;
            Singleton.Instance.ballVisible = false;
            Singleton.Instance.ball2Visible = false;
            Singleton.Instance.virtualVisible = false;
            Singleton.Instance.virtualShootVisible = false;
            Singleton.Instance.isDecreaseHealth = false;
            Singleton.Instance.rightChooseShoot = false;
            Singleton.Instance.leftChooseShoot  = false;
            Singleton.Instance.leftSideShoot = 2;
            Singleton.Instance.rightSideShoot = 2;
            player1.WalkSlot++;
            player2.WalkSlot++;


            
            //set ball position according to player position this is open to change screen resolution.
            ball2pos = player2.Position;
            ball1pos = player1.Position;

            foreach (GameObject g in gameObjects) {
                g.Reset();
            }


        }


        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.MediumPurple);

            spriteBatch.Begin();
            spriteBatch.Draw(background, GraphicsDevice.Viewport.Bounds, Color.White);

            if(item1 != null) {
                item1.Draw(spriteBatch);
            }
            if(item2 != null) {
                item2.Draw(spriteBatch);
            }
            
           
            for (int i = 0; i < player1.Health; i++) {
                    spriteBatch.Draw(heart, new Vector2(3 + i * heart.Width, 3), Color.White);
            }

            


           
            for (int i = 0; i < player2.Health; i++) {
                    spriteBatch.Draw(heart, new Vector2((1600 - heart.Width) - (i * heart.Width), 3), Color.White);
            }

            


            //Draw if not collide
            if (!Singleton.Instance.isCollision(voBall,player2, player1.Power)) {


                player2.Draw(spriteBatch);
            }


            if (!Singleton.Instance.isCollision(wizBall, player1, player2.Power)) {

                player1.Draw(spriteBatch);
            }

                spriteBatch.DrawString(gameFont, "" + (Math.Floor(Singleton.Instance.timer) +1), new Vector2(graphics.PreferredBackBufferWidth / 2, 20), Color.Red);
                spriteBatch.DrawString(gameFont, "WalkSlot " + player1.WalkSlot, new Vector2(3,100),Color.Red);
                spriteBatch.DrawString(gameFont, "WalkSlot " + player2.WalkSlot, new Vector2(1250, 100), Color.Red);

                spriteBatch.DrawString(gameFont, "Power " + player1.Power, new Vector2(3, 300), Color.Red);
                spriteBatch.DrawString(gameFont, "Power " + player2.Power, new Vector2(1250, 300), Color.Red);
          
                voBall.Draw(spriteBatch);
                
           
                wizBall.Draw(spriteBatch);
                
            
            
           
            spriteBatch.Draw(rect, coor, Color.White);
            if (Singleton.Instance.virtualVisible) {
                if (Singleton.Instance.isLeftTurn) {
                    spriteBatch.Draw(virtualBox, new Vector2(Singleton.Instance.rightArea[Singleton.Instance.rightSideMove] , 700), Color.White * 0.5f);
                }
                else if (Singleton.Instance.isRightTurn) {
                    spriteBatch.Draw(virtualBox, new Vector2(Singleton.Instance.leftArea[Singleton.Instance.leftSideMove], 700), Color.White * 0.5f);
                }
                 
            }
            if (Singleton.Instance.virtualShootVisible) {
                if (Singleton.Instance.isLeftMove && !Singleton.Instance.rightChooseShoot) {
                    spriteBatch.Draw(virtualShoot, new Vector2(Singleton.Instance.leftArea[Singleton.Instance.leftSideShoot], 700), Color.White * 0.5f);
                }
                else if (Singleton.Instance.isRightMove && !Singleton.Instance.leftChooseShoot) {
                    spriteBatch.Draw(virtualShoot, new Vector2(Singleton.Instance.rightArea[Singleton.Instance.rightSideShoot], 700), Color.White * 0.5f);
                }
            }
           
            

            spriteBatch.End();

            base.Draw(gameTime);
        }

       
        


      

    }
}
