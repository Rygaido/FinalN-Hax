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

        //IDEA: using a speed and direction is very accurate, but is very math-heavy
        //private int speed; //speed object is moving (pixels)
        //private int direction; // direction object is moving (degrees)

        //alternatively, just replace with xSpeed and ySpeed ints,
        public int xSpeed;
        public int ySpeed;

        public bool collidingWithWall = false;

        public const int gravity= 1; //player and enemies accelerate downward by pixels per frame per frame
        //GRAVITY PULLS IN THE POSITIVE Y DIRECTION!!! //lower on screen == higher Y //don't confuse that

        //overload update to call move method, then call base update method
        public override void Update() {
            Move();
            base.Update();
        }

        //change location based on speed and direction
        public virtual void Move() {
            //method stub

            //find the new x and y coordinates by check former coordinates and adding x and y speeds
            int x = Location.X + xSpeed;
            int y = Location.Y + ySpeed;

            //create new rectangle at proper location
            Location = new Rectangle(x,y,Location.Width,Location.Height);
        }

        //call this method on an object that has landed on a platform
        public virtual void Landing() {

        }
    }
}
