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

//Superclass to all dynamic moving game objects
namespace Hax {
    class Movable : GameObject{

        private Boolean active; //whether or not object is in use
        //if not active do not draw or check collisions

        private int speed; //speed object is moving (pixels)
        private int direction; // direction object is moving (degrees)

        //IDEA: using a speed and direction is very accurate, but is very math-heavy
        //if necessary, just replace with xSpeed and ySpeed ints,
        //private int xSpeed;
        //private int ySpeed;


        public const int gravity= 5; //player and enemies accelerate downward by pixels per frame

        //overload update to call move method, then call base update method
        public override void Update() {
            Move();
            base.Update();
        }

        //change location based on speed and direction
        public virtual void Move() {
            //method stub
        }
    }
}
