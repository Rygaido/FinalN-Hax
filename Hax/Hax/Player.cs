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

        private int jumpSpeed = 5; //vertical speed at instant of jump
        private int runSpeed = 5; //max running speed

        //boolean variables for whether certain skills are usable at the time
        private Boolean canAttack;
        private Boolean canDefend;
        private Boolean canJump=true;//set to true by default for now

        //Method stubs for things player can do
        //these methods are marked private, public methods below should be called from game
        private void Attack() {
            if (canAttack) {
                state = Playerstate.attacking;
                 //method stub
            }
        }
        private void Defend() {
            if (canDefend) {
                state = Playerstate.defending;
                //method stub
            }
        }
        private void Jump() {
            if (canJump) {
                state = Playerstate.jumping;

                ySpeed -= jumpSpeed;
                //method stub
            }
        }
        //method controls walking, does not have a bool value as I assume it can't be disabled
        private void Walk(bool facingLeft){
            if (state != Playerstate.jumping) { //unless player is airborn, set state to walking
                state = Playerstate.walking;
            }
            faceLeft = facingLeft;
            if (xSpeed < runSpeed && !faceLeft) { //increase horizantal speed until runspeed is reached
                xSpeed++;
            }
            if (xSpeed > -runSpeed && faceLeft) { //if facing left, must actually decrease speed to negative limit
                xSpeed--;
            }
        }

        //public methods corressponding to button presses
        public void LeftKey() {
            Walk(true);
        }
        public void RightKey() {
            Walk(false);
        }
        public void UpKey() {
            Jump();
        }
        public void DownKey() {

        }
        public void NoKey() { //no key is being pressed
            
        }

        //overload of update method
        public override void Update() {
            if (state != Playerstate.walking) {
                if (xSpeed > 0) xSpeed--;
                if (xSpeed < 0) xSpeed++;
            }
            //apply gravity
            //ySpeed += Movable.gravity;

            state = Playerstate.standing;
            base.Update();
        }
    }
}
