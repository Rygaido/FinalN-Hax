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
       // public GameObject background;
        private GameObject[,] grid; //contains all game objects
        private int row;
        private int col;
        public static Vector2 scroll; //hold the change in X and Y position due to scrolling

        private List<Movable> movables; //List holds all moving objects
        public List<Movable> Movables {
            get { return movables; }
            set { movables = value; }
        }
        private List<Movable> temp; //holds a duplicate list for foreachloops
        private List<Movable> resetList;
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

        public Map(Player player){
            p = player;
            p.Map = this;
            movables = new List<Movable>();
        }

        //has update and draw methods similar to game object
        //update all movables and grid objects 
        public void Update() {

            temp = new List<Movable>(movables);

            //reset if player falls off of map
            if (p.Location.Y > row*50+600) {
                Reset();
            }//*/

            if(goal!=null){
                checkWin = p.Location.Intersects(goal.Location);
            }

            //loop through all objects in grid
            for (int i = 0; i < grid.GetLongLength(0); i++){
                for (int j = 0; j < grid.GetLongLength(1); j++) {
                    if (grid[i, j] != null) { //do nothing for null objects, empty space, a lot of that

                        //update object
                        grid[i, j].Update();

                        try { //if the object is a wall, check collision with player and all movables
                            Platform w = (Platform)grid[i, j];
                            w.checkPlayer(p);

                            foreach (Movable m in temp) {
                                w.checkObject(m);
                            }
                        }
                        catch (Exception e) {} //try catch is needed to typecast a GameObject into a Wall sub-object
                        //and exception being thrown simply means gameobject was not a wall and can be ignored for now
                    }
                }
            }
            foreach (Movable m in temp) { //loop through all movables
                if (m.Active == true) { //only update active movables
                    m.Update();

                    try {//check if object is a bullet
                        Projectile b = (Projectile)m;

                        if (b.Hostile == false) {//check if object is player's bullet
                            //if so, check object against all enemies in Movables
                            foreach (Movable n in temp) {
                                try {//check if object is an enemy
                                    Enemy e = (Enemy)n;

                                    //bullet collides with enemy
                                    if (b.Location.Intersects(e.Location)) {
                                        e.Active = false;
                                        b.Active = false;
                                    }
                                }
                                catch (Exception e) { } //object was not an enemy
                            }
                        }
                        else {
                            //enemy's bullet collides with player
                            if (b.Location.Intersects(p.Location)) {
                                p.TakeHit();
                                b.Active = false;
                            }
                        }
                    }
                    catch (Exception e) {} //object was not a projectile
                }
                else {//remove inactive movables
                    movables.Remove(m);
                }
            }
          //  background.Update();
            //use the overload of update: Update(scroll);
            //that way all objects will move with grid instead of screen
        }


        //draw all gameobjects in grid and movables in list
        public void Draw(SpriteBatch sb) {

            temp = new List<Movable>(movables);

         //   background.Draw(sb);
            for (int i = 0; i < grid.GetLongLength(0); i++) {
                for (int j = 0; j < grid.GetLongLength(1); j++) {
                    if (grid[i, j] != null) {
                        grid[i, j].Draw(sb);
                    }
                }
            }
            foreach (Movable m in temp) {
                m.Draw(sb);
            }

            //draw player spawn and current scroll coordinates for debug purposes
           // sb.DrawString(ImageBank.font,playerSpawn.X+" "+playerSpawn.Y+"\n"+scroll.X+" "+scroll.Y,new Vector2(0,0),Color.BlueViolet);
        }
        //Reset all enemies on map
        public void Reset() {
            p.Reset();

            movables = new List<Movable>(resetList);
            temp = new List<Movable>(movables);

            foreach (Movable m in temp) {
                m.Reset();
            }
        }

        //check for collisions between player movables and grid
        public void CheckCollisions() {
            //method stub
        }

        //read a file and populated the grid with objects
        public void Load(string level) {
            BinaryReader reader;

            //Get testmap data file
            try {
                reader = new BinaryReader(File.Open(level+".dat", FileMode.Open));
            }
            catch (Exception e) {
                throw e;
            }

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
                    else if((int)m < ((int)'a'+50)){ //otherwise just place a wall for now
                        grid[i, j] = new Platform();
                        grid[i, j].Location = new Rectangle(j * 50, i * 50, 50, 50);

                        grid[i, j].Image = ImageBank.platforms[(int)m-((int)'a' + 1)];
                    }
                    else if (m == (char)((int)'a'+50)) { //'a'+50 should be the basic enemy
                        grid[i, j] = null; //treat as blank space

                        //then add a new minion on spot to Que of Movables
                        WalkingMinion mini = new WalkingMinion(p, j * 50, i * 50-30);

                        mini.Map = this; //give enemy reference to this map object

                        //mini.Location = new Rectangle(j * 50, i * 50, 50, 50);
                        movables.Add((Movable)mini);
                    }
                    else if (m == (char)((int)'a' + 51)) { //'a'+50 should be the basic enemy
                        grid[i, j] = null; //treat as blank space

                        //then add a new minion on spot to Que of Movables
                        ShootingMinion e2= new ShootingMinion(p, j * 50, i * 50 - 30);

                        e2.Map = this; //give enemy reference to this map object

                        //mini.Location = new Rectangle(j * 50, i * 50, 50, 50);
                        movables.Add((Movable)e2);
                    }
                    else if (m == (char)((int)'a' + 52)) { //'a'+50 should be the basic enemy
                        grid[i, j] = null; //treat as blank space

                        //then add a new minion on spot to Que of Movables
                        LampMinion e2 = new LampMinion(p, j * 50, i * 50 - 30);

                        e2.Map = this; //give enemy reference to this map object

                        //mini.Location = new Rectangle(j * 50, i * 50, 50, 50);
                        movables.Add((Movable)e2);
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
                        grid[i, j] = new Platform();
                        grid[i, j].Location = new Rectangle(j * 50, i * 50, 50, 50);

                    }
                }
            }
            reader.Close();

            resetList = new List<Movable>(Movables);
            Reset();
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
