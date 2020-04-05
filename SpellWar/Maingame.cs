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
        Vector2 player2, player1;
        Vector2 coor;
        Texture2D rect;
        float[] leftArea, rightArea;
        int leftSideMove, rightSideMove;
        Random sideRand;
        int side;
        KeyboardState kBState;


        public MainGame() {
            graphics = new GraphicsDeviceManager(this);
            // Set the window height and width
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 920;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize() {

            leftSideMove = 2;
            rightSideMove = 2;
            sideRand = new Random();
            leftArea = new float[5];
            rightArea = new float[5];
            rect = new Texture2D(graphics.GraphicsDevice, 30, 500);
            Color[] data = new Color[30 * 500];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
            rect.SetData(data);

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
            //wizzard = Content.Load<Texture2D>("wizzard");
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




            if(Singleton.Instance.gameState == Singleton.GameState.ISPLAYING) {

                // Allows the game to exit
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                    this.Exit();



                //Turn Left To Shoot
                if (Singleton.Instance.isLeftTurn == true) {

                    //right will move first
                    Singleton.Instance.CurrentKey = Keyboard.GetState();
                    if (!Singleton.Instance.isRightMove) {
                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (leftSideMove > 0) {
                                leftSideMove--;
                            }

                            Console.WriteLine(leftSideMove);
                            ball2pos.X = rightArea[leftSideMove];
                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (leftSideMove < 4) {
                                leftSideMove++;
                            }

                            Console.WriteLine(leftSideMove);
                            ball2pos.X = rightArea[leftSideMove];
                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Enter) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {
                            //After right move
                            Singleton.Instance.isRightMove = true;
                        }
                    }
                    

                    //Left Side Can Shoot Now
                    else if (Singleton.Instance.isRightMove) {

                        //Left Side
                        if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                            kState = 1; v = -820;
                            alpha = MathHelper.ToRadians(68f); // the angle at which the object is thrown (measured in radians)
                            vx = v * Math.Cos(alpha);
                            vy = v * Math.Sin(alpha);
                        }

                    }

                

                }
                //Turn Right To Shoot
                else {


                    //Left will move first
                    Singleton.Instance.CurrentKey = Keyboard.GetState();
                    //ball1pos.X = leftArea[1];

                    if (!Singleton.Instance.isLeftMove) {
                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (leftSideMove > 0) {
                                leftSideMove--;
                            }

                            Console.WriteLine(leftSideMove);
                            ball1pos.X = leftArea[leftSideMove];
                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (leftSideMove < 4) {
                                leftSideMove++;
                            }

                            Console.WriteLine(leftSideMove);
                            ball1pos.X = leftArea[leftSideMove];
                        }

                        if (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Enter) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {
                            //After left move
                            Singleton.Instance.isLeftMove = true;
                        }
                    }
                   

                    if (Singleton.Instance.isLeftMove) {
                        //Right Side Can Shoot Now
                        if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                            kState = 2; v = -820;
                            // position 3
                            //alpha = MathHelper.ToRadians(68f); // the angle at which the object is thrown (measured in radians)   
                            alpha = MathHelper.ToRadians(75f);
                            vx = v * Math.Cos(alpha);
                            vy = v * Math.Sin(alpha);

                        }
                    }

                }

 
                if (kState == 1) {


                    ball1pos.Y = (float)(vy * t2 + g * t2 * t2 / 2) + (player2.Y) - ball.Height;
                    ball1pos.X = (float)((vx * -1) * t2) + player1.X;
                    t2 = t2 + gameTime.ElapsedGameTime.TotalSeconds;
                }

                //Right to left side
                if (kState == 2) {
                    ball2pos.Y = (float)(vy * t2 + g * t2 * t2 / 2) + (player1.Y) - ball.Height;
                    ball2pos.X = (float)((vx) * t2) + player2.X;

                    t2 = t2 + gameTime.ElapsedGameTime.TotalSeconds;
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


            Singleton.Instance.isRightMove = false;
            Singleton.Instance.isLeftMove = false;

            //Right Player
            player2 = new Vector2(rightArea[2] + 60, graphics.GraphicsDevice.Viewport.Height - 200);
            //Left Player
            player1 = new Vector2(leftArea[2], graphics.GraphicsDevice.Viewport.Height - 200);
            
            //set ball position according to player position this is open to change screen resolution.
            ball2pos = player2;
            ball1pos = player1;



        }


        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.MediumPurple);

            spriteBatch.Begin();
            spriteBatch.Draw(ball, ball2pos, Color.White);

            spriteBatch.Draw(ball2, ball1pos, Color.White);
            spriteBatch.Draw(rect, coor, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
