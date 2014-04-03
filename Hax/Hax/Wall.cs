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

//wall stops other objects from moving though it. de-activates projectiles
//also usable as a floor
//implements platform
namespace Hax {
    class Wall  : GameObject, Platform {
        //this class probably wont need any methods, collision between movables and walls
        //are expected to be handled in the Map

        public Wall() {

            Image = ImageBank.wallImage;
        }

        //platform checks movable-gameobject's location and yspeed to see if it needs to catch object and do what platforms do
        public void checkObject(Movable mov) {
            
            if (mov.Location.X > Location.X-mov.Location.Width ) { //check object is within reach on leftside
                if (mov.Location.X < Location.X+Location.Width) { //check object is within reach on rightside
                    int foot = mov.Location.Y + mov.Location.Height; //store Y value of lowest point on mov object
                    if (foot <= Location.Y) { //check object is above platform's surface
                       
                        //all 3 requirements met, restrict object's yspeed so that it can't pass through object
                        if (mov.ySpeed >= Location.Y-foot) {
                            //mov.ySpeed = 0;
                            mov.ySpeed = Location.Y - foot;
                            mov.Landing();
                        }
                    }
                }
            }

            if (mov.Location.Top > Location.Bottom+1){ //check object is within reach on bottomside
                if (mov.Location.Top < Location.Top-1){ //check object is within reach on topside
                    if (mov.Location.Right <= Location.Left)
                    { //check object is to the left of wall's leftmost edge

                        //all 3 requirements met, restrict object's xspeed so that it can't pass through object
                        if (mov.xSpeed >= Location.Left - mov.Location.Right)
                        //if(mov.Location.X  >= Location.X+Location.Width)
                        {
                            //mov.ySpeed = 0;
                            mov.xSpeed = 0;
                            mov.collidingWithWall = true;
                        }
                    }

                    //Location.
                }
            }
        }
    }
}
