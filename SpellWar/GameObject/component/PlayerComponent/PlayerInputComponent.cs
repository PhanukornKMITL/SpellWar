using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar.gameObject.component.PlayerComponent {
    class PlayerInputComponent : InputComponent {



        public PlayerInputComponent(Game currentScene) : base(currentScene) {

        }

        public override void ChangeMappingKey(string Key, Keys newInput) {
            base.ChangeMappingKey(Key, newInput);
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent) {
            base.Draw(spriteBatch, parent);
        }

        public override void ReceiveMessage(int message, Component sender) {
            base.ReceiveMessage(message, sender);
        }

        public override void Reset() {


        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent) {

           
            //Turn Left To Shoot
            if (Singleton.Instance.isLeftTurn) {
                


                //right will move first
                Singleton.Instance.CurrentKey = Keyboard.GetState();
               

                Singleton.Instance.virtualVisible = true;

                if (!Singleton.Instance.isRightMove) {
                    
                   

                    if (parent.Name.Equals("Player2") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {
                        Console.WriteLine(parent.Name);



                        if (Singleton.Instance.rightSideMove > 0 && parent.WalkSlot + (Singleton.Instance.count - 1) >= 0) {
                            Singleton.Instance.rightSideMove--;
                            Singleton.Instance.count--;

                        }

                    }

                    if (parent.Name.Equals("Player2") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                        if (Singleton.Instance.rightSideMove < 4 && parent.WalkSlot - (Singleton.Instance.count + 1) >= 0) {
                            Singleton.Instance.rightSideMove++;
                            Singleton.Instance.count++;

                        }

                    }

                    if (parent.Name.Equals("Player2") && (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Enter) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey) || Singleton.Instance.timer <= 0)) {
                        //After right move
                        Singleton.Instance.isRightMove = true;
                        if (Singleton.Instance.count < 0) {
                            parent.WalkSlot -= Singleton.Instance.count * -1;
                        }
                        else {
                            parent.WalkSlot -= Singleton.Instance.count;
                        }
                    }
                }




                //Left Side Can Shoot Now
                else if (Singleton.Instance.isRightMove) {

                    Singleton.Instance.virtualVisible = false;

                    if (!Singleton.Instance.leftChooseShoot) {
                        Singleton.Instance.virtualShootVisible = true;


                        if (parent.Name.Equals("Player1") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (Singleton.Instance.rightSideShoot > 0) {
                                Singleton.Instance.rightSideShoot--;

                            }

                        }

                        if (parent.Name.Equals("Player1") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (Singleton.Instance.rightSideShoot < 4) {
                                Singleton.Instance.rightSideShoot++;
                            }

                        }

                        if (parent.Name.Equals("Player1") && ((Singleton.Instance.CurrentKey.IsKeyDown(Keys.Up) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) || Singleton.Instance.timer <= 0)) {
                            //After left move
                            Singleton.Instance.leftChooseShoot = true;
                            Singleton.Instance.virtualShootVisible = false;
                            Singleton.Instance.kState = 1;
                            gameObjects.Single(s => s.Name.Equals("Player2")).Position = new Vector2(Singleton.Instance.rightArea[Singleton.Instance.rightSideMove], 920 - 170);

                        }

                    }
                }
                if (Singleton.Instance.timer <= 0) {
                    Singleton.Instance.isRightMove = true;
                    Singleton.Instance.timer = 3;
                }



            }

            //============ Turn Right To Shoot ================ 
            else if (Singleton.Instance.isRightTurn) {


                Singleton.Instance.virtualVisible = true;

                //Left will move first
                Singleton.Instance.CurrentKey = Keyboard.GetState();

                if (!Singleton.Instance.isLeftMove) {


                    if (parent.Name.Equals("Player1") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {
                        Console.WriteLine(parent.Name);

                        if (Singleton.Instance.leftSideMove > 0 && parent.WalkSlot + (Singleton.Instance.count - 1) >= 0) {


                            Singleton.Instance.leftSideMove--;
                            Singleton.Instance.count--;

                        }


                    }


                    if (parent.Name.Equals("Player1") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {
                        Console.WriteLine(parent.Name);

                        if (Singleton.Instance.leftSideMove < 4 && parent.WalkSlot - (Singleton.Instance.count + 1) >= 0) {

                            Singleton.Instance.leftSideMove++;
                            Singleton.Instance.count++;
                        }

                    }

                    if (parent.Name.Equals("Player1") && (Singleton.Instance.CurrentKey.IsKeyDown(Keys.Enter) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey) || Singleton.Instance.timer <= 0)) {
                        //After left move
                        Singleton.Instance.isLeftMove = true;

                        if (Singleton.Instance.count < 0) {
                            parent.WalkSlot -= Singleton.Instance.count * -1;
                        }
                        else {
                            parent.WalkSlot -= Singleton.Instance.count;
                        }
                        
                    }


                }


                else if (Singleton.Instance.isLeftMove) {

                    Singleton.Instance.virtualVisible = false;

                    //Right Side Can Shoot Now

                    if (!Singleton.Instance.rightChooseShoot) {

                        Singleton.Instance.virtualShootVisible = true;

                        if (parent.Name.Equals("Player2") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Left) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (Singleton.Instance.leftSideShoot > 0) {
                                Singleton.Instance.leftSideShoot--;

                            }

                        }

                        if (parent.Name.Equals("Player2") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Right) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) {

                            if (Singleton.Instance.leftSideShoot < 4) {
                                Singleton.Instance.leftSideShoot++;
                            }

                        }

                        if ((parent.Name.Equals("Player2") && Singleton.Instance.CurrentKey.IsKeyDown(Keys.Up) && !Singleton.Instance.CurrentKey.Equals(Singleton.Instance.PreviousKey)) || Singleton.Instance.timer <= 0) {
                            //After left move
                            //Console.WriteLine(this.Name);

                            Singleton.Instance.rightChooseShoot = true;
                            Singleton.Instance.virtualShootVisible = false;
                            Singleton.Instance.kState = 2;
                            gameObjects.Single(s => s.Name.Equals("Player1")).Position = new Vector2(Singleton.Instance.leftArea[Singleton.Instance.leftSideMove], 920 - 170);


                        }

                    }

                }
                if (Singleton.Instance.timer <= 0) {
                    Singleton.Instance.isLeftMove = true;
                    Singleton.Instance.timer = 3;
                }




            }


        }
    }
}
