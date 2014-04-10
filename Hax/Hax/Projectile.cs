using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//represents any variety of bullet
namespace Hax {
    class Projectile:Movable {

        Boolean hostile; //variable tracks wether bullet was fired by enemy or not, 
        //if true, it should damage player on collision, otherwise it should damage enemies
    }
}
