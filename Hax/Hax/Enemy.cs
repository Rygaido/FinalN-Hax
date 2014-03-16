using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//an enemy, should damage player on collision, and be damaged by player projectiles
namespace Hax {
    class Enemy:Unit {

        //enum state for behavior of enemy// standing still, walk straight, walk after player
        //may want to add more states later
        enum Enemystate { standing, walking, chasing};

        Projectile bullet; //the bullet the enemy shoots, set to null for a non-shooting enemy

        //spawn a bullet
        public void Shoot() {
            //method stub
        }
    }
}
