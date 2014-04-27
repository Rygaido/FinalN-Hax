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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Map map; //map contains a grid of game objects read from a file
        Player player; //THE player object
        GameObject background;

        //temporary screens for certain situations
        GameObject pausemessagething;
        GameObject winscreenpopup;
        GameObject loosescreenpopup;

        //buttons for win and lose screens
        GameObject resetButton;
        GameObject continueButton;

        bool win = false;
        
        KeyboardState previous; //previous state of keyboard
        MouseState mousePrevious; //previous state of the mouse

        //bools controlling pause screen
        private bool paused;
        private bool pausedKeyDown = false;
        private bool pausedForGuide = false;

        public Game1()
            : base() {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize() {
            base.Initialize();

            
            //spawn a player and a wall here for testing purposes
            pausemessagething = new GameObject();
            pausemessagething.Location = new Rectangle(170, 100, 500, 200);
            pausemessagething.Image = ImageBank.pausemessage;


            player = new Player();

            //set up winscreen
            winscreenpopup = new GameObject();
            winscreenpopup.Location = new Rectangle(170,100,500,400);
            winscreenpopup.Image = ImageBank.winscreen;

            //set up lose screen
            loosescreenpopup = new GameObject();
            loosescreenpopup.Location = new Rectangle(170, 100, 500, 400);
            loosescreenpopup.Image = ImageBank.looseScreen;
            
            //set up reset and continue buttons
            resetButton = new GameObject();
            resetButton.Location = new Rectangle(370,380,80,35);

            continueButton = new GameObject();
            continueButton.Location = new Rectangle(resetButton.Location.Right+0, 380, 80, 40);
            
            //create new map
            map = new Map(player);
            map.Load();

            //load background, set it in place
            background = new GameObject();
            background.Location = new Rectangle(0, 0, 800, 600);
            background.Image = ImageBank.background;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ImageBank.defaultImage=Content.Load<Texture2D>("enemy1");
            ImageBank.wallImage  = Content.Load<Texture2D>("platform");

            //load sprites into imagebank here
            ImageBank.playerStand.Add(Content.Load<Texture2D>("idle1"));
            ImageBank.playerStand.Add(Content.Load<Texture2D>("idle2"));
            ImageBank.playerWalk.Add(Content.Load<Texture2D>("run1"));
            ImageBank.playerWalk.Add(Content.Load<Texture2D>("run2"));
            ImageBank.playerJump.Add(Content.Load<Texture2D>("jump"));
            ImageBank.playerAttack.Add(Content.Load<Texture2D>("shootingaction"));
            ImageBank.playerDefend.Add(Content.Load<Texture2D>("shieldaction"));
            ImageBank.winscreen = Content.Load<Texture2D>("win");
            ImageBank.walkingMinion.Add(Content.Load<Texture2D>("enemy1"));
            ImageBank.walkingMinion.Add(Content.Load<Texture2D>("enemy1_flip")); 
            ImageBank.shootingMinion.Add(Content.Load<Texture2D>("enemy2idle"));
            ImageBank.goal = Content.Load<Texture2D>("goal");
            ImageBank.pausemessage = Content.Load<Texture2D>("wordart");
            ImageBank.looseScreen = Content.Load<Texture2D>("gameoverscreen");
            ImageBank.bullet = Content.Load<Texture2D>("New Canvas");
            ImageBank.background = Content.Load<Texture2D>("800back");
            ImageBank.playerBullet = Content.Load<Texture2D>("bullet");

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent() {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //get current state of keyboard
            KeyboardState current = Keyboard.GetState();
            MouseState mouseCurrent = Mouse.GetState();

            if (current.IsKeyDown(Keys.P)) //pause key P
            {
                BeginPaused(true);

            }
            if (current.IsKeyDown(Keys.Enter)) //unpause key enter
            {
                EndPause(true);
                
            }
            if (win) //show win screen
            {
                WinScreen(true);
                paused = true;
                //place buttons on win screen
                resetButton.Location = new Rectangle(370, 380, 80, 35);
                 continueButton.Location = new Rectangle(resetButton.Location.Right + 0, 380, 80, 40);
                //check if buttons are clicked
                 if (mouseCurrent.LeftButton == ButtonState.Pressed) {
                     if (mouseCurrent.X > resetButton.Location.Left && mouseCurrent.X < resetButton.Location.Right && mouseCurrent.Y > resetButton.Location.Top && mouseCurrent.Y < resetButton.Location.Bottom) {
                         Reset();
                     }
                     if (mouseCurrent.X > continueButton.Location.Left && mouseCurrent.X < continueButton.Location.Right && mouseCurrent.Y > continueButton.Location.Top && mouseCurrent.Y < continueButton.Location.Bottom) {
                         Exit();
                     }
                 }
            }
            if (player.IsDead()) { //show gameover screen
                paused = true;

                //place buttons on gameover screen
                continueButton.Location = new Rectangle(360, 374, 80, 35);
                resetButton.Location = new Rectangle(continueButton.Location.Right, 374, 80, 30);
                //check if buttons are pressed
                if (mouseCurrent.LeftButton == ButtonState.Pressed) {
                    if (mouseCurrent.X > resetButton.Location.Left && mouseCurrent.X < resetButton.Location.Right && mouseCurrent.Y > resetButton.Location.Top && mouseCurrent.Y < resetButton.Location.Bottom) {
                        Reset();
                    }
                    if (mouseCurrent.X > continueButton.Location.Left && mouseCurrent.X < continueButton.Location.Right && mouseCurrent.Y > continueButton.Location.Top && mouseCurrent.Y < continueButton.Location.Bottom) {
                        Exit();
                    }
                }
            }

            
            //  checkPause(Keyboard.GetState());
            //checkPauseGuide();

            // TODO: Add your update logic here
            if (paused == false) 
            {

                //check if a key is currently pressed, but wasn't before, call appropriate method on player
                if (current.IsKeyDown(Keys.Left))
                {// && !previous.IsKeyDown(Keys.Left)) {
                    player.LeftKey(); //left key can be held
                }
                if (current.IsKeyDown(Keys.Right))
                {// && !previous.IsKeyDown(Keys.Right)) {
                    player.RightKey(); //right key can be held
                }
                if (current.IsKeyDown(Keys.Up) && !previous.IsKeyDown(Keys.Up))
                {
                    player.UpKey();
                }
                if (!current.IsKeyDown(Keys.Up) && previous.IsKeyDown(Keys.Up))
                {
                    player.ReleaseUpKey();
                }
                if (current.IsKeyDown(Keys.Down))
                {
                    player.DownKey();
                }
                if (current.IsKeyDown(Keys.S)) {
                    player.SKey();
                }
                if (current.IsKeyDown(Keys.D)) {
                    player.DKey();
                }

                player.Update();

                map.Update();
                
                if (map.CheckWin && current.IsKeyDown(Keys.Down))
                {
                    win = true;
                   // player.Reset();
                    //enemy.Reset();
                }

            }
            else if (win == true) {
                if (mouseCurrent.LeftButton == ButtonState.Pressed) {
                    if (mouseCurrent.X > resetButton.Location.Left && mouseCurrent.X < resetButton.Location.Right && mouseCurrent.Y > resetButton.Location.Top && mouseCurrent.Y < resetButton.Location.Bottom) {
                        Reset();
                    }
                    if (mouseCurrent.X > continueButton.Location.Left && mouseCurrent.X < continueButton.Location.Right && mouseCurrent.Y > continueButton.Location.Top && mouseCurrent.Y < continueButton.Location.Bottom) {
                        Exit();
                    }
                }
            }

            //store previous state of keyboard
            previous = current;
            mousePrevious = mouseCurrent;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
           
            spriteBatch.Begin();

            background.Draw(spriteBatch);
            map.Draw(spriteBatch);
         /*   enemy.Draw(spriteBatch);
            wally.Draw(spriteBatch);
            wally2.Draw(spriteBatch);
            wally3.Draw(spriteBatch);
            wally4.Draw(spriteBatch);
            goal.Draw(spriteBatch);*/
            player.Draw(spriteBatch);//*/

            if (paused == true)
            {

                pausemessagething.Draw(spriteBatch);
            }

            if (win == true)
            { 
                winscreenpopup.Draw(spriteBatch);

            }
            if (player.IsDead()) {
                loosescreenpopup.Draw(spriteBatch);
            }
            //winscreenpopup.Draw(spriteBatch);
            //resetButton.Draw(spriteBatch);
            //continueButton.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
        //method to begin pauseing
        private void BeginPaused(bool UserInitiated)
        {
            paused = true;
            pausedForGuide = !UserInitiated;
        }
        //method to end pausing
        private void EndPause(bool letsplay)
        {
            if (letsplay == true)
            {
                paused = false;
                pausedForGuide = false;
            }
            
        }
        //helper method for win screen
        private void WinScreen(bool winthing)
        {
             if (winthing == true)
             {
              win = true;
             }
        }

        //resets player and map, un-ends the game and allows level to be replayed
        private void Reset() {
            //player.Reset();
            map.Reset();
            EndPause(true);
            win = false;
        }
    }
}
