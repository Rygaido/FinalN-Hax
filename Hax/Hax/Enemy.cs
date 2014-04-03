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
        enum Enemystate { standing, walking, chasing};

        Projectile bullet; //the bullet the enemy shoots, set to null for a non-shooting enemy

        public Enemy()
        {
            faceLeft = true;
            Location = new Rectangle(500, 300, 50, 55);
            
        }

        //spawn a bullet
        public void Shoot() {
            //method stub
        }

        public override void Update()
        {
           base.Update();
           ySpeed += Movable.gravity;
        }

        public virtual void Reset()
        {
            Location = new Rectangle(500, 300, 50, 55);
        }

        public void CollideWithPlayer(Player p)
        {
            if (p.Location.Intersects(Location))
            {
                p.TakeHit();
            }
        }
    }
}
