using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Find_The_Path
{
    class brick:entity
    {
        static readonly public float brickHeight = 150f;
        static readonly public float brickWidth = 150f;
        Star star; 
        public brick()
        {
            this.height = brickHeight; 
            this.width = brickWidth;
            star = null; 
            movability = new nonMovable(); 
        }
        public brick(string textureName, Vector2 pos , bool hasStar) : this() 
        {
            this.name = this.name + textureName;
            this.position = new Vector2(pos.X * width, pos.Y * height);
            if (hasStar)
            {
                star = new Star(new Vector2(this.position.X + brickWidth / 2 - Star.width / 2, this.position.Y + brickHeight / 2 - Star.height / 2));
            }
        }
        public brick(brick br)
        {
            this.name = br.getName();
            this.color = br.getColor();
            this.height = br.getHeight();
            this.image = br.getImage();
            this.movability = br.getMovability();
            this.position = br.getPosition();
            this.select = br.getSelect();
            this.star = br.getStar();
            this.width = br.getWidth(); 
        }
        public void updateStarPos()
        {
            if (hasStar())
            {
                this.star.setPosition(new Vector2(this.position.X + brickWidth / 2 - Star.width / 2, this.position.Y + brickHeight / 2 - Star.height / 2));
            }
        }
        public bool hasStar()
        {
            return star != null;
        }
        public Star getStar()
        {
            if (hasStar())
            {
                return star;
            }
            return null; 
        }
        public bool isMovable() 
        {
            return movability.isMovable(); 
        }
        public void changeStarSelectState()
        {
            if (hasStar())
            {
                star.changeSelectState();
            }
        }
    }
}
