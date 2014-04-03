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
        public static Texture2D wallImage;

        public static Texture2D playerStand;
        public static Texture2D playerWalk;
        public static Texture2D playerJump;

        public static Texture2D walkingMinion;
        public static Texture2D goal;
    }
}
