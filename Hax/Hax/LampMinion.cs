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

        public LampMinion(Player p,int spawnX, int spawnY)
           : base(p,spawnX,spawnY){
               Image = ImageBank.walkingMinion[0];

            animationSpeed = 15;
        }

        public override void Update(){
            //enemy only behaves if not dead
            if (current != Enemystate.dead){
                base.Update();

                //player intersects with enemy location
                if (Location.Intersects(player.Location)){
                    CollideWithPlayer();
                }

                //changes enemy direction if enemy walked into a wall
                if (current==Enemystate.walking && ySpeed == 0){
                    current = Enemystate.dead;
                }

                //if enemy is standing, check if player is nearby to start walking
                if (current==Enemystate.standing ) {
                    CheckInRange();
                }

                if (current==Enemystate.walking) {//set speed for if enemy is walking

                    ySpeed += Movable.gravity;

                    //Animate(ImageBank.walkingMinion);
                    //if (hitWall == false) //set speed, if hitwall then direction is reversed
                    xSpeed = runSpeed;
                    
                }
            }
            else{ //enemy is dead, 
                //put animation here
                //deactivate here
            }
           
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
        }

        //override draw method
        public override void Draw(SpriteBatch sb){
            if (current != Enemystate.dead){ //draw normally unless dead
                base.Draw(sb);
            }
            else{ //draw upsidedown if dead
                SpriteEffects se = SpriteEffects.FlipVertically;

                sb.Draw(Image, RealLocation, null, Color.White, 0.0f, new Vector2(0, 0), se, 0.0f);
            }
        }

        //Enemy touches player
        public override void CollideWithPlayer(){
            base.CollideWithPlayer();
        }

    }
}
