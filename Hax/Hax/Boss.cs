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


namespace Hax {
    class Boss : Enemy {
        private int range = 200; //minimum distance between enemy and player before enemy moves

        private int stateTimer = 0;
        private int idleTime = 180;
        private int shootingTime = 180;
        private int walkingTime = 120;
        private int warningTime = 120;
        private int chargingTime = 180;
        private int vulnerableTime = 180;
        private int deathTime = 90;
        private int deathTimer = 0;

        private const int BOSS_HEALTH = 35;
        private int RUN_SPEED = 10;

        private int random;

        private int chanceToCharge = 1;

        private int flashTime = 0;
        private int flashTimer = -1;

        Random rand;

        public Boss(Player p, int x, int y):base(p,x,y) {
            Location = new Rectangle(x, y, 185, 151);

            Image = ImageBank.bossIdle[0];
            health = BOSS_HEALTH;
            current = Enemystate.notSpawned;

            bulletImage = ImageBank.bossLaser[0];

            rand = new Random();
        }

        public override void Update() {
            if (current != Enemystate.notSpawned) { //boss has not spawned stage, where he's invisible
                base.Update();

                if (timer > 0) { //reset timer for shooting
                    timer--;
                }
                if (timer <= 0) {
                    shotFired = false;
                }

                if (health <= 0)
                {
                }
                else if (health < BOSS_HEALTH / 4) //boss flashes red at different intervals based on damage taken relative to health
                {
                    //col = Color.Red;
                    RUN_SPEED = 15; //run speed increases too
                    idleTime = 90;
                    flashTimer = 30;
                }
                else if (health < BOSS_HEALTH / 2)
                {
                    //col = Color.Red;
                    RUN_SPEED = 10;
                    idleTime = 90;
                    flashTimer = 60;
                }
                else
                {
                    col = Color.White;
                    RUN_SPEED = 10;
                    flashTimer = -1;
                    idleTime = 180;
                }

                if (flashTimer > 0) {//boss flashes red for increments of time when damaged
                    flashTime++;

                    if (flashTime >= flashTimer) {
                        flashTime = 0;
                        if (col == Color.Red) {
                            col = Color.White;
                        }
                        else {
                            col = Color.Red;
                        }
                    }
                }

                //player intersects with enemy location
                if (Location.Intersects(player.Location) && current != Enemystate.vulnerable && current != Enemystate.dead) {
                    if (current == Enemystate.crown) //collide with player in crownstate: tell map player won
                    {
                        map.GameOver = true;
                    }
                    else //otherwise, kill player
                    {
                        CollideWithPlayer();
                    }
                }

                if (current == Enemystate.standing) { //Idle stage
                    //stands idley, transitions to either offensive stage depending on distance from player

                    //set idle animation
                    Animate(ImageBank.bossIdle);

                    xSpeed = 0;

                    //face player
                    faceLeft = IsPlayerLeft();

                    stateTimer++; //stay in this state for predetermined number of frames
                    if (stateTimer >= idleTime) {
                        stateTimer = 0;

                        if (!CheckInRange()) //if player is far away, increase chance to charge
                        {
                            chanceToCharge = 2;
                        }
                        else
                        {
                            chanceToCharge = 1;
                        }

                        random = rand.Next(1,4);

                        if (random > chanceToCharge) { //shoot at player if random is greater than chance to charge
                            current = Enemystate.warning;
                        }
                        else {//charge at player if he's far
                            current = Enemystate.walking;
                        }
                    }
                }
                if (current == Enemystate.warning) { //warning stage
                    //shooting animation plays before actually shooting

                    //set sho0ting animation
                    Animate(ImageBank.bossShooting);

                    stateTimer++;//stay in this state for predetermined number of frames
                    if (stateTimer >= warningTime) {
                        stateTimer = 0;

                        random = rand.Next(0,2);

                        current = Enemystate.shooting;
                    }
                }
                if (current == Enemystate.shooting) { //Shooting stage
                    //starts shooting at player for predetermined frames

                    //set idle animation
                    Animate(ImageBank.bossShooting);

               ///     col = Color.Red;
                    if (random == 0) {
                        bulletY = 100;
                    }
                    else {
                        bulletY = 0;
                    }
                    if (!faceLeft) {
                        bulletX = Location.Width;
                    }
                    else {
                        bulletX = 0;
                    }
                    Shoot();

                    stateTimer++;//stay in this state for predetermined number of frames
                    if (stateTimer >= shootingTime) {
                        stateTimer = 0;

                        current = Enemystate.standing; //revert to standing state
                    }
                }
                if (current == Enemystate.walking) { //pre-Chasing stage
                    //walks twords player slowly for predetermined frames before charging
              //      col = Color.Pink;

                    //set idle animation
                    Animate(ImageBank.bossChasing);

                    //keep facing player //this is not done in the charge method, so he can be dodged then
              //      faceLeft = IsPlayerLeft();

                    if (faceLeft) {
                        xSpeed = -2;
                    }
                    else {
                        xSpeed = 2;
                    }

                    stateTimer++;//stay in this state for predetermined number of frames
                    if (stateTimer >= walkingTime) {
                        stateTimer = 0;

                        current = Enemystate.chasing;
                    }
                }
                if (current == Enemystate.chasing) { //Chasing stage
                    //charges straight forward for predetermined frames or until hits wall

               //     col = Color.Yellow;

                    //set idle animation
                    Animate(ImageBank.bossChasing);

                    //does not redirect to face player in this state, will keep chargin past him

                    if (xSpeed == 0) { //crashing into wall
                        stateTimer = 0;
                        current = Enemystate.vulnerable; //transition to vulnerable state
                    }

                    if (faceLeft) { //Set to maximum run speed in direction faced previously
                        xSpeed = -RUN_SPEED;
                    }
                    else {
                        xSpeed = RUN_SPEED;
                    }

                    stateTimer++;//stay in this state for predetermined number of frames
                    if (stateTimer >= chargingTime) {
                        stateTimer = 0;

                        current = Enemystate.standing;
                    }
                }
                if (current == Enemystate.vulnerable) { //vulnerable stage
                  //  col = Color.Orange;
                    xSpeed = 0;

                    //set idle animation
                    Animate(ImageBank.bossVulnerable);

                    stateTimer++;//stay in this state for predetermined number of frames
                    if (stateTimer >= vulnerableTime) {
                        stateTimer = 0;

                        current = Enemystate.standing;
                    }
                }
                if (current == Enemystate.dead) //boss is dead
                {
                    Image = ImageBank.bossDeath[0]; //explosion
                    xSpeed = 0;
                    ySpeed = 0;

                    deathTimer++; //hold for a certain number of frames
                    if (deathTimer >= deathTime)
                    {
                     //   map.Movables.Add(new GoldHat(player, Location.X, Location.Y));
                        ySpeed = -5;
                        current = Enemystate.crown; //then become the crown
                        col = Color.White;
                        Image = ImageBank.goldHat;
                        Location = new Rectangle(Location.X, Location.Y, 87, 78);
                        health = BOSS_HEALTH;
                    }
                }
            }
            else {
                if (CheckInRange()) {
                    current = Enemystate.standing; //spawn boss suddenly
                }
            }
        }

