﻿#region Using Statements
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

        protected Map map; //reference to the map object
        public Map Map { //getter and setter property
            get { return map; }
            set { map = value; }
        }
        protected Boolean active=true; //whether or not object is in use
        //if not active do not draw or check collisions

        public Boolean Active { get { return active; } set { active = value; } }

        private Rectangle previous;

        public Rectangle Previous
        {
            get { return previous; }
            set { previous = value; }
        }

        //IDEA: using a speed and direction is very accurate, but is very math-heavy
        //private int speed; //speed object is moving (pixels)
        //private int direction; // direction object is moving (degrees)

        //alternatively, just replace with xSpeed and ySpeed ints,
        public int xSpeed;
        public int ySpeed;
        protected bool collidingWithWall = false;
        protected bool collidingWithPlatform = false;
        public bool CollidingWithPlatform {
            get { return collidingWithPlatform; }
            set { collidingWithPlatform = value; }
        }

        public const int gravity= 1; //player and enemies accelerate downward by pixels per frame per frame
        //GRAVITY PULLS IN THE POSITIVE Y DIRECTION!!! //lower on screen == higher Y //don't confuse that

        //overload update to call move method, then call base update method
        public override void Update() {
            Previous = Location;
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

        public override void Draw(SpriteBatch sb) {
            if (Active) {
                base.Draw(sb);
            }
        }

        //call this method on an object that has landed on a platform
        public virtual void Landing() {

        }
        ///*
        public virtual void Reset() {
            xSpeed = 0;
            ySpeed = 0;
        }//*/
    }
}
