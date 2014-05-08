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
            if (current != Enemystate.notSpawned) {
                base.Update();
               
                if (current == Enemystate.standing) { //Idle stage
                    Animate(ImageBank.bossIdle);

                    col = Color.White;

                    xSpeed = 0;
                    faceLeft = IsPlayerLeft();

                    stateTimer++;
                    if (stateTimer >= idleTime) {
                        stateTimer = 0;
                        if (Math.Abs(player.Location.X - Location.X) <= Math.Abs(range)) {
                            current = Enemystate.shooting;
                        }
                        else {

                            current = Enemystate.walking;
                        }
                    }
                }
                if (current == Enemystate.shooting) { //Shooting stage
                    col = Color.Red;

                    stateTimer++;
                    if (stateTimer >= shootingTime) {
                        stateTimer = 0;

                        current = Enemystate.standing;
                    }
                }
                if (current == Enemystate.walking) { //pre-Chasing stage
                    col = Color.Pink;

                    faceLeft = IsPlayerLeft();

                    if (faceLeft) {
                        xSpeed = -2;
                    }
                    else {
                        xSpeed = 2;
                    }
                    stateTimer++;
                    if (stateTimer >= walkingTime) {
                        stateTimer = 0;

                        current = Enemystate.chasing;
                    }
                }
                if (current == Enemystate.chasing) { //Chasing stage
                    col = Color.Yellow;


                    if (xSpeed == 0) {
                        stateTimer = 0;
                        current = Enemystate.vulnerable;
                    }

                    if (faceLeft) {
                        xSpeed = -RUN_SPEED;
                    }
                    else {
                        xSpeed = RUN_SPEED;
                    }


                    stateTimer++;
                    if (stateTimer >= chargingTime) {
                        stateTimer = 0;

                        current = Enemystate.standing;
                    }
                }
                if (current == Enemystate.chasing) { //Chasing stage
                    col = Color.Orange;
                }
            }
            else {
                CheckInRange();
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
        public void CheckInRange() {
            if (Math.Abs(player.Location.X - Location.X) <= Math.Abs(range)) {
                current = Enemystate.standing;
            }
        }

        public override void CollideWithPlayer() {
            
        }

        public override void Reset() {
            base.Reset();
            health = BOSS_HEALTH;
            //current = Enemystate.notSpawned;
        }
    }
}
