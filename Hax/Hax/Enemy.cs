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
        public enum Enemystate { standing, walking, chasing, warning, shooting, dead, vulnerable, notSpawned};

        protected Enemystate current=Enemystate.standing;
        public Enemystate Current {
            get { return current; }
        }
        protected Projectile bullet; //the bullet the enemy shoots, set to null for a non-shooting enemy
        protected Texture2D bulletImage = ImageBank.bullet;
        protected Player player;

        protected int spawnX;
        protected int spawnY;

        protected bool shotFired;
        protected int timer = 0;
        protected int shootCooldown = 50;
        protected int bulletSpeed = 5;

        protected int bulletX=0;
        protected int bulletY = 0;

        public Enemy(Player p, int x, int y)
        {
            faceLeft = true;
            Location = new Rectangle(x, y, 50, 55);
            spawnX = x;
            spawnY = y;
            player = p;
            health = 1;
        }

        //spawn a bullet
        public virtual void Shoot() {
            shotFired = true;
            timer = shootCooldown;

            //make new bullet at enemy's location
            bullet = new Projectile(Location.X+bulletX, Location.Y+bulletY, true);
            //bullet.Update();
            bullet.Offset = offset;
            bullet.Image = bulletImage;
            bullet.Map = map;
            map.Movables.Add(bullet);

            //set speed negative if player is to the left
            if (faceLeft){//player.Location.X < Location.X) {
                bullet.xSpeed = -bulletSpeed;
               // faceLeft = true;
            }
            else {
                bullet.xSpeed = bulletSpeed;
              //  faceLeft = false;
            }
        }

        public override void Update(){ //apply the gravity
           base.Update();
           ySpeed += Movable.gravity;

           if (health <= 0) {
               current = Enemystate.dead;
           }
        }

        public override void Reset(){ //overide reset, enemies respawn and set to standing
            base.Reset();

            active = true;
            health = 1;
            Location = new Rectangle(spawnX, spawnY, Location.Width, Location.Height);

            current = Enemystate.standing;
        }

        public virtual void CollideWithPlayer(){
            if (current != Enemystate.dead) {
                //if player hits enemy on top
                if (player.Previous.Bottom < Location.Center.Y) {
                    player.ySpeed = -15;//bounces player
                    current = Enemystate.dead; //kill enemy
                }
                else if (player.State == Player.Playerstate.defending) {
                    current = Enemystate.dead;
                    //player.State = Player.Playerstate.shieldBreaking;

                    player.TakeHit();
                }
                else {
                    player.TakeHit();
                }
            }
        }
    }
}
