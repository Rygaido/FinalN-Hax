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


        //constructor takes x&y spawn coords and a player
        public ShootingMinion(Player p,int spawnX, int spawnY)
           : base(p,spawnX,spawnY-=100){ //spawn coords offset since this minion is TALL

               Image = ImageBank.shootingMinion[0];
               Location = new Rectangle(spawnX, spawnY, 65, 104);
               faceLeft = false;
               bullet = new Projectile(0, 0, true);
               bullet.Active = false;
        }

        public override void Update() {
            base.Update();

            if (Enemystate.dead != current) { //do nothing when dead //not a zombie
                CheckInRange();

                //player intersects with enemy location
                if (Location.Intersects(player.Location)) {
                    CollideWithPlayer();
                }
                if(player.Location.X < Location.X) {
                    faceLeft = true;
                }
                //call shoot method if conditions to shoot are met
                if (current == Enemystate.shooting && !shotFired) {
                    Shoot();
                }

                if (timer > 0) {
                    timer--;
                }
                if (timer <= 0) {
                    shotFired = false;
                }
            }
            else {
                Active = false;
            }
        }

        //player gets within range on X and Y coordinates, set state to shooting//otherwise revert to standing
        public void CheckInRange() {
            if (Math.Abs(player.Location.X - Location.X) <= Math.Abs(range) && Math.Abs(player.Location.Y - Location.Y)/2 <= Math.Abs(range)) {
                current = Enemystate.shooting;
            }
            else {
                current = Enemystate.standing;
            }
        }

        //spawn a bullet
        public override void Shoot(){
            //can't shoot when player is to the right or if a previous bullet is active
            //if (bullet.Active==false) {
            base.Shoot();
            //}
        }

        public override void Reset() {
            base.Reset();

            bullet.Active = false;
        }
    }
}
