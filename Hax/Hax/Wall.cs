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

//wall stops other objects from moving though it. de-activates projectiles
//also usable as a floor
//implements platform
namespace Hax {
    class Wall  : Platform {
        //this class probably wont need any methods, collision between movables and walls
        //are expected to be handled in the Map

        public Wall() {
            solid = true;
            Image = ImageBank.wallImage;
        }

        //platform checks movable-gameobject's location and yspeed to see if it needs to catch object and do what platforms do
        public override void checkObject(Movable mov) {

            base.checkObject(mov);

            if (mov.Location.X > Location.X - mov.Location.Width) { //check object is within reach on leftside
                if (mov.Location.X < Location.X + Location.Width) { //check object is within reach on rightside
                    int head = mov.Location.Top; //store Y value of highest point on mov object
                    int ceiling = Location.Bottom; //y value of lowest point on wall

                    if (head >= ceiling) { //check object is below platform's surface
                        //all 3 requirements met, restrict object's yspeed so that it can't pass through object
                        if (-mov.ySpeed > head - ceiling) {
                            //mov.ySpeed = 0;
                            mov.ySpeed = -(head - ceiling);
                           // mov.Landing();

                           // mov.CollidingWithPlatform = !solid;
                        }
                    }
                }
            }

            if (Location.Intersects(mov.Location)) //player collides with wall
            {
                if (mov.Previous.Right < Location.Center.X) {//push to the right
                    try {
                        Player p = (Player)mov;
                        Map.scroll.X += mov.xSpeed;
                    } catch (Exception e) { }

                    mov.xSpeed = 0;
                    mov.Location = new Rectangle(Location.Left - mov.Location.Width, mov.Location.Y, mov.Location.Width, mov.Location.Height);
                }

                if (mov.Previous.Left > Location.Center.X) //push to the left
                {
                    try {
                        Player p = (Player)mov;
                        Map.scroll.X += mov.xSpeed;
                    } catch (Exception e) { }

                    mov.xSpeed = 0;
                    mov.Location = new Rectangle(Location.Right, mov.Location.Y, mov.Location.Width, mov.Location.Height);
                }
            }
        }

    }
}
