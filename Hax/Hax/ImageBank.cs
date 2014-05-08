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
    static class ImageBank {

        public static Texture2D defaultImage; //default image assigned to objects, used for testing


        //list of frames in all of player's animations
        public static List<Texture2D> playerStand = new List<Texture2D>();
        public static List<Texture2D> playerStandShoot = new List<Texture2D>();
        public static List<Texture2D> playerStandDefend = new List<Texture2D>();
        public static List<Texture2D> playerWalk = new List<Texture2D>();
        public static List<Texture2D> playerWalkShoot = new List<Texture2D>();
        public static List<Texture2D> playerWalkDefend = new List<Texture2D>();
        public static List<Texture2D> playerJump = new List<Texture2D>();
        public static List<Texture2D> playerAttack = new List<Texture2D>();
        public static List<Texture2D> playerDefend = new List<Texture2D>();
        public static List<Texture2D> playerShieldBreak = new List<Texture2D>();

        //lists of frames for all enemies' animations
        public static List<Texture2D> walkingMinion=new List<Texture2D>();
        public static List<Texture2D> shootingMinion = new List<Texture2D>();
        public static List<Texture2D> lampMinion = new List<Texture2D>();
        public static List<Texture2D> lampMinionBroken = new List<Texture2D>();

        //boss minion
        public static List<Texture2D> bossIdle = new List<Texture2D>();
        public static List<Texture2D> bossChasing = new List<Texture2D>();
        public static List<Texture2D> bossShooting = new List<Texture2D>();
        public static List<Texture2D> bossVulnerable = new List<Texture2D>();

        //sprites for player and enemy bullets
        public static Texture2D bullet;
        public static Texture2D playerBullet;

        //sprites for other objects
        public static Texture2D goal;
        public static Texture2D pausemessage;
        public static Texture2D winscreen;
        public static Texture2D looseScreen;
        public static Texture2D wallImage;
        public static List<Texture2D> platforms = new List<Texture2D>();
        public static Texture2D background;

        public static SpriteFont font; //a spritefont//used to draw text

        public static Texture2D square; //a white square
    }
}
