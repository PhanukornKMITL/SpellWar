using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar {
    class Singleton {

      public  enum GameState {
            ISPLAYING, PAUSE
        }

        public bool isLeftTurn, isRightTurn, isLeftMove = false, isRightMove = false;
        public KeyboardState PreviousKey, CurrentKey;

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


    }
}
