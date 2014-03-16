using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hax {
    //the place where the player is trying to reach
    class Goal :GameObject{
        //probably doesn't need to do anything special
        //just call map's Clear and Load methods after player collides with this
    }

    //the place where player starts
    class Spawn : GameObject {
        //propably doesn't need to do anything, well just move player's X and Y to match after loading a map
    }
}
