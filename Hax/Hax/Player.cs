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

        private int jumpSpeed = 50; //vertical speed at instant of jump
        private int runSpeed = 5; //max running speed
        private int acceleration = 1;

        //boolean variables for whether certain skills are usable at the time
        private Boolean canAttack;
        private Boolean canDefend;
        private Boolean canJump=true;//set to true by default for now

        public Player() { //default constructor
            Image = ImageBank.defaultImage;
        }

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

                ySpeed = -jumpSpeed;
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
        public void LeftKey() { //left and right keys are intended to be held
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
            if (state == Playerstate.standing) { //if player is standing still, deccelarate
                if (xSpeed > 0) { xSpeed-=acceleration; }
                else if (xSpeed < 0) { xSpeed += acceleration; } 
                else { xSpeed = 0; } //Bugfix: if caught in small space between acc and -acc, set speed to 0 to avoid sliding
            }

            //update player's image based on current state
            if (state == Playerstate.jumping) {
                Image = ImageBank.playerJump;
            } else if (state == Playerstate.walking) {
                Image = ImageBank.playerWalk;
            } else if (state == Playerstate.standing) {
                Image = ImageBank.playerStand;
            }

            //after updating image, reset state (unless it's jumping state, which must be changed externally)
            if (state != Playerstate.jumping) { 
                state = Playerstate.standing;
            }

            base.Update();

            //apply gravity
            ySpeed += Movable.gravity;
            //ySpeed = 1;
        }

        public override void Landing() {
            base.Landing();
            if (state == Playerstate.jumping) {
                state = Playerstate.standing;
            }
        }
    }
}
