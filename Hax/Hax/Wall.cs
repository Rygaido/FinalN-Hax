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
    class Wall  : Platform {
        //this class probably wont need any methods, collision between movables and walls
        //are expected to be handled in the Map

        public Wall() {

            Image = ImageBank.wallImage;
        }

        //platform checks movable-gameobject's location and yspeed to see if it needs to catch object and do what platforms do
        public override void checkObject(Movable mov) {

            base.checkObject(mov);

            if (Location.Intersects(mov.Location))
            {// && mov.Previous.Right < Location.Left && mov.Previous.Bottom > Location.Top){
                if (mov.Previous.Right < Location.Center.X)
                {
                    mov.xSpeed = 0;
                    mov.Location = new Rectangle(Location.Left - mov.Location.Width, mov.Location.Y, mov.Location.Width, mov.Location.Height);

                }

                if (mov.Previous.Left > Location.Center.X)
                {
                    mov.xSpeed = 0;
                    mov.Location = new Rectangle(Location.Right, mov.Location.Y, mov.Location.Width, mov.Location.Height);
                }
            }
        }

        //platform checks movable-gameobject's location and yspeed to see if it needs to catch object and do what platforms do
        public override void checkPlayer(Movable mov) {

            if (mov.Active == true) {
                if (mov.Location.X > Location.X - mov.Location.Width) { //check object is within reach on leftside
                    if (mov.Location.X < Location.X + Location.Width) { //check object is within reach on rightside
                        int foot = mov.Location.Y + mov.Location.Height; //store Y value of lowest point on mov object
                        if (foot <= Location.Y) { //check object is above platform's surface

                            //all 3 requirements met, restrict object's yspeed so that it can't pass through object
                            if (mov.ySpeed >= Location.Y - foot) {
                                //mov.ySpeed = 0;
                                mov.ySpeed = Location.Y - foot;
                                mov.Landing();
                            }
                        }
                    }
                }

                if (Location.Intersects(mov.Location)) {// && mov.Previous.Right < Location.Left && mov.Previous.Bottom > Location.Top){
                    if (mov.Previous.Right < Location.Center.X) {
                        Map.scroll.X += mov.xSpeed;
                        mov.xSpeed = 0;
                        mov.Location = new Rectangle(Location.Left - mov.Location.Width, mov.Location.Y, mov.Location.Width, mov.Location.Height);
                    }

                    if (mov.Previous.Left > Location.Center.X) {
                        Map.scroll.X += mov.xSpeed;
                        mov.xSpeed = 0;
                        mov.Location = new Rectangle(Location.Right, mov.Location.Y, mov.Location.Width, mov.Location.Height);
                    }
                }
            }
        }
    }
}
