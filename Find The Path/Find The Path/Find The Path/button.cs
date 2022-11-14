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

    //Step 1 : Create an Interface.
    abstract class button:entity
    {
        public button() { }
        public button(string textureName,  Vector2 pos, float width, float height)
            : base(textureName, pos , width , height)
        {
        }
        abstract public void work();
    }
}
