using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        Vector2 player2, player1;
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
                rightArea[i] = (((graphics.PreferredBackBufferWidth / 2) / 5) * (i + 5)) + 30;
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


            Reset();
            base.Initialize();
        }


        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ball = Content.Load<Texture2D>("yellowball");
            ball2 = Content.Load<Texture2D>("yellowball");
            gameFont = Content.Load<SpriteFont>("gfont");
            background = Content.Load<Texture2D>("background");

            //อันนี้ตัวอย่างใส่ตัวละครนะ ก็คือ วาดใส่ตรงนี้ได้เลย แทน wizzard
            wizzard = Content.Load<Texture2D>("wizzard");
            voodoo = Content.Load<Texture2D>("voodoo");
            //ballpos.X = graphics.PreferredBackBufferWidth  - ball.Width; // Place the ball in the middle
            //ballpos.Y = graphics.GraphicsDevice.Viewport.Height - ball.Height; // Place the ball on the bottom side of the window | the ground
            //ballpos.Y = 500;
            //ball2pos.Y = graphics.GraphicsDevice.Viewport.Height - 148;
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


            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            
            Console.WriteLine(timer+ " "+ gameTime.ElapsedGameTime.TotalSeconds);

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

                            Console.WriteLine(rightSideMove);
                            //player2.X = rightArea[rightSideMove];
                            //ball2pos = player2;
                            
                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (rightSideMove < 4) {
                                rightSideMove++;
                            }

                            Console.WriteLine(rightSideMove);
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

                        //Left Side
                        if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                            kState = 1; v = -820;
                            alpha = MathHelper.ToRadians(68f); // the angle at which the object is thrown (measured in radians)
                            vx = v * Math.Cos(alpha);
                            vy = v * Math.Sin(alpha);
                            

                        player2.X = rightArea[rightSideMove];

                        }

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

                            
                            //ball1pos.X = leftArea[leftSideMove];
                            //player1.X = leftArea[leftSideMove];
                            //ball1pos = player1;
                           

                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (leftSideMove < 4) {
                                leftSideMove++;
                            }

                            //Console.WriteLine(leftSideMove);
                            //ball1pos.X = leftArea[leftSideMove];
                            //player1.X = leftArea[leftSideMove];
                            //ball1pos = player1;
                           
                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Enter) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {
                            //After left move
                            Singleton.Instance.isLeftMove = true;
                        }
                    }


                    else if (Singleton.Instance.isLeftMove) {
                        virtualVisible = false;
                        //Right Side Can Shoot Now
                        if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                            kState = 2; v = -820;
                            // position 3
                            //alpha = MathHelper.ToRadians(68f); // the angle at which the object is thrown (measured in radians)   
                            alpha = MathHelper.ToRadians(75f);
                            vx = v * Math.Cos(alpha);
                            vy = v * Math.Sin(alpha);

                            player1.X = leftArea[leftSideMove];


                        }
                    }

                }


                if (kState == 1) {


                    ball1pos.Y = (float)(vy * t2 + g * t2 * t2 / 2) + (player2.Y) - ball.Height;
                    ball1pos.X = (float)((vx * -1) * t2) + player1.X;
                    t2 = t2 + gameTime.ElapsedGameTime.TotalSeconds;

                    ballVisible = true;
                }

                //Right to left side
                if (kState == 2) {
                    ball2pos.Y = (float)(vy * t2 + g * t2 * t2 / 2) + (player1.Y) - ball.Height;
                    ball2pos.X = (float)((vx) * t2) + player2.X;

                    t2 = t2 + gameTime.ElapsedGameTime.TotalSeconds;
                    ball2Visible = true;
                }


                if (ball2pos.Y > graphics.GraphicsDevice.Viewport.Height - ball.Height) {
                    ball2pos.Y = graphics.GraphicsDevice.Viewport.Height - ball.Height;
                    kState = 0;
                    t2 = 0;
                    Singleton.Instance.isRightTurn = false;
                    Singleton.Instance.isLeftTurn = true;
                    Reset();
                }

                if (ball1pos.Y > graphics.GraphicsDevice.Viewport.Height - ball2.Height) {
                    ball1pos.Y = graphics.GraphicsDevice.Viewport.Height - ball2.Height;
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

            virtualPos = Vector2.Zero;
            Singleton.Instance.isRightMove = false;
            Singleton.Instance.isLeftMove = false;
            leftSideMove = 2;
            rightSideMove = 2;
            timer = 60;
            ballVisible = false;
            ball2Visible = false;
            virtualVisible = false;

            //Right Player
            player2 = new Vector2(rightArea[2] + 60, graphics.GraphicsDevice.Viewport.Height - 170);
            //Left Player
            player1 = new Vector2(leftArea[2], graphics.GraphicsDevice.Viewport.Height - 170 );



            //set ball position according to player position this is open to change screen resolution.
            ball2pos = player2;
            ball1pos = player1;



        }


        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.MediumPurple);

            spriteBatch.Begin();
            spriteBatch.Draw(background, GraphicsDevice.Viewport.Bounds, Color.White);
            
            spriteBatch.Draw(voodoo, player1, Color.White);
            spriteBatch.Draw(wizzard, player2, Color.White);
            spriteBatch.DrawString(gameFont, "" + Math.Floor(timer), new Vector2(graphics.PreferredBackBufferWidth / 2, 20), Color.Red);

            if (ballVisible) {
                spriteBatch.Draw(ball, ball1pos, Color.White);
            }
            else if(ball2Visible){
                spriteBatch.Draw(ball2, ball2pos, Color.White);
            }
            
           
            spriteBatch.Draw(rect, coor, Color.White);
            if (virtualVisible) {
                if (Singleton.Instance.isLeftTurn) {
                    spriteBatch.Draw(virtualBox, new Vector2(rightArea[rightSideMove], 700), Color.White * 0.5f);
                }
                else if (Singleton.Instance.isRightTurn) {
                    spriteBatch.Draw(virtualBox, new Vector2(leftArea[leftSideMove], 700), Color.White * 0.5f);
                }
            }
           
            

            spriteBatch.End();

            base.Draw(gameTime);
        }

      

    }
}
