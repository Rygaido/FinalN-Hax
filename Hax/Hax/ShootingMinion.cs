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
    class ShootingMinion:Enemy {
        private int range = 200;
        private int bulletSpeed = 4;

        public ShootingMinion(Player p,int spawnX, int spawnY)
           : base(p,spawnX,spawnY-=100){

               Image = ImageBank.shootingMinion[0];
               Location = new Rectangle(spawnX, spawnY, 65, 104);
               faceLeft = false;
               bullet = new Projectile(0, 0, true);
               bullet.Active = false;
        }

        public override void Update() {
            base.Update();

            if (Enemystate.dead != current) {
                CheckInRange();

                //player intersects with enemy location
                if (Location.Intersects(player.Location)) {
                    CollideWithPlayer();
                }
                if (bullet.Location.Intersects(player.Location)) {
                    player.TakeHit();
                    bullet.Active = false;
                }

                if (current == Enemystate.shooting) {
                    Shoot();
                    
                }


            }
            else {
                Active = false;
            }
        }

        //player gets within range on X and Y coordinates, set state to walk
        public void CheckInRange() {
            if (Math.Abs(player.Location.X - Location.X) <= Math.Abs(range)) {
                current = Enemystate.shooting;
            }
            else {
                current = Enemystate.standing;
            }
        }

        //spawn a bullet
        public override void Shoot(){
            //can't shoot when player is to the right
            if (bullet.Active==false && player.Location.X < Location.X) {
                //make new bullet at enemie's location
                bullet = new Projectile(Location.X, Location.Y, true);

                //set speed negative if player is to the left
                bullet.xSpeed = -bulletSpeed;
                bullet.Map = map;
                map.Movables.Add(bullet);
            }
        }

        public override void Reset() {
            base.Reset();

            bullet.Active = false;
        }
    }
}
