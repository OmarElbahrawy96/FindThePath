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
    class nonMovable:moveState
    {
        public nonMovable() { }
        public override bool isMovable()
        {
            return false; 
        }
        public override void move(ref Vector2 initial, Vector2 destination)
        {
            return; 
        }
    }
}
