﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

//The player's object, subclass of movable and GameObject
namespace Hax {
    class Player : Unit{

        //playerstate tracks the action player is currently performing 
        //facingLeft is tracked seperately
        public enum Playerstate { standing, walking, jumping, defending, shieldBreaking };

        private Playerstate state;
        private Playerstate previous;

        //getter/setter for current state
        public Playerstate State {
            get { return state; }
            set { state = value; }
        }

        private int jumpSpeed = 20; //vertical speed at instant of jump
        private int runSpeed = 5; //max running speed
        private int acceleration = 1;

        //boolean variables for whether certain skills are usable at the time
        private Boolean canAttack=true;
        private Boolean canDefend=true;
        private Boolean canJump=true;//set to true by default for now

<<<<<<< HEAD
        private Boolean hasJumped=false; //player can not jump repeatedly
        private Boolean idle = false; //set to false during all actions, if it is true set player to default state of standing
        private Boolean attacking = false; //player is shooting something currently

        private Projectile bullet; //the bullet object player is shooting
        private int bulletSpeed = 10; //speed of player's bullet

        //when shield is broken, set to true, then run shieldTimer for Cooldown frames to restore shield
        private int shieldTimer = 0;
        private int shieldCooldown = 150;
        private bool shieldBroken = false;
=======
        //projectile for the gun (note may wanna pass it in as apposed to having an attribute)
        private Projectile bullet;

>>>>>>> 93e7c9c16b8a7a9856a2b6e3df1a06c81837f86c

        public Player() { //default constructor
            health = 1;
            Image = ImageBank.defaultImage;

            Location = new Rectangle(50, 250, 57, 87);
            //Location.Width = Image.Width;
            //Location.Height = Image.Height;
            bullet = new Projectile(0,0,false);
            bullet.Active = false;
            //map = m;
            state = Playerstate.standing;
            previous = state;

            bullet = new Projectile(2, 2, false);
        }

        //Method stubs for things player can do
        //these methods are marked private, public key-pressed methods below should be called from game

        //player shoots a bullet
        private void Attack() {
            if (canAttack) {
                attacking = true;
                //idle = false;
                Shoot();
            }
        }
        private void Defend() { //player produces a shield, which can block one attack
            if (canDefend && !shieldBroken) {
                idle = false;
                state = Playerstate.defending;

            }
        }
        private void Jump() { //player leaps into the air
            if (!hasJumped && state != Playerstate.defending) { //cant jump while defending, or while has previously jumped
           // if(previous == Playerstate.standing){
                hasJumped = true;
                idle = false;
                state = Playerstate.jumping;

                ySpeed = -jumpSpeed; //jumpspeed is actually negative because Y axis is wierd
            }
        }
        //method controls walking, does not have a bool value as I assume it can't be disabled
        private void Walk(bool facingLeft){
            if (!collidingWithWall)
            {
                idle = false;
                if (state != Playerstate.jumping)
                { //unless player is airborn, set state to walking
                    state = Playerstate.walking;
                }
                faceLeft = facingLeft;
                if (xSpeed < runSpeed && !faceLeft)
                { //increase horizantal speed until runspeed is reached
                    xSpeed++;
                }
                if (xSpeed > -runSpeed && faceLeft)
                { //if facing left, must actually decrease speed to negative limit
                    xSpeed--;
                }
            }
        }
        //spawn a bullet
        public void Shoot() {
            //can't shoot when player is to the right
           // this.col = Color.Red;
            if (bullet.Active == false) {
                //make new bullet at player's location
                bullet = new Projectile(Location.X, Location.Y, false);

                //set speed negative if player is facing the left
                bullet.xSpeed = bulletSpeed;
                if (faceLeft) {
                    bullet.xSpeed *= -1;
                }
                //add bullet to map.Movables and add map to bullet
                bullet.Map = map;
                map.Movables.Add(bullet);
            }
        }

