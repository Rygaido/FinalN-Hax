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


//takes player input string
namespace Hax {
    class InputWindow:GameObject {
        string input;

        //read chars from keyboard into a string
        public void ReadKeyboard(KeyboardState current, KeyboardState previous) {
            bool shift = false;//check if shift key is down
            if (current.IsKeyDown(Keys.LeftShift)) {
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
    }
}
