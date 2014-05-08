#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion


namespace Hax {
    class Boss : Enemy {
        private int range = 200; //minimum distance between enemy and player before enemy moves

        private int stateTimer = 0;
        private int idleTime = 180;
        private int shootingTime = 180;
        private int walkingTime = 120;
        private int chargingTime = 180;
        private int vulnerableTime = 180;

        private int BOSS_HEALTH = 20;
        private int RUN_SPEED = 10;

        public Boss(Player p, int x, int y):base(p,x,y) {
            Location = new Rectangle(x, y, 165, 107);

            Image = ImageBank.bossIdle[0];
            health = BOSS_HEALTH;
            current = Enemystate.notSpawned;
        }

        public override void Update() {
            if (current != Enemystate.notSpawned) { //boss has not spawned stage, where he's invisible
                base.Update();
               
                if (current == Enemystate.standing) { //Idle stage
                    //stands idley, transitions to either offensive stage depending on distance from player

                    //set idle animation
                    Animate(ImageBank.bossIdle);

                    col = Color.White;

                    xSpeed = 0;

                    //face player
                    faceLeft = IsPlayerLeft();

                    stateTimer++; //stay in this state for predetermined number of frames
                    if (stateTimer >= idleTime) {
                        stateTimer = 0;

                        if (CheckInRange()) { //shoot at player if he's near
                            current = Enemystate.shooting;
                        }
                        else {//charge at player if he's far
                            current = Enemystate.walking;
                        }
                    }
                }
                if (current == Enemystate.shooting) { //Shooting stage
                    //starts shooting at player for predetermined frames

                    //insert shoot animation here /////////////////////

                    col = Color.Red;

                    stateTimer++;//stay in this state for predetermined number of frames
                    if (stateTimer >= shootingTime) {
                        stateTimer = 0;

                        current = Enemystate.standing;
                    }
                }
                if (current == Enemystate.walking) { //pre-Chasing stage
                    //walks twords player slowly for predetermined frames before charging
                    col = Color.Pink;

                    //insert walk animation here /////////////////////

                    //keep facing player //this is not done in the charge method, so he can be dodged then
                    faceLeft = IsPlayerLeft();

                    if (faceLeft) {
                        xSpeed = -2;
                    }
                    else {
                        xSpeed = 2;
                    }

                    stateTimer++;//stay in this state for predetermined number of frames
                    if (stateTimer >= walkingTime) {
                        stateTimer = 0;

                        current = Enemystate.chasing;
                    }
                }
                if (current == Enemystate.chasing) { //Chasing stage
                    //charges straight forward for predetermined frames or until hits wall

                    col = Color.Yellow;

                    //insert charge animation here (probably just a faster walk) ///////////////////// 

                    //does not redirect to face player in this state, will keep chargin past him

                    if (xSpeed == 0) { //crashing into wall
                        stateTimer = 0;
                        current = Enemystate.vulnerable; //transition to vulnerable state
                    }

                    if (faceLeft) { //Set to maximum run speed in direction faced previously
                        xSpeed = -RUN_SPEED;
                    }
                    else {
                        xSpeed = RUN_SPEED;
                    }

                    stateTimer++;//stay in this state for predetermined number of frames
                    if (stateTimer >= chargingTime) {
                        stateTimer = 0;

                        current = Enemystate.standing;
                    }
                }
                if (current == Enemystate.vulnerable) { //vulnerable stage
                    col = Color.Orange;

                    //insert vulnerable animation here /////////////////////

                    stateTimer++;//stay in this state for predetermined number of frames
                    if (stateTimer >= vulnerableTime) {
                        stateTimer = 0;

                        current = Enemystate.standing;
                    }
                }
            }
            else {
                if (CheckInRange()) {
                    current = Enemystate.standing; //spawn boss suddenly
                }
            }
        }

        private Boolean IsPlayerLeft() {
            return player.Location.X < Location.X;
        }

        public override void Draw(SpriteBatch sb) {
            if (current != Enemystate.notSpawned) {
                if (current != Enemystate.dead) { //draw normally unless dead
                    base.Draw(sb);
                }
                else { //draw upsidedown if dead
                    SpriteEffects se = SpriteEffects.FlipVertically;

                    sb.Draw(Image, RealLocation, null, Color.White, 0.0f, new Vector2(0, 0), se, 0.0f);
                }
            }
        }

        //player gets within range on X and Y coordinates, set state to walk
        public bool CheckInRange() {
            return Math.Abs(player.Location.X - Location.X) <= Math.Abs(range);
            /*
            if (Math.Abs(player.Location.X - Location.X) <= Math.Abs(range)) {
                current = Enemystate.standing;
            }*/
        }

        public override void CollideWithPlayer() {
            
        }

        public override void Reset() {
            base.Reset();
            health = BOSS_HEALTH;
            //current = Enemystate.notSpawned;
        }

        public override void TakeDamage(int damage) {
            if (current == Enemystate.vulnerable || current == Enemystate.walking || current == Enemystate.chasing || current == Enemystate.shooting) {
                base.TakeDamage(damage);
            }
        }
    }
}
