using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hax {
    class Platform : GameObject{

        protected bool solid = false; //can the player pass through this platform with the down key?

        public Platform() {

            Image = ImageBank.wallImage;
        }

        //platform checks movable-gameobject's location and yspeed to see if it needs to catch object and do what platforms do
        public virtual void checkObject(Movable mov) {

                if (mov.Location.X > Location.X - mov.Location.Width) { //check object is within reach on leftside
                    if (mov.Location.X < Location.X + Location.Width) { //check object is within reach on rightside
                        int foot = mov.Location.Y + mov.Location.Height; //store Y value of lowest point on mov object
                        if (foot <= Location.Y) { //check object is above platform's surface

                            //all 3 requirements met, restrict object's yspeed so that it can't pass through object
                            if (mov.ySpeed >= Location.Y - foot) {
                                //mov.ySpeed = 0;
                                mov.ySpeed = Location.Y - foot;
                                mov.Landing();

                                mov.CollidingWithPlatform = !solid;
                            }
                        }
                    }
                }
        }
    }
}
