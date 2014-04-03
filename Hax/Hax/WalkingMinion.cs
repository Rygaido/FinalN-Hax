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

namespace Hax
{
    class WalkingMinion:Enemy
    {
        bool isInRange;

        public WalkingMinion(){
            Image = ImageBank.walkingMinion;

        }

        public override void Update()
        {
            base.Update();

            if (isInRange == true)
            {
                xSpeed = -15;
            }
            else
            {
                xSpeed = 0;
            }
        }

        public void CheckInRange(Player p)
        {
            if (Math.Abs(p.Location.X - Location.X) <= Math.Abs(200))
            {
                isInRange = true;
            }
        }

        public override void Reset()
        {

            base.Reset();

            isInRange = false;
        }

    }
}