        //public methods corressponding to button presses
        public void LeftKey() { //left and right keys are intended to be held
            Walk(true);
        }
        public void RightKey() {
            Walk(false);
        }
        public void UpKey() { //up key jumps
            Jump();
        }
        public void ReleaseUpKey() //releasing up key stops upward motion
        {
            if (!(ySpeed > 0))
            {
                ySpeed = 0;
            }
        }
        public void DownKey() { //down key


        }
        public void SKey() { //s key shoots
            Attack();
        }
        public void DKey() { //d key defends
            Defend();
        }
        public void NoKey() { //no key is being pressed
            
        }

        //overload of update method
        public override void Update() {
            if (previous != state) {
               // ResetAnimation();
            }

            if (state == Playerstate.standing|| state == Playerstate.defending) { //if player is standing still, deccelarate
                if (xSpeed > 0) { xSpeed-=acceleration; }
                else if (xSpeed < 0) { xSpeed += acceleration; } 
                else { xSpeed = 0; } //Bugfix: if caught in small space between acc and -acc, set speed to 0 to avoid sliding
            }

            Map.scroll.X -= xSpeed;
            Map.scroll.Y -= ySpeed;

            //update player's image based on current state
            if (state == Playerstate.jumping) {
                Animate(ImageBank.playerJump);
            } else if (state == Playerstate.walking) {
                animationSpeed = 7;
                if (!attacking) {
                    Animate(ImageBank.playerWalk);
                }
                else {
                    Animate(ImageBank.playerAttack);
                }
            } else if (state == Playerstate.standing) {
                animationSpeed = 25;
                if (!attacking) {
                    Animate(ImageBank.playerStand);
                }
                else {
                    Animate(ImageBank.playerAttack);
                }
            }
            /*else if (attacking) {
                animationSpeed = 1;
                Animate(ImageBank.playerAttack);
            }*/
            else if (state == Playerstate.defending) {
                animationSpeed = 1;
                Animate(ImageBank.playerDefend);
            }///*
            if (animationEnd) {
                attacking = false;
            }//*/

            //after updating image, reset state (unless it's jumping state, which must be changed externally)
            if (state != Playerstate.jumping && idle) { 
                state = Playerstate.standing;
            }

            if (state != previous)
            {
                animationTimer = 25;
            }

            //increment timer when shield is broken
            if (shieldTimer < shieldCooldown && shieldBroken) {
                shieldTimer++;
            }
            else if (shieldTimer == shieldCooldown) { //when timer passes cooldown, repair shield
                shieldBroken = false;
            }

            previous = state;

            base.Update();

            //apply gravity
            ySpeed += Movable.gravity;
            //ySpeed = 1;

            idle = true;
        }

        //player touches ground and can jump again
        public override void Landing() {
            base.Landing();
            if (state == Playerstate.jumping) {
                state = Playerstate.standing;
            }
            hasJumped = false;
        }

        //reset player and variables to replay level
        public void Reset(){
            Location = new Rectangle(map.PlayerSpawn.X, map.PlayerSpawn.Y-100, Location.Width, Location.Height);
            
            Map.scroll.X = Location.X + 100;
            Map.scroll.Y = Location.Y - 200;
            health = 1;
            bullet = new Projectile(0, 0, false);
            bullet.Active = false;

            xSpeed = 0;
            ySpeed = 0;
        }

        //player has collided with enemy or bullet
        public void TakeHit(){
            if (state != Playerstate.defending) {
                base.TakeDamage(1);
            }
            else { //if player is defending, he will not take damage
                state = Playerstate.shieldBreaking;
                shieldBroken = true;
                shieldTimer = 0;
            }
            //Reset();
        }

        //helper method, move player to a point
        public void JumpToPoint(Point p) {
            Location = new Rectangle(p.X, p.Y, Location.Width, Location.Height);
        }

        //first power: shooting
        public void Fire()
        {
            //basic plan: use a similar method as the shooting minion for this. we need to know which way he is facing so we know how to do the bullet speed
            //also do we want one bullet at a time or machine gun and what is the gun's range
        }

        //second power: defending
        public void Defend()
        {
            //basic plan: make the player take a hit or two from the direction he is facing. We also need to discuss are we implementing health or just one shot one kill without the shield?
            //player should also not be able to move when the shield is in use, so freeze him in place. we also need to discuss if we want to put a cool down on this or let the player use it at their discretion
        }
    }
}
