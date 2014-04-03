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
    //the place where the player is trying to reach
    class Goal :GameObject{
        //probably doesn't need to do anything special
        //just call map's Clear and Load methods after player collides with this

        //TODO constructor should have next level 

        public Goal()
        {
            Image = ImageBank.goal;
            Location = new Rectangle(750, 324, Image.Width, Image.Height);
        }


    }



    //the place where player starts
    class Spawn : GameObject {
        //propably doesn't need to do anything, well just move player's X and Y to match after loading a map
    }
}
