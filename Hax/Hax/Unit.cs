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

//subclass of movable, for objects that can act and take damage such as enemies and the player
//A class that was not in the original tree
namespace Hax {
    class Unit:Movable {
        int health;

        
        //set to false to face right
        //since mirroring the sprite is a simple transformation, I figured it was better to
        //track the facing Direction seperate from the state machine, otherwise we'd need twice as many states 


        //decrease health, check if dead
        public void TakeDamage(int damage) {
            health -= damage;

            //method stub?
        }

        //returns true if health is less than or equal to 0
        public Boolean IsDead() {
            return health <= 0;
        }
    }
}
