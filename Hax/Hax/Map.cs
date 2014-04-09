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

//This object was not part of the original tree, but I think controlling objects is going
//to be more complicated than originally expected. making a map object would make 
//it much easier to control the grid, espescially when scrolling

//Map contains a matrix of GameObjects, initial values read from a file
//and a queue containing all moving objects
//makes up the entire level except for the player
namespace Hax {
    class Map {
        private GameObject[,] grid; //contains all game objects
        private Vector2 scroll; //hold the change in X and Y position due to scrolling

        private Queue<Movable> movables; //que holds all moving objects
        //enemies, projectiles and platforms not constrained to grid
        //Idea: Enemies are loaded from file to grid, so make them inactive until they scroll
        //onto screen, then activate them, remove from grid and add them to queue

        //has update and draw methods similar to game object
        //update all movables and grid objects 
        public void Update() {
            //method stub

            //use the overload of update: Update(scroll);
            //that way all objects will move with grid instead of screen
        }
        public void Draw(SpriteBatch sb) {
            //method stub
        }

        //check for collisions between player movables and grid
        public void CheckCollisions(Player p) {
            //method stub
        }

        //read a file and populated the grid with objects
        private void Load() {
            //method stub
        }
        private void Clear() { //empty grid and queue, prepare for next room
            //method stub
        }

        //create a method to get the section of the grid that is visable on screen,
        //cropping the rest of grid that scrolled out of sight
        //This might be useful to improve runtime by not wasting time drawing and 
        //checking collisions on uneccessary objects. 
        //Our game may not even be big enough to need this - LOW PRIORITY
        private GameObject[,] OnScreen() {
            return null; //method stub
        }
    }
}
