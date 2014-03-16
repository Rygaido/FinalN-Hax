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

//superclass of all objects that will appear on screen
//requires methods and attributes needed to draw object to screen
namespace Hax {
    class GameObject {

        private Rectangle location; //represent space occupied by object, should have X&Y coordinates and a length&width
        private Texture2D image; //the image drawn over object's rectangle
        private Vector2 offset; //a modifier for location based on the scrolling grid

        //accessor for rectangle which accounts for offset
        public Rectangle Location {
            get { return new Rectangle((int)(location.X+offset.X), (int)(location.Y+offset.Y), location.Width, location.Height); }
        }
        
        //draw image on location
        public virtual void Draw() {
            //method stub
        }

        //
        public virtual void Update() {
            //method stub
        }

        //overload to update that changes offset, then calls normal update
        public virtual void Update(Vector2 scroll) { 
            offset = scroll;
            Update();

            //new Rectangle(new Vector2());
        }
    }
}
