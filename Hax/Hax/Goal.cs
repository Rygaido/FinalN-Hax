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
}
