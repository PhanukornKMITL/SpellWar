﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar.gameObject.component {
    public class PhysicsComponent : Component {
        int turn = 0;
        

        public PhysicsComponent(Game currentScene) {
        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent) {
            base.Draw(spriteBatch, parent);
        }

        public override void ReceiveMessage(int message, Component sender) {
            base.ReceiveMessage(message, sender);
        }

        public override void Reset() {


            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent) {
            base.Update(gameTime, gameObjects, parent);
        }
    }
}