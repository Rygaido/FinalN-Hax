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
using System.Threading;

//takes player input string
namespace Hax {
    class InputWindow:GameObject {
        private string input="";

        private Player player;
        private bool konami = false;
        public bool Konami
        {
            get { return konami; }
        }

        public InputWindow(Player p) {
            player = p;
            Image = ImageBank.square;
            col = Color.Black;

            Location = new Rectangle(50, 200, 700, 50);
        }

        public override void Draw(SpriteBatch sb) {

            sb.Draw(Image, new Rectangle(Location.X - 5, Location.Y - 5, Location.Width + 10, Location.Height + 10), Color.Red);
            base.Draw(sb);

            
            sb.DrawString(ImageBank.font,input,new Vector2(Location.X, Location.Y),Color.White,0.0f,new Vector2(0,0),2,SpriteEffects.None,0.0f);
        }

        //read chars from keyboard into a string
        public void ReadKeyboard(KeyboardState current, KeyboardState previous) {
            bool shift = false;//check if shift key is down
            if (current.IsKeyDown(Keys.LeftShift) || current.IsKeyDown(Keys.RightShift)) {
                shift = true;
            }

            foreach (Keys k in current.GetPressedKeys()) { //loop through all pressed keys
                if (!previous.IsKeyDown(k)) { //only proceed if key was not down on previous frame

                    string keys = k.ToString(); //convert key to a string

                    if (keys.Length == 1) { //a key string with a lenght of 1 must be a letter or number

                        if (Char.IsLetterOrDigit(keys[0])) { //if key pressed was a letter or digit
                            //add key to string

                            if (!shift) {
                                input += keys[0].ToString().ToLower();
                            }
                            else { //add uppercase version if shift is held
                                input += keys[0].ToString().ToUpper();
                            }
                        }

                    }
                    //special case: backspace key deletes last char
                    if (k.Equals(Keys.Back) && input.Length > 0) {
                        input = input.Substring(0, input.Length - 1);
                    }
                    //special case, Underscore (shift-minus) is a permitted non-numeric/decimal char
                    if (k.Equals(Keys.OemMinus) && shift) {
                        input += "_";
                    }
                }
            }
        }

        public void ProcessCheat() {
            input = input.ToUpper();
            if (input == "FISTFULOFPIXELS") {
                player.ActivateAttack();
                //player.Col = Color.Red;
            }
            else if (input == "RIPCOULSON") {
                player.ActivateDefense();
                //player.Col = Color.Green;
            }
            else if (input == "SIXTWOTHREE") {
                player.DeActivateCheats();
                player.Col = Color.White;
                player.Location = new Rectangle(player.Location.X, player.Location.Bottom-87, 57, 87);
                player.JumpSpeed = 20;
                player.RunSpeed = 5;
                konami = false;
            }
                //color coded cheatcodes, originally for debugging, now for lulz
            else if (input == "ANEWSTART") { //blue mode cheat
                player.Col = Color.Blue;
            }
            else if (input == "THECHIN") //red mode cheat
            {
                player.Col = Color.Crimson;
            }
            else if (input == "CAPTAINCANADA") //invisible mode cheat
            {
                player.Col = Color.Transparent;
            }
            else if (input == "LIGHTISHRED") //pinkmode cheat
            {
                player.Col = Color.HotPink;
            }
            else if (input == "THISPARTYSOVER")
            { //purple mode cheat
                player.Col = new Color(200, 0, 255);
            }
            else if (input == "DRINKME") //small mode cheat
            {
                player.Location = new Rectangle(player.Location.X, player.Location.Y, player.Location.Width / 2, player.Location.Height / 2);
            }
            else if (input == "ROADRUNNER") //fastmode cheat
            {
                player.RunSpeed *= 4;
            }
            else if (input == "MOONSHOES") //high jump cheat
            {
                player.JumpSpeed *= 2;
            }
            else if (input == "UPUPDOWNDOWNLEFTRIGHTLEFTRIGHTBA") //high jump cheat
            {
                konami = !konami;
            }
            else//unrecognized cheat code
            {
                player.FailedCheat();
            }

            input = "";//reset input everytime
        }
    }
}
