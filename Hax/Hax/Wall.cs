using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//wall stops other objects from moving though it. de-activates projectiles
//also usable as a floor
//implements platform
namespace Hax {
    class Wall  : GameObject, Platform {
        //this class probably wont need any methods, collision between movables and walls
        //are expected to be handled in the Map
    }
}
