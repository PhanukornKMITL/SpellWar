using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpellWar.gameObject;
using System;

namespace SpellWar {

    public class MainGame : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ball; Vector2 ball2pos = Vector2.Zero; // The position of the ball in 2D space (X,Y)
        Texture2D ball2; Vector2 ball1pos = Vector2.Zero;
        Texture2D wizzard; Vector2 wizzardPos = Vector2.Zero;
        Texture2D voodoo; Vector2 voodooPos = Vector2.Zero;
        Texture2D background;
        Player player1, player2;
        Vector2 coor, virtualPos;
        Texture2D rect, virtualBox;
        float[] leftArea, rightArea;
        int leftSideMove, rightSideMove;
        Random sideRand;
        int side;
        KeyboardState kBState;
        SpriteFont gameFont;
        double timer =2D;
        bool ballVisible, ball2Visible, virtualVisible;
        GameObject voBall, wizBall;
        


        public MainGame() {
            graphics = new GraphicsDeviceManager(this);
            // Set the window height and width
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 920;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize() {

           
            timer = 0;
            leftSideMove = 2;
            rightSideMove = 2;
            sideRand = new Random();
            leftArea = new float[5];
            rightArea = new float[5];
            rect = new Texture2D(graphics.GraphicsDevice, 30, 500);
            virtualBox = new Texture2D(graphics.GraphicsDevice, 160, 300);
            Color[] data = new Color[30 * 500];
            Color[] color = new Color[160 * 300];
            for (int i = 0; i < data.Length; ++i) {
                data[i] = Color.Chocolate;

            }
            rect.SetData(data);
            for (int i = 0; i < color.Length; ++i) {
                color[i] = Color.Green;
            }
            virtualBox.SetData(color);

            coor = new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.GraphicsDevice.Viewport.Height - 500);

            //Initialize Partition of Area
            for (int i = 0; i < leftArea.Length; i++) {
                leftArea[i] = ((graphics.PreferredBackBufferWidth / 2) / 5) * i;
                rightArea[i] = (((graphics.PreferredBackBufferWidth / 2) / 5) * (i + 5));
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

            //voBall = new Rectangle((int)ball1pos.X, (int)ball1pos.Y, ball.Width, ball.Height);
            // wizBall = new Rectangle((int)ball2pos.X, (int)ball2pos.Y, ball.Width, ball.Height);

           



            player1 = new Player(voodoo);
            player2 = new Player(wizzard);
          
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


                            if (rightSideMove > 0) {
                                rightSideMove--;
                            }

                           
                            //player2.X = rightArea[rightSideMove];
                            //ball2pos = player2;

                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (rightSideMove < 4) {
                                rightSideMove++;
                            }

                            
                            //player2.X = rightArea[rightSideMove];
                            //ball2pos = player2;

                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Enter) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {
                            //After right move

                            Singleton.Instance.isRightMove = true;
                        }
                    }


                    //Left Side Can Shoot Now
                    else if (Singleton.Instance.isRightMove) {
                        virtualVisible = false;
                        Console.WriteLine("leftshoot"+" "+timer);
                        //Left Side
                        if (Keyboard.GetState().IsKeyDown(Keys.Right) || timer <= 0) {
          
                            kState = 1; v = -820;
                            alpha = MathHelper.ToRadians(68f); // the angle at which the object is thrown (measured in radians)
                            vx = v * Math.Cos(alpha);
                            vy = v * Math.Sin(alpha);


                            player2.Position = new Vector2( rightArea[rightSideMove], graphics.GraphicsDevice.Viewport.Height - 170);

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

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (leftSideMove > 0) {
                                leftSideMove--;
                            }

                         

                          

                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (leftSideMove < 4) {
                                leftSideMove++;
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
                        
                        if (Keyboard.GetState().IsKeyDown(Keys.Left) ||  timer <= 0) {
                            kState = 2; v = -820;
                           
                            //alpha = MathHelper.ToRadians(68f); // the angle at which the object is thrown (measured in radians)   
                            alpha = MathHelper.ToRadians(75f);
                            vx = v * Math.Cos(alpha);
                            vy = v * Math.Sin(alpha);

                            player1.Position = new Vector2(leftArea[leftSideMove], graphics.GraphicsDevice.Viewport.Height - 170);


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

            Singleton.Instance.PreviousKey = Singleton.Instance.CurrentKey;

            base.Update(gameTime);
        }

        public void Reset() {
            voBall = new Ball(ball2);
            wizBall = new Ball(ball);
            virtualPos = Vector2.Zero;
            Singleton.Instance.isRightMove = false;
            Singleton.Instance.isLeftMove = false;
            leftSideMove = 2;
            rightSideMove = 2;
            timer = 30;
            ballVisible = false;
            ball2Visible = false;
            virtualVisible = false;

            //Right Player
            player2.Position = new Vector2(rightArea[2] , graphics.GraphicsDevice.Viewport.Height - 170);
            //Left Player
            player1.Position= new Vector2(leftArea[2], graphics.GraphicsDevice.Viewport.Height - 170 );



            //set ball position according to player position this is open to change screen resolution.
            ball2pos = player2.Position;
            ball1pos = player1.Position;



        }


        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.MediumPurple);

            spriteBatch.Begin();
            spriteBatch.Draw(background, GraphicsDevice.Viewport.Bounds, Color.White);

            if (!isCollision(voBall.getRect,player2.getRect)) {
                spriteBatch.Draw(wizzard, player2.Position, Color.White);
            }


            if (!isCollision(wizBall.getRect, player1.getRect)) {
                spriteBatch.Draw(voodoo, player1.Position, Color.White);
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
                    spriteBatch.Draw(virtualBox, new Vector2(rightArea[rightSideMove] , 700), Color.White * 0.5f);
                }
                else if (Singleton.Instance.isRightTurn) {
                    spriteBatch.Draw(virtualBox, new Vector2(leftArea[leftSideMove], 700), Color.White * 0.5f);
                }
            }
           
            

            spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool isCollision(Rectangle obj1, Rectangle obj2) {
            if (obj1.Intersects(obj2)) {
                return true;
            }
            else {
                return false;
            }
        }

      

    }
}
