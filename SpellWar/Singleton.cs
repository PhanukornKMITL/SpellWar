using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using SpellWar.gameObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar {
    class Singleton {

      public  enum GameState {
            ISPLAYING, PAUSE, PLAYER1_WIN, PLAYER2_WIN
        }

        public float[] shootPosLeft, shootPosRight;
        public float[] leftArea, rightArea;
        public int leftSideMove, rightSideMove, leftSideShoot = 2, rightSideShoot = 2;
        public bool isLeftTurn, isRightTurn, isLeftMove = false, isRightMove = false, leftChooseShoot = false, rightChooseShoot = false;
        public KeyboardState PreviousKey, CurrentKey;
        public double timer = 2D;
        public int count, turnCount = 0, turn = 0;
        public bool ballVisible, ball2Visible, virtualVisible, virtualShootVisible, isDecreaseHealth,endTurn;
        public int kState = 0;
       

        public GameState gameState;

        private static Singleton instance;
    
        public static Singleton Instance {
            get {
                if (instance == null) {
                    instance = new Singleton();
                    
                }
                return instance;
            }
        }

        public Singleton() {
          leftArea = new float[5];
          rightArea = new float[5];
          shootPosLeft = new float[5];
          shootPosRight = new float[5];


            //leftAngle
            shootPosLeft[0] = MathHelper.ToRadians(62f);
            shootPosLeft[1] = MathHelper.ToRadians(70f);
            shootPosLeft[2] = MathHelper.ToRadians(73f);
            shootPosLeft[3] = MathHelper.ToRadians(77.025f);
            shootPosLeft[4] = MathHelper.ToRadians(79.97f);

            shootPosRight[0] = MathHelper.ToRadians(77.7f);
            shootPosRight[1] = MathHelper.ToRadians(77.025f);
            shootPosRight[2] = MathHelper.ToRadians(70f);
            shootPosRight[3] = MathHelper.ToRadians(66.67f);
            shootPosRight[4] = MathHelper.ToRadians(61f);


            


        }

        public bool isCollision(GameObject obj1, GameObject obj2, int power) {
            if (obj1.getRect.Intersects(obj2.getRect) && isDecreaseHealth == false) {
                obj2.Health -= power;
                obj1 = null;
                
                isDecreaseHealth = true;

                return true;
            }
            else {
                return false;
            }
        }



    }
}
