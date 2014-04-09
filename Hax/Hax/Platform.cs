using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Interface identifies object as a platform
//Should be able to use this to find which gameobjects player and enemies can stand on

//this was not in the original plan, not entirely sure if it will be useful either
//implement into all objects that can be stood on
namespace Hax {
    interface Platform {
        //interface stub... not sure if anything needs to be in here

        //methodstub //platform checks movable-gameobject's location and yspeed to see if it needs to catch object and do what platforms do
        void checkObject(Movable mov);
    }
}
