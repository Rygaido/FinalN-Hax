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

//an enemy, should damage player on collision, and be damaged by player projectiles
namespace Hax {
    class Enemy:Unit {

        //enum state for behavior of enemy// standing still, walk straight, walk after player
        //may want to add more states later
        protected enum Enemystate { standing, walking, chasing, shooting, dead};

        protected Enemystate current=Enemystate.standing;
        protected Projectile bullet; //the bullet the enemy shoots, set to null for a non-shooting enemy
        protected Player player;

        protected int spawnX;
        protected int spawnY;

        public Enemy(Player p, int x, int y)
        {
            faceLeft = true;
            Location = new Rectangle(x, y, 50, 55);
            spawnX = x;
            spawnY = y;
            player = p;
        }

        //spawn a bullet
        public virtual void Shoot() {
            //method stub
        }

        public override void Update(){ //apply the gravity
           base.Update();
           ySpeed += Movable.gravity;
        }

        public override void Reset(){ //overide reset, enemies respawn and set to standing
            active = true;

            Location = new Rectangle(spawnX, spawnY, Location.Width, Location.Height);

            current = Enemystate.standing;
        }

        public virtual void CollideWithPlayer(){
            //if player hits enemy on top
            if (player.Previous.Bottom < Location.Center.Y){
                player.ySpeed = -15;//bounces player
                current = Enemystate.dead; //kill enemy
            }
            else{
                player.TakeHit();
            }
        }
    }
}
