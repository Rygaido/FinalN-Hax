﻿#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
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

        SoundEffect music;
        SoundEffectInstance musicInstance;

        bool win = false;
        
        KeyboardState previous; //previous state of keyboard
        MouseState mousePrevious; //previous state of the mouse

        //bools controlling pause screen
        private bool paused;
        private bool pausedKeyDown = false;
        private bool pausedForGuide = false;

        private bool textMode = false;
        InputWindow textBox;

        List<string> levels=new List<string>();
        int currentLevel = 0;

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

            textBox = new InputWindow(player);

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
            
            //load list of levels
           // levels.Add("testLevel");
          // /*
            levels.Add("levelOne");
            levels.Add("levelTwo");
            levels.Add("levelThree");
            levels.Add("levelFour");
            levels.Add("levelFive");
            levels.Add("levelSix");
            levels.Add("levelSeven");//*/
            levels.Add("bossLevel");

            //create new map
            map = new Map(player);
            map.Load(levels[0]);

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
            ImageBank.playerStandShoot.Add(Content.Load<Texture2D>("idle1shoot"));
            ImageBank.playerStandShoot.Add(Content.Load<Texture2D>("idle2shoot"));
            ImageBank.playerStandDefend.Add(Content.Load<Texture2D>("idle1shield"));
            ImageBank.playerStandDefend.Add(Content.Load<Texture2D>("idle2shield"));
            ImageBank.playerWalk.Add(Content.Load<Texture2D>("run1"));
            ImageBank.playerWalk.Add(Content.Load<Texture2D>("run2"));
            ImageBank.playerWalkShoot.Add(Content.Load<Texture2D>("run1shoot"));
            ImageBank.playerWalkShoot.Add(Content.Load<Texture2D>("run2shoot"));
            ImageBank.playerWalkDefend.Add(Content.Load<Texture2D>("run1shield"));
            ImageBank.playerWalkDefend.Add(Content.Load<Texture2D>("run2shield"));
            ImageBank.playerJump.Add(Content.Load<Texture2D>("jump"));
            ImageBank.playerAttack.Add(Content.Load<Texture2D>("shootingaction"));
            ImageBank.playerDefend.Add(Content.Load<Texture2D>("shieldaction"));
            ImageBank.winscreen = Content.Load<Texture2D>("Winscreen");
            ImageBank.walkingMinion.Add(Content.Load<Texture2D>("enemy1"));
            ImageBank.walkingMinion.Add(Content.Load<Texture2D>("enemy1_flip")); 
            ImageBank.shootingMinion.Add(Content.Load<Texture2D>("enemy2idle"));
            ImageBank.lampMinion.Add(Content.Load<Texture2D>("lamp"));
            ImageBank.lampMinionBroken.Add(Content.Load<Texture2D>("lampbroke"));
            ImageBank.goal = Content.Load<Texture2D>("goal");
            ImageBank.pausemessage = Content.Load<Texture2D>("pausescreen");
            ImageBank.looseScreen = Content.Load<Texture2D>("losescreen");
            ImageBank.bullet = Content.Load<Texture2D>("New Canvas");
            ImageBank.background = Content.Load<Texture2D>("800back");
            ImageBank.win_background = Content.Load<Texture2D>("youwin2");
            ImageBank.konami_background=Content.Load<Texture2D>("konamibackground");
            ImageBank.playerBullet = Content.Load<Texture2D>("bullet");
            ImageBank.keys = Content.Load<Texture2D>("keys");
            ImageBank.goldHat = Content.Load<Texture2D>("GOLD");

            ImageBank.bossIdle.Add(Content.Load<Texture2D>("bosstank"));
            ImageBank.bossChasing.Add(Content.Load<Texture2D>("chargetank"));
            ImageBank.bossShooting.Add(Content.Load<Texture2D>("robotturtle"));
            ImageBank.bossVulnerable.Add(Content.Load<Texture2D>("vunerable"));
            ImageBank.bossLaser.Add(Content.Load<Texture2D>("laser"));
            ImageBank.bossDeath.Add(Content.Load<Texture2D>("EXPLOSION"));

            ImageBank.platforms.Add(Content.Load<Texture2D>("platform"));
            ImageBank.platforms.Add(Content.Load<Texture2D>("shelfblock"));
            ImageBank.platforms.Add(Content.Load<Texture2D>("edge1"));
            ImageBank.platforms.Add(Content.Load<Texture2D>("edge2"));

            ImageBank.font = Content.Load<SpriteFont>("font1");
            ImageBank.square = Content.Load<Texture2D>("WhiteSquare");

            music = Content.Load<SoundEffect>("HaxMusicFinal");
            musicInstance = music.CreateInstance();
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

            if (musicInstance.State == SoundState.Stopped) //start music
            {
                musicInstance.Volume = 1.0f; //full volume, loop
                musicInstance.IsLooped = true;
                musicInstance.Play();
            }
            else //keep playing music
            {
                musicInstance.Resume();
            }

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

                if (textMode) { //if text mode was true, set it to false and process cheats
                    textMode = false;
                    textBox.ProcessCheat();
                }
            }
            if (current.IsKeyDown(Keys.LeftShift)) {
                textMode = true;
                BeginPaused(true);
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
                         //Exit();
                         NextLevel();
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
            if (paused == false) {

                //check if a key is currently pressed, but wasn't before, call appropriate method on player
                if (current.IsKeyDown(Keys.Left)) {// && !previous.IsKeyDown(Keys.Left)) {
                    player.LeftKey(); //left key can be held
                }
                if (current.IsKeyDown(Keys.Right)) {// && !previous.IsKeyDown(Keys.Right)) {
                    player.RightKey(); //right key can be held
                }
                if (current.IsKeyDown(Keys.Up) && !previous.IsKeyDown(Keys.Up)) {
                    player.UpKey();
                }
                if (!current.IsKeyDown(Keys.Up) && previous.IsKeyDown(Keys.Up)) {
                    player.ReleaseUpKey();
                }
                if (current.IsKeyDown(Keys.Down)) {
                    player.DownKey();
                }
                if (current.IsKeyDown(Keys.S)) {
                    player.SKey();
                }
                if (current.IsKeyDown(Keys.D)) {
                    player.DKey();
                }
                if (current.IsKeyDown(Keys.F)) {
                    player.FKey();
                }

                player.Update();

                map.Update();

                if (map.CheckWin && current.IsKeyDown(Keys.Down)) { //player pressed down key on a staircase
                    win = true;
                }
                if (map.GameOver) //player has touched the golden hat
                {
                    NextLevel();
                }
            }
            else {
                textBox.ReadKeyboard(current, previous);
            }

            //store previous state of keyboard
            previous = current;
            mousePrevious = mouseCurrent;

            if (background.Image == ImageBank.background && textBox.Konami)
            {
                background.Image = ImageBank.konami_background;
            }
            if (background.Image == ImageBank.konami_background && !textBox.Konami)
            {
                background.Image = ImageBank.background;
            }

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
            player.Draw(spriteBatch);//*/

            if (paused == true)
            {
                if (!textMode) {
                    pausemessagething.Draw(spriteBatch);
                }
                else {
                    textBox.Draw(spriteBatch);
                }
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

        public void NextLevel() { //transition to next level
            win = false;
            paused = false;
            currentLevel++;

            if (currentLevel >= levels.Count) //if no more levels
            {
                map = new Map(player); //make blank map, change background to win screen and remove player from game area
                background.Image = ImageBank.win_background;
                player.Location = new Rectangle(999, 999, 1, 1);
            }
            else//otherwise, load next level
            {
                map = new Map(player);
                map.Load(levels[currentLevel]);
            }
        }
    }
}
