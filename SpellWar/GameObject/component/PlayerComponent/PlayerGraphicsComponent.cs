using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace SpellWar.gameObject.component.PlayerComponent {
    public class PlayerGraphicsComponent : GraphicsComponent {
        float playerSpeed = 200;
        SoundEffect _moveEffect,_atkEffect;
        SoundEffectInstance moveEffectInstance, atkEffectInstance;
        double time;

        //bool attacking = false;



        public PlayerGraphicsComponent(Game currentScene,Texture2D texture,SoundEffect moveEffect, SoundEffect atk_Effect, Vector2 postion) : base(currentScene, texture) {
            _moveEffect = moveEffect;
            _atkEffect = atk_Effect;
            animated = new AnimatedSprite(postion, texture);
            animated.addAnimation(4, 0, 0, "Stand", 183, 183, new Vector2(0, 0));
            animated.addAnimation(4, 183, 0, "Move", 183, 183, new Vector2(0, 0));
            animated.addAnimation(4, 366, 0, "Attack", 183, 183, new Vector2(0, 0));
            animated.PlayAnimation("Stand");
            moveEffectInstance = _moveEffect.CreateInstance();
            atkEffectInstance = _atkEffect.CreateInstance();




        }

        public override void Draw(SpriteBatch spriteBatch, GameObject parent) {
           animated.Draw(spriteBatch,parent.Position);
           

            
        }

        public override void ReceiveMessage(int message, Component sender) {
            base.ReceiveMessage(message, sender);
        }

        public override void Reset() {

            base.Reset();
        }

        public override void Update(GameTime gameTime, List<GameObject> gameObjects, GameObject parent) {
            //_moveEffect.Play();
           



            if (Singleton.Instance.isRightMove && Singleton.Instance.leftChooseShoot)
            {
                //p2 select box move ----> p1 select box shoot ----> p2 move
                if (parent.Name.Equals("Player2"))
                {
                    animated.PlayAnimation("Move");
                    time += gameTime.ElapsedGameTime.TotalSeconds;
                    
                    if(time > 0.5) {

                        moveEffectInstance.Play();
                        time = 0;
                    }
                    
                    
                    //Singleton.Instance.currenDir = Singleton.myDirection.move;
                }
                else if (parent.Name.Equals("Player1"))
                {
                    if (Singleton.Instance.leftChooseShoot && Singleton.Instance.P1attacking)
                    {
                        animated.PlayAnimation("Attack");
                        time += gameTime.ElapsedGameTime.TotalSeconds;

                        if (time > 0.3)
                        {
                            atkEffectInstance.Volume = 0.3f;
                            atkEffectInstance.Play();
                            time = 0;
                        }

                        Singleton.Instance.currenDir = Singleton.myDirection.attack;
                    }

                    else 
                    {
                        animated.PlayAnimation("Stand");
                    }

                }


            }
            else if (Singleton.Instance.isLeftMove && Singleton.Instance.rightChooseShoot)
            {
                //p1 select box move ----> p2 select box shoot ----> p1 move
                if (parent.Name.Equals("Player1"))
                {
                    animated.PlayAnimation("Move");
                    //_moveEffect.Play(0.5f,0.5f,0.5f);

                    time += gameTime.ElapsedGameTime.TotalSeconds;
                    Console.WriteLine(time);
                    if (time > 0.5) {

                        moveEffectInstance.Play();
                        time = 0;
                    }
                    
                    

                }
                else if (parent.Name.Equals("Player2"))
                {
                    
                        if (Singleton.Instance.rightChooseShoot && Singleton.Instance.P2attacking) {

                            
                            animated.PlayAnimation("Attack");
                            time += gameTime.ElapsedGameTime.TotalSeconds;
                            if (time > 0.3)
                            {
                                atkEffectInstance.Volume = 0.3f;
                                atkEffectInstance.Play();
                                time = 0;
                            }
                    }
 
                    else
                    {
                        animated.PlayAnimation("Stand");

                    }

                }
            }

            else 
            {
                animated.PlayAnimation("Stand");

            }

            Singleton.Instance.currenDir = Singleton.myDirection.none;

            animated.Update(gameTime);

        }

    }
}
