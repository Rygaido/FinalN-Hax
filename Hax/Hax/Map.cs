#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using System.IO;
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
        private int row;
        private int col;
        public static Vector2 scroll; //hold the change in X and Y position due to scrolling

        private List<Movable> movables; //List holds all moving objects
        //enemies, projectiles and platforms not constrained to grid
        //Idea: Enemies are loaded from file to grid, so make them inactive until they scroll
        //onto screen, then activate them, remove from grid and add them to queue

        private Player p; //reference to player objecy

        private Goal goal;
        private bool checkWin;
        public bool CheckWin{
            get { return checkWin; }
        }

        private Point playerSpawn;
        public Point PlayerSpawn {
            get { return playerSpawn; }
            set { playerSpawn = value; }
        }

        public Map(Player player) {
            //playerSpawn = new Point(0, 0);
            p = player;
            p.map = this;
            movables = new List<Movable>();
        }

        //has update and draw methods similar to game object
        //update all movables and grid objects 
        public void Update() {
            
            ///*
            if (p.Location.Y > row*100) {
                Reset();
            }//*/

            checkWin = p.Location.Intersects(goal.Location);

            //method stub
            for (int i = 0; i < grid.GetLongLength(0); i++){
                for (int j = 0; j < grid.GetLongLength(1); j++) {
                    if (grid[i, j] != null) {
                        grid[i, j].Update();

                        try {
                            Wall w = (Wall)grid[i, j];
                            w.checkPlayer(p);

                            foreach (Movable m in movables) {
                                w.checkObject(m);
                            }
                        }
                        catch (Exception e) {}


                    }
                }
            }//*/
            foreach (Movable m in movables) {
                m.Update();
            }

            //use the overload of update: Update(scroll);
            //that way all objects will move with grid instead of screen
        }


        //draw all gameobjects in grid and movables in list
        public void Draw(SpriteBatch sb) {
            for (int i = 0; i < grid.GetLongLength(0); i++) {
                for (int j = 0; j < grid.GetLongLength(1); j++) {
                    if (grid[i, j] != null) {
                        grid[i, j].Draw(sb);
                    }
                }
            }
            foreach (Movable m in movables) {
                m.Draw(sb);
            }
            //method stub
        }
        //Reset all enemies on map
        public void Reset() {
            p.Reset();

            foreach (Movable m in movables) {
                try {
                    Enemy e = (Enemy)m;
                    e.Reset();
                }//*/
                catch (Exception e) { }
            }
        }

        //check for collisions between player movables and grid
        public void CheckCollisions() {
            //method stub
        }

        //read a file and populated the grid with objects
        public void Load() {
            //Get testmap data file
            BinaryReader reader = new BinaryReader(File.Open("TestMap.dat",FileMode.Open));

            //write number of rows, then columns then the string
            int r = reader.ReadInt32();
            row = r;
            int c = reader.ReadInt32();
            col = c;
            string s = reader.ReadString();
            char[] chars = s.ToCharArray();

            grid = new GameObject[r, c]; //populate matrix of GameObjects
            for (int i = 0; i < r; i++){//loop through all rows and cols
                for (int j = 0; j < c; j++){

                    char m = chars[(i * c + j)];

                    if (m == 'a') { //'a' is a blank space
                        grid[i, j] = null;
                    }
                    else if (m == (char)((int)'a'+50)) { //'a'+50 should be the basic enemy
                        grid[i, j] = null; //treat as blank space

                        //then add a new minion on spot to Que of Movables
                        WalkingMinion mini = new WalkingMinion(p, j * 50, i * 50-30);

                        //mini.Location = new Rectangle(j * 50, i * 50, 50, 50);
                        movables.Add((Movable)mini);
                    }
                    else if (m == (char)((int)'a' + 150)) { //'a'+150 Player spawn
                        grid[i, j] = null; //treat as blank space

                        playerSpawn = new Point(j*50,i*50);
                    }
                    else if (m == (char)((int)'a' + 151))
                    { //'a'+151 Player goal
                        grid[i, j] = new Goal(); //treat as blank space

                        grid[i,j].Location= new Rectangle(j * 50, i * 50,50,50);
                        goal = (Goal)grid[i, j];
                    }
                    else { //otherwise just place a wall for now
                        grid[i, j] = new Wall();
                        grid[i, j].Location = new Rectangle(j * 50, i * 50, 50, 50);
                    }
                }
            }

            reader.Close();
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
