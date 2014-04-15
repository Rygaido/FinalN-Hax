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
        Wall wally; //a lone wall used for testing
        Wall wally2; //another lone wall used for testing

        Wall wally3;
        Wall wally4;
        GameObject pausemessagething;
        GameObject winscreenpopup;
        GameObject loosescreenpopup;

        GameObject resetButton;
        GameObject continueButton;

        WalkingMinion enemy;
        Goal goal;
        bool win = false;
        
        int animationTimer = 0;
        

        KeyboardState previous; //previous state of keyboard
        MouseState mousePrevious;

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
            wally = new Wall();
            wally.Location = new Rectangle(50, 400, 100, 100);
            wally2 = new Wall();
            wally2.Location = new Rectangle(300, 400, 500, 100);
           // wally2.Location = new Rectangle(125, 300, 900, 200);
            wally3 = new Wall();
            //wally3.Location = new Rectangle(300, 300, 100, 100);
            wally4 = new Wall();
            wally4.Location = new Rectangle(550, 300, 100, 100);
            goal = new Goal();
            winscreenpopup = new GameObject();
            winscreenpopup.Location = new Rectangle(170,100,500,400);
            winscreenpopup.Image = ImageBank.winscreen;

            loosescreenpopup = new GameObject();
            loosescreenpopup.Location = new Rectangle(170, 100, 500, 400);
            loosescreenpopup.Image = ImageBank.looseScreen;
            

            resetButton = new GameObject();
            //204,200 / 505,282
            //int xx=winscreenpopup.Location.X + (int)((204.0 / 505) * winscreenpopup.Location.Width);
            //int yy=winscreenpopup.Location.Y + (int)((200.0 / 282) * winscreenpopup.Location.Height);
            //resetButton.Location = new Rectangle(xx, yy, (int)((271.0 / 505) * winscreenpopup.Location.Width)-xx, (int)((220.0 / 282) * winscreenpopup.Location.Height)-yy);
            resetButton.Location = new Rectangle(370,380,80,35);

            continueButton = new GameObject();
            continueButton.Location = new Rectangle(resetButton.Location.Right+0, 380, 80, 40);
            //284,199 / 505,282
            //xx = winscreenpopup.Location.X + (int)((284.0 / 505) * winscreenpopup.Location.Width);
           // yy = winscreenpopup.Location.Y + (int)((199.0 / 282) * winscreenpopup.Location.Height);
            //continueButton.Location = new Rectangle(xx, yy, (int)((365.0 / 505) * winscreenpopup.Location.Width) - xx, (int)((220.0 / 282) * winscreenpopup.Location.Height) - yy);



            enemy = new WalkingMinion(player,500,300);

            map = new Map(player);
            map.Load();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent() {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ImageBank.defaultImage=Content.Load<Texture2D>("enemy");
            ImageBank.wallImage  = Content.Load<Texture2D>("platform");

            //mario sprites are just placeholders //remember to replace them before releasing game
            ImageBank.playerStand.Enqueue(Content.Load<Texture2D>("idle1"));
            ImageBank.playerStand.Enqueue(Content.Load<Texture2D>("idle2"));
            ImageBank.playerWalk.Enqueue(Content.Load<Texture2D>("run1"));
            ImageBank.playerWalk.Enqueue(Content.Load<Texture2D>("run2"));
            ImageBank.playerJump.Enqueue(Content.Load<Texture2D>("jump"));
            ImageBank.winscreen = Content.Load<Texture2D>("win");
            ImageBank.walkingMinion = Content.Load<Texture2D>("walkingMinion");
            ImageBank.goal = Content.Load<Texture2D>("goal");
            ImageBank.pausemessage = Content.Load<Texture2D>("wordart");
            ImageBank.looseScreen = Content.Load<Texture2D>("gameoverscreen");
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

            if (current.IsKeyDown(Keys.P))
            {
                BeginPaused(true);

            }
            if (current.IsKeyDown(Keys.Enter))
            {
                EndPause(true);
                
            }
            if (win)
            {
                WinScreen(true);
                paused = true;
                resetButton.Location = new Rectangle(370, 380, 80, 35);
                 continueButton.Location = new Rectangle(resetButton.Location.Right + 0, 380, 80, 40);
                 if (mouseCurrent.LeftButton == ButtonState.Pressed) {
                     if (mouseCurrent.X > resetButton.Location.Left && mouseCurrent.X < resetButton.Location.Right && mouseCurrent.Y > resetButton.Location.Top && mouseCurrent.Y < resetButton.Location.Bottom) {
                         Reset();
                     }
                     if (mouseCurrent.X > continueButton.Location.Left && mouseCurrent.X < continueButton.Location.Right && mouseCurrent.Y > continueButton.Location.Top && mouseCurrent.Y < continueButton.Location.Bottom) {
                         Exit();
                     }
                 }
            }
            if (player.IsDead()) {
                paused = true;

                continueButton.Location = new Rectangle(360, 374, 80, 35);
                resetButton.Location = new Rectangle(continueButton.Location.Right, 374, 80, 30);
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
                {// && !previous.IsKeyDown(Keys.Down)) {
                    player.DownKey();
                }

                /*
                wally.checkObject(player);
                wally2.checkObject(player);
                wally3.checkObject(player);
                wally4.checkObject(player);

                wally.checkObject(enemy);
                wally2.checkObject(enemy);
                wally3.checkObject(enemy);
                wally4.checkObject(enemy);
                */

                player.Update();

                map.Update();
                //enemy.CheckInRange(player);
                enemy.Update();
                goal.Update();
                
                /* if (enemy.Location.Y > 600)
                 {
                     enemy.Reset();
                 }*/


                if (player.Location.Intersects(goal.Location) && current.IsKeyDown(Keys.Down))
                {
                    win = true;
                   // player.Reset();
                    //enemy.Reset();
                }

                animationTimer++;
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

        private void BeginPaused(bool UserInitiated)
        {
            
            paused = true;
            pausedForGuide = !UserInitiated;
          

        }

        private void EndPause(bool letsplay)
        {
            if (letsplay == true)
            {
                paused = false;
                pausedForGuide = false;
            }
            
        }

        private void WinScreen(bool winthing)
        {
             if (winthing = true)
             {
              win = true;
             }
        }

        private void DeathScreen(bool death)
        { 
            
        }

        private void Reset() {
            //player.Reset();
            map.Reset();
            EndPause(true);
            win = false;
        }
    }
}
