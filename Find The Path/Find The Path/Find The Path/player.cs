using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Find_The_Path
{
    //Step 1 : Create an Interface.
    abstract class player:entity
    {
        abstract public void move(string from, string to, Vector2 brickPos);
    }
}
 