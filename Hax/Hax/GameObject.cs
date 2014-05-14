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

//superclass of all objects that will appear on screen
//requires methods and attributes needed to draw object to screen
namespace Hax {
    class GameObject {

        private Rectangle location; //represent space occupied by object, should have X&Y coordinates and a length&width
        private Texture2D image; //the image drawn over object's rectangle
        protected Vector2 offset; //a modifier for location based on the scrolling grid
        public Vector2 Offset {
            get { return offset; }
            set { offset = value; }
        }

        //timer used for animation, speed at which animation frames change, and current frame visable
        protected int animationTimer = 0;
        protected int animationSpeed = 25;
        protected int animationFrame = 0;
        protected bool animationEnd = false;

        protected Color col=Color.White; //protected variable controls color of draw method for debug purposes
        public Color Col { //mutator property
            set { col = value; }
        }
        //is object facing left? should image be mirrored
        protected bool faceLeft;

        //property for rectangle location, used in move method
        public Rectangle Location {
            get { return location; }
            set { location = value; }
        }
        //alternate accessor for rectangle which accounts for offset //used in draw method
        public Rectangle RealLocation {
            get { return new Rectangle((int)(location.X+offset.X), (int)(location.Y+offset.Y), location.Width, location.Height); }
        }

        //get and set property fo image
        public Texture2D Image {
            get { return image; }
            set { image = value; }
        }

        public GameObject() { //default constructor
            Image = ImageBank.defaultImage;
           
            location.Width = Image.Width;
            location.Height = Image.Height;
        }
        
        //draw image on location
        public virtual void Draw(SpriteBatch sb) {

            if (Image != null) {
                SpriteEffects se = SpriteEffects.None; //apply effect to flip image if faceLeft is true
                if (faceLeft) { se = SpriteEffects.FlipHorizontally; }
                //SpriteEffects.
                sb.Draw(Image, RealLocation, null, col, 0.0f, new Vector2(0, 0), se, 0.0f);
            }
        }

        //
        public virtual void Update() {
            offset = Map.scroll;
            //method stub
        }

        protected void Animate(List<Texture2D> s) //animates the object using a list of images for frames
        {
            animationEnd = false; //bool used to track if animation has just ended or not

            if (animationTimer > animationSpeed) //when timer reaches speed
            {
                animationFrame++; //increment frame
                if (animationFrame >= s.Count)//then if frames have exceeded sprites in list
                {
                    animationFrame = 0; //reset frame
                    animationEnd = true; //Animation cycle has ended
                }
                //update image to new frame
                Image = s[animationFrame];
                //reset timer
                animationTimer = 0;
            }
            //until then, increment timer
            animationTimer++;
        }
        //helper method//resets animation cycle
        protected void ResetAnimation() {
            animationFrame = 0;
            animationTimer = animationSpeed;
        }
    }
}
