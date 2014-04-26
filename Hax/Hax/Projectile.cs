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

        Boolean hostile; //variable tracks wether bullet was fired by enemy or not, 
        //if true, it should damage player on collision, otherwise it should damage enemies

        public Projectile(int x, int y, bool h) {
            Location = new Rectangle(x,y,70,50);
            hostile = h;
            //we need to add an if statement that changes the pic depending on if the bullet is hostile or not
            Image = ImageBank.bullet;
        }

        public override void Update() {
            base.Update();

            if (xSpeed == 0) {
                active = false;
            }

            if (RealLocation.X <= -Location.Width) {
                Active = false;
            }

            /*/player intersects with bullet location
            if (Location.Intersects(player.Location)) {
                CollideWithPlayer();
            }*/
        }

        public void CheckCollisionB(Unit u) {
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
