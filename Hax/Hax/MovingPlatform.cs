using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//A platform thhat moves, //Need to make play move with it if they are colliding
namespace Hax {
    class MovingPlatform:Movable {
        Boolean collidingPlayer = false;

        public void checkObject(Movable mov) {
            throw new NotImplementedException();
        }
    }
}
