using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//The player's object, subclass of movable and GameObject
namespace Hax {
    class Player : Movable{

        //playerstate tracks the action player is currently performing 
        //facingLeft is tracked seperately
        enum Playerstate { standing, walking, jumping, attacking, defending };

        private Playerstate state;

        //boolean variables for whether certain skills are usable at the time
        private Boolean canAttack;
        private Boolean canDefend;
        private Boolean canJump;

        //Method stubs for things player can do
        public void Attack() {
            if (canAttack) {
                state = Playerstate.attacking;
                 //method stub
            }
        }
        public void Defend() {
            if (canDefend) {
                state = Playerstate.defending;
                //method stub
            }
        }
        public void Jump() {
            if (canJump) {
                state = Playerstate.jumping;
                //method stub
            }
        }

        
    }
}