        private Boolean IsPlayerLeft() { //returns true if player is to the left
            return player.Location.Center.X < Location.Center.X;
        }

        public override void Draw(SpriteBatch sb) {
            if (current != Enemystate.notSpawned) {
                    base.Draw(sb);
            }
        }

        //player gets within range on X and Y coordinates, set state to walk
        public bool CheckInRange() {
            return Math.Abs(player.Location.Center.X - Location.Center.X) <= Math.Abs(range);
            //return Location.X - player.Location.X <= range && player.Location.X - (Location.X+Location.Width) <=range;
            /*
            if (Math.Abs(player.Location.X - Location.X) <= Math.Abs(range)) {
                current = Enemystate.standing;
            }*/
        }

        public override void CollideWithPlayer() {//kill player unless hes defending, inwhich case boss takes damage
            //if (current != Enemystate.vulnerable && current != Enemystate.dead ) { 
                if (player.State == Player.Playerstate.defending) {
                    current = Enemystate.vulnerable;
                    TakeDamage(5);
                }
                player.TakeHit();
           // }
        }

        public override void Reset() {//respawn boss
            
            base.Reset();
            health = BOSS_HEALTH;
            col = Color.White;
            Location = new Rectangle(spawnX, spawnY, 185, 151);
            //current = Enemystate.notSpawned;
        }

        public override void TakeDamage(int damage) {
            if (current == Enemystate.vulnerable || current == Enemystate.walking || current == Enemystate.chasing || current == Enemystate.shooting) {
                base.TakeDamage(damage);
            }
        }
    }
}
