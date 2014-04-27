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

//represents any variety of bullet
namespace Hax {
    class Projectile:Movable {

        private Boolean hostile; //variable tracks wether bullet was fired by enemy or not, 
        //if true, it should damage player on collision, otherwise it should damage enemies

        public Boolean Hostile {
            get { return hostile; }
            set { hostile = value; }
        }

        public Projectile(int x, int y, bool h) {
            hostile = h;
<<<<<<< HEAD
            
            if (hostile == false) { //bullet owned by player
                Image = ImageBank.playerBullet;
                Location = new Rectangle(x + 30, y + 40, 10, 10);
            }
            else { //bullet owned by enemy
                Image = ImageBank.bullet;
                Location = new Rectangle(x, y, 70, 50);
            }
=======
            //we need to add an if statement that changes the pic depending on if the bullet is hostile or not
            Image = ImageBank.bullet;
>>>>>>> 93e7c9c16b8a7a9856a2b6e3df1a06c81837f86c
        }

        //bullet update checks for conditions to remove bullet from play
        public override void Update() {
            base.Update();

            if (xSpeed == 0) { //projectile is destroyed if not moving
                active = false;
            }

            if (RealLocation.X <= -Location.Width) { //remove bullet if it passes offscreen to the left
                Active = false;
            }
            if (RealLocation.X >= 800) { //or to the right
                Active = false;
            }
        }

        public void CheckCollisionB(Unit u) { //collision with a unit
            if (Location.Intersects(u.Location)) {
                u.TakeDamage(1);
                this.Active = false;
            }
        }
        ///*
        public override void Reset() {
            Active = false;
        }//*/
    }
}
