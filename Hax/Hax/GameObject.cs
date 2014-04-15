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
        private Vector2 offset; //a modifier for location based on the scrolling grid

        protected int animationTimer = 0;
        protected int animationSpeed = 25;

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

                sb.Draw(Image, RealLocation, null, Color.White, 0.0f, new Vector2(0, 0), se, 0.0f);
            }
        }

        //
        public virtual void Update() {
            offset = Map.scroll;
            //method stub
        }

        protected void Animate(Queue<Texture2D> sprites)
        {
            if (animationTimer > animationSpeed)
            {
                Image = sprites.Dequeue();
                sprites.Enqueue(Image);
                animationTimer = 0;
            }

            animationTimer++;
        }
    }
}
