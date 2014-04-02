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

        KeyboardState previous; //previous state of keyboard

        public Game1()
            : base() {
            graphics = new GraphicsDeviceManager(this);
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

            map = new Map();
            //spawn a player and a wall here for testing purposes
            player = new Player();
            player.Location = new Rectangle(50, 250, 100, 100);
            wally = new Wall();
            wally.Location = new Rectangle(50, 400, 100, 100);
            wally2 = new Wall();
            wally2.Location = new Rectangle(300, 400, 100, 100);
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
            ImageBank.playerStand = Content.Load<Texture2D>("mario_stand");
            ImageBank.playerWalk = Content.Load<Texture2D>("mario_walk");
            ImageBank.playerJump = Content.Load<Texture2D>("mario_jump");
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

            // TODO: Add your update logic here

            //get current state of keyboard
            KeyboardState current = Keyboard.GetState();

            //check if a key is currently pressed, but wasn't before, call appropriate method on player
            if (current.IsKeyDown(Keys.Left)){// && !previous.IsKeyDown(Keys.Left)) {
                player.LeftKey(); //left key can be held
            }
            if (current.IsKeyDown(Keys.Right)) {// && !previous.IsKeyDown(Keys.Right)) {
                player.RightKey(); //right key can be held
            }
            if (current.IsKeyDown(Keys.Up) && !previous.IsKeyDown(Keys.Up)) {
                player.UpKey();
            }
            if (current.IsKeyDown(Keys.Down)) {// && !previous.IsKeyDown(Keys.Down)) {
                player.DownKey();
            }

            //store previous state of keyboard
            previous = current;

            player.Update();
            map.Update();
            wally.checkObject(player);
            wally2.checkObject(player);

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
            player.Draw(spriteBatch);
            wally.Draw(spriteBatch);
            wally2.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
