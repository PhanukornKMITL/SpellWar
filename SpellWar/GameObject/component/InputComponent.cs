using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar.gameObject.component {
    public class InputComponent : Component {

        public Dictionary<string, Keys> InputList;
        public InputComponent(Game currentScene) {
            //InputList = new Dictionary<string, Keys>();
        }

        public InputComponent(Dictionary<string, Keys> inputList) {
           // InputList = inputList;
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent) {


        }
        public override void Reset() {


        }
        public virtual void ChangeMappingKey(string Key, Keys newInput) {
           // InputList[Key] = newInput;
        }
    }
}
