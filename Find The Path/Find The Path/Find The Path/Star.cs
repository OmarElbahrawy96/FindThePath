using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Find_The_Path
{
    class Star : entity 
    {
        public static readonly float width = 45;
        public static readonly float height = 45;
        public Star() {
            this.name += "star";
            base.height = height;
            base.width = width; 
        }
        public Star(Vector2 pos) : this() 
        {
            this.position = pos; 
        }
        
    }
}
