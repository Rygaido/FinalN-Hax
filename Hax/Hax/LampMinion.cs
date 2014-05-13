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

namespace Hax
{
    class LampMinion:Enemy
    {
        private int runSpeed = -5; //how fast minion moves, and in which direction
        private int range = 20; //minimum distance between enemy and player before enemy moves

        private int deathTimer = 0;
        private int deathTime = 120;

        public LampMinion(Player p,int spawnX, int spawnY)
           : base(p,spawnX,spawnY){
               Image = ImageBank.lampMinion[0];

            animationSpeed = 15;
        }

        public override void Update(){

            //enemy only behaves if not dead
            if (current != Enemystate.dead){

                if (current == Enemystate.walking) {//set speed for if enemy is walking (falling)

                    if (ySpeed == 0) { //dead upon contact with floor
                        current = Enemystate.dead;
                    }
                }

                if (current == Enemystate.standing) { //standing lamp defies gravity
                    ySpeed = 0;

                    //check if player is nearby to start walking
                    CheckInRange();
                }
                //player intersects with enemy location
                if (Location.Intersects(player.Location)){
                    CollideWithPlayer();
                }

                
            }
            else{ //enemy is dead, 
                //put animation here
                //deactivate here

                deathTimer++;
                if (deathTimer >= deathTime) {
                    active = false;
                }
            }

            base.Update();

        }

        //player gets within range on X and Y coordinates, set state to walk
        public void CheckInRange()
        {
            if (Math.Abs(player.Location.X - Location.X) <= Math.Abs(range) && player.Location.Y - Location.Y > 0){
                current = Enemystate.walking;
                ySpeed += Movable.gravity;
            }
        }

        //Reset the enemy 
        public override void Reset(){
            base.Reset();

            //standing is default behavior
            current = Enemystate.standing;
            if (runSpeed > 0) { runSpeed *= -1; } //reset runspeed to the negative direction
            deathTimer = 0;
            Image = ImageBank.lampMinion[0];
        }

        //override draw method
        public override void Draw(SpriteBatch sb){
            if (current != Enemystate.dead){ //draw normally unless dead
                base.Draw(sb);
            }
            else{ //draw upsidedown if dead
               // SpriteEffects se = SpriteEffects.FlipVertically;
                SpriteEffects se = SpriteEffects.None;

                Image = ImageBank.lampMinionBroken[0];

                sb.Draw(Image, RealLocation, null, Color.White, 0.0f, new Vector2(0, 0), se, 0.0f);
               // base.Draw(sb);
            }
        }

        //Enemy touches player
        public override void CollideWithPlayer(){
            base.CollideWithPlayer();
        }

    }
}
