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
        bool hitWall;
        

        public WalkingMinion(Player p)
           : base(p){
            Image = ImageBank.walkingMinion;
            hitWall = false;
            isInRange = false;
        }

        public override void Update()
        {
            if (Active == true)
            {
                base.Update();

                CollideWithPlayer();

                if (isInRange == true && xSpeed == 0)
                {
                    hitWall = !hitWall;
                }

                //////
                if (isInRange == false)
                {
                    CheckInRange();
                }

                if (isInRange == true)
                {
                    if (hitWall == false)
                    {
                        xSpeed = -10;
                    }
                    else
                    {
                        xSpeed = 10;
                    }
                }
                else
                {
                    xSpeed = 0;
                }
            }
            else
            {
                xSpeed = 0;
                ySpeed+=Movable.gravity;
                Move();
            }
           
        }

        public void CheckInRange()
        {
            if (Math.Abs(player.Location.X - Location.X) <= Math.Abs(200))
            {
                isInRange = true;
            }
        }

        public override void Reset()
        {

            base.Reset();

            isInRange = false;
            hitWall = false;
        }

        public override void Draw(SpriteBatch sb)
        {
            if (active == true)
            {
                base.Draw(sb);
            }
            else
            {
                SpriteEffects se = SpriteEffects.FlipVertically;

                sb.Draw(Image, RealLocation, null, Color.White, 0.0f, new Vector2(0, 0), se, 0.0f);
            }
        }

        public override void CollideWithPlayer()
        {
            
            if (Location.Intersects(player.Location)){// && mov.Previous.Right < Location.Left && mov.Previous.Bottom > Location.Top){
                    if (player.Previous.Bottom < Location.Center.Y)
                    {
                        player.ySpeed =-15;
                        Active = false;
                    }
                    else
                    {
                        base.CollideWithPlayer();
                    }
            }
        }

    }
}
