using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpellWar.gameObject;
using System;
using System.Collections.Generic;

namespace SpellWar {

    public class MainGame : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ball; Vector2 ball2pos = Vector2.Zero; // The position of the ball in 2D space (X,Y)
        Texture2D ball2; Vector2 ball1pos = Vector2.Zero;
        Texture2D wizzard; //Vector2 wizzardPos = Vector2.Zero;
        Texture2D voodoo; //Vector2 voodooPos = Vector2.Zero;
        Texture2D background, heart;
        GameObject player1, player2;
        Vector2 coor, virtualPos;
        Texture2D rect, virtualBox, virtualShoot;
        Random sideRand;
        int side;
        KeyboardState kBState;
        SpriteFont gameFont;
        double timer =2D;
        bool ballVisible, ball2Visible, virtualVisible, virtualShootVisible;
        GameObject voBall, wizBall;
        List<GameObject> gameObjects;
        float[] leftAngle,rightAngle;
        bool _isDecreaseHealth, canWalk;
        int count;
        


        public MainGame() {
            graphics = new GraphicsDeviceManager(this);
            // Set the window height and width
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 920;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize() {

            gameObjects = new List<GameObject>();
            timer = 0;
            Singleton.Instance.leftSideMove = 2;
            Singleton.Instance.rightSideMove = 2;
            sideRand = new Random();
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

            //Console.WriteLine(side);

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
            wizzard = Content.Load<Texture2D>("wizzard");
            voodoo = Content.Load<Texture2D>("voodoo");
            heart = Content.Load<Texture2D>("heart");









            player1 = new Player(voodoo) {
                Name = "Player1",
                Health = 3,
                WalkSlot = 1,
                Power = 1
            };
            player2 = new Player(wizzard) {
                Name = "Player2",
                Health = 3,
                WalkSlot = 1,
                Power = 1
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
        int kState = 0;
        double v, vx, vy, alpha, t2 = 0;
        //----------------------------------------------------------------------//
        protected override void Update(GameTime gameTime) {


            if(timer > 0)
            timer -= gameTime.ElapsedGameTime.TotalSeconds;
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



                //Turn Left To Shoot
                if (Singleton.Instance.isLeftTurn == true) {
                    

                    //right will move first
                    Singleton.Instance.CurrentKey = Keyboard.GetState();
                   
                    virtualVisible = true;
                    if (!Singleton.Instance.isRightMove) {
                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {


                            if (Singleton.Instance.rightSideMove > 0 && player2.WalkSlot + (count - 1) >= 0) {
                                Singleton.Instance.rightSideMove--;
                                count--;
                                
                            }

                           

                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (Singleton.Instance.rightSideMove < 4 && player2.WalkSlot - (count + 1) >= 0) {
                                Singleton.Instance.rightSideMove++;
                                count++;
                                
                            }

                            
                            

                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Enter) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey) || timer <= 0) {
                            //After right move

                            Singleton.Instance.isRightMove = true;
                        }
                    }


                    //Left Side Can Shoot Now
                    else if (Singleton.Instance.isRightMove) {
                        virtualVisible = false;
                        Console.WriteLine("leftshoot"+" "+timer);

                        if (!Singleton.Instance.leftChooseShoot) {
                            virtualShootVisible = true;


                            if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                                if (Singleton.Instance.rightSideShoot > 0) {
                                    Singleton.Instance.rightSideShoot--;
                                }

                            }

                            if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey) ) {

                                if (Singleton.Instance.rightSideShoot < 4) {
                                    Singleton.Instance.rightSideShoot++;
                                }

                            }

                            if ((Singleton.Instance.CurrentKey.IsKeyDown(Keys.Enter) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) || timer <= 0) {
                                //After left move
                                Singleton.Instance.leftChooseShoot = true;
                                virtualShootVisible = false;
                                kState = 1; v = -820;
                                // rightAngle

                                alpha = Singleton.Instance.shootPosRight[Singleton.Instance.rightSideShoot];


                                Console.WriteLine(alpha);
                                vx = v * Math.Cos(alpha);
                                vy = v * Math.Sin(alpha);


                                player2.Position = new Vector2(Singleton.Instance.rightArea[Singleton.Instance.rightSideMove], graphics.GraphicsDevice.Viewport.Height - 170);

                            }

                        }
                 }
                    if (timer <= 0) {
                        Singleton.Instance.isRightMove = true;
                        timer = 3;
                    }



                }
                //Turn Right To Shoot
                else {
                    
                    virtualVisible = true;
                    //Left will move first
                    Singleton.Instance.CurrentKey = Keyboard.GetState();
                    //ball1pos.X = leftArea[1];
                   

                    if (!Singleton.Instance.isLeftMove) {


                        Console.WriteLine(count);

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {


                           

                            if (Singleton.Instance.leftSideMove > 0  && player1.WalkSlot + (count - 1) >= 0) {

                                
                                    Singleton.Instance.leftSideMove--;
                                    count--;
                                
                               
   
                            }


                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            
                            if (Singleton.Instance.leftSideMove < 4 && player1.WalkSlot - (count + 1) >= 0) {
                              
                                     Singleton.Instance.leftSideMove++;
                                     count++; 
                                
                                                
                            }

                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Enter) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {
                            //After left move
                            Singleton.Instance.isLeftMove = true;
                            
                        }

                        if (timer <= 0) {
                            Singleton.Instance.isLeftMove = true;
                            timer = 3;
                        }
                    }


                    else if (Singleton.Instance.isLeftMove) {
                        
                        virtualVisible = false;

                        //Right Side Can Shoot Now

                        if (!Singleton.Instance.rightChooseShoot) {
                            virtualShootVisible = true;


                            if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                                if (Singleton.Instance.leftSideShoot > 0 ) {
                                    Singleton.Instance.leftSideShoot--;
                                    
                                }

                            }

                            if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                                if (Singleton.Instance.leftSideShoot < 4) {
                                    Singleton.Instance.leftSideShoot++;
                                }

                            }

                            if ((Singleton.Instance.CurrentKey.IsKeyDown(Keys.Enter) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) || timer <= 0 ) {
                                //After left move
                                Singleton.Instance.rightChooseShoot = true;
                                virtualShootVisible = false;

                                kState = 2; v = -820;

                                //leftAngle

                                alpha = Singleton.Instance.shootPosLeft[Singleton.Instance.leftSideShoot];





                                Console.WriteLine(alpha);
                                vx = v * Math.Cos(alpha);
                                vy = v * Math.Sin(alpha);

                                player1.Position = new Vector2(Singleton.Instance.leftArea[Singleton.Instance.leftSideMove], graphics.GraphicsDevice.Viewport.Height - 170);


                            }

                        }
                        
                       
                    }

                }


                //Projectile Part
                if (kState == 1) {


                    voBall.Position = new Vector2( (float)((vx * -1) * t2) + player1.Position.X, (float)(vy * t2 + g * t2 * t2 / 2) + (player2.Position.Y) - ball.Height); 
                   
                    t2 = t2 + gameTime.ElapsedGameTime.TotalSeconds;
                    ballVisible = true;
                }

                //Right to left side
                if (kState == 2) {
                    wizBall.Position = new Vector2((float)((vx) * t2) + player2.Position.X, (float)(vy * t2 + g * t2 * t2 / 2) + (player1.Position.Y) - ball.Height); 
                    

                    t2 = t2 + gameTime.ElapsedGameTime.TotalSeconds;
                    ball2Visible = true;
                }


                if (wizBall.Position.Y > graphics.GraphicsDevice.Viewport.Height - ball.Height) {
                    wizBall.Position = new Vector2(wizBall.Position.X, graphics.GraphicsDevice.Viewport.Height - ball.Height); 
                    kState = 0;
                    t2 = 0;
                    Singleton.Instance.isRightTurn = false;
                    Singleton.Instance.isLeftTurn = true;
                    Reset();
                }

                if (voBall.Position.Y > graphics.GraphicsDevice.Viewport.Height - ball2.Height) {
                    voBall.Position = new Vector2( voBall.Position.X, graphics.GraphicsDevice.Viewport.Height - ball2.Height); 
                    kState = 0;
                    t2 = 0;
                    Singleton.Instance.isLeftTurn = false;
                    Singleton.Instance.isRightTurn = true;
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

            base.Update(gameTime);
        }

        public void Reset() {

            voBall = new Ball(ball2);
            wizBall = new Ball(ball);
            gameObjects.Add(voBall);
            gameObjects.Add(wizBall);
            virtualPos = Vector2.Zero;
            Singleton.Instance.isRightMove = false;
            Singleton.Instance.isLeftMove = false;
            Singleton.Instance.leftSideMove = 2;
            Singleton.Instance.rightSideMove = 2;
            count = 0;
            timer = 30;
            canWalk = true;
            ballVisible = false;
            ball2Visible = false;
            virtualVisible = false;
            virtualShootVisible = false;
            _isDecreaseHealth = false;
            Singleton.Instance.rightChooseShoot = false;
            Singleton.Instance.leftChooseShoot  = false;
            Singleton.Instance.leftSideShoot = 2;
            Singleton.Instance.rightSideShoot = 2;

            //Right Player
            player2.Position = new Vector2(Singleton.Instance.rightArea[2] , graphics.GraphicsDevice.Viewport.Height - 170);
            //Left Player
            player1.Position= new Vector2(Singleton.Instance.leftArea[2], graphics.GraphicsDevice.Viewport.Height - 170 );



            //set ball position according to player position this is open to change screen resolution.
            ball2pos = player2.Position;
            ball1pos = player1.Position;



        }


        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.MediumPurple);

            spriteBatch.Begin();
            spriteBatch.Draw(background, GraphicsDevice.Viewport.Bounds, Color.White);
           
            foreach(GameObject g in gameObjects) {
                g.Draw(spriteBatch);
            }

            //Draw if not collide
            if (!isCollision(voBall,player2, player1.Power)) {
                
                spriteBatch.Draw(wizzard, player2.Position, Color.White);
            }


            if (!isCollision(wizBall, player1, player2.Power)) {
               
                spriteBatch.Draw(voodoo, player1.Position, Color.White);
            }


            //Draw Heart
            for (int i = 0; i < player1.Health; i++) {
                spriteBatch.Draw(heart, new Vector2(3 + i * heart.Width,3 ), Color.White);
            }
            

            for (int i = 0; i < player2.Health; i++) {
                spriteBatch.Draw(heart, new Vector2((graphics.PreferredBackBufferWidth - heart.Width) - (i * heart.Width) , 3), Color.White);
            }




                spriteBatch.DrawString(gameFont, "" + (Math.Floor(timer) +1), new Vector2(graphics.PreferredBackBufferWidth / 2, 20), Color.Red);

            if (ballVisible) {
                spriteBatch.Draw(ball, voBall.getRect, Color.White);
            }
            else if(ball2Visible){
                spriteBatch.Draw(ball2, wizBall.getRect, Color.White);
            }
            
           
            spriteBatch.Draw(rect, coor, Color.White);
            if (virtualVisible) {
                if (Singleton.Instance.isLeftTurn) {
                    spriteBatch.Draw(virtualBox, new Vector2(Singleton.Instance.rightArea[Singleton.Instance.rightSideMove] , 700), Color.White * 0.5f);
                }
                else if (Singleton.Instance.isRightTurn) {
                    spriteBatch.Draw(virtualBox, new Vector2(Singleton.Instance.leftArea[Singleton.Instance.leftSideMove], 700), Color.White * 0.5f);
                }
                 
            }
            if (virtualShootVisible) {
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

       public bool isCollision(GameObject obj1, GameObject obj2,int power) {
            if (obj1.getRect.Intersects(obj2.getRect) && _isDecreaseHealth == false) {
                obj2.Health -= power;
                obj1 = null;
             
                _isDecreaseHealth = true;

                return true;
            }
            else {
                return false;
            }
        }

        


      

    }
}
