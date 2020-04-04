using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace SpellWar {

    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D ball; Vector2 ballpos = Vector2.Zero; // The position of the ball in 2D space (X,Y)
        Texture2D ball2; Vector2 ball2pos = Vector2.Zero;
        Texture2D wizzard; Vector2 wizzardPos = Vector2.Zero;
        Vector2 player1, player2;
        Vector2 coor;
        Texture2D rect;
        float[] leftArea, rightArea;


        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            // Set the window height and width
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 920;
            Content.RootDirectory = "Content";
        }


        protected override void Initialize() {



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
                rightArea[i] = ((graphics.PreferredBackBufferWidth / 2) / 5) * (i + 5);
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



            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();



            //Left Side
            else if (Keyboard.GetState().IsKeyDown(Keys.Right)) {
                kState = 1; v = -820;
                alpha = MathHelper.ToRadians(68f); // the angle at which the object is thrown (measured in radians)
                vx = v * Math.Cos(alpha);
                vy = v * Math.Sin(alpha);
            }

            //Right Side
            if (Keyboard.GetState().IsKeyDown(Keys.Left)) {
                kState = 2; v = -820;
                // position 3
                //alpha = MathHelper.ToRadians(68f); // the angle at which the object is thrown (measured in radians)   
                alpha = MathHelper.ToRadians(75f);
                vx = v * Math.Cos(alpha);
                vy = v * Math.Sin(alpha);

            }

            if (kState == 1) {


                ball2pos.Y = (float)(vy * t2 + g * t2 * t2 / 2) + (player1.Y) - ball.Height;
                ball2pos.X = (float)((vx * -1) * t2) + player2.X;
                t2 = t2 + gameTime.ElapsedGameTime.TotalSeconds;
            }

            //Right to left side
            if (kState == 2) {
                ballpos.Y = (float)(vy * t2 + g * t2 * t2 / 2) + (player2.Y) - ball.Height;
                ballpos.X = (float)((vx) * t2) + player1.X;

                t2 = t2 + gameTime.ElapsedGameTime.TotalSeconds;
            }


            if (ballpos.Y > graphics.GraphicsDevice.Viewport.Height - ball.Height) {
                ballpos.Y = graphics.GraphicsDevice.Viewport.Height - ball.Height;
                kState = 0;
                t2 = 0;
                Reset();
            }

            if (ball2pos.Y > graphics.GraphicsDevice.Viewport.Height - ball2.Height) {
                ball2pos.Y = graphics.GraphicsDevice.Viewport.Height - ball2.Height;
                kState = 0;
                t2 = 0;
                Reset();
            }




            base.Update(gameTime);
        }


        public void Reset() {

            //Right Player
            player1 = new Vector2(rightArea[2] + 60, graphics.GraphicsDevice.Viewport.Height - 200);
            //Left Player
            player2 = new Vector2(leftArea[2], graphics.GraphicsDevice.Viewport.Height - 200);

            //set ball position according to player position this is open to change screen resolution.
            ballpos = player1;
            ball2pos = player2;



        }


        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.MediumPurple);

            spriteBatch.Begin();
            spriteBatch.Draw(ball, ballpos, Color.White);

            spriteBatch.Draw(ball2, ball2pos, Color.White);
            spriteBatch.Draw(rect, coor, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
