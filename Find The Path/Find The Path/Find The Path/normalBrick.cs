﻿using System;
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
    class normalBrick:brick
    {
        public normalBrick()
        {
            movability = new movable(); 
        }
        public normalBrick(string textureName, Vector2 pos , bool hasStar) : base ( textureName , pos , hasStar)
        {
            movability = new movable(); 
        }
    }
}
