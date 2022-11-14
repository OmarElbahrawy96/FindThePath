using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Find_The_Path
{
    //Step 2 : Create a concrete class implementing the same interface 
    class autoMove:player
    {
        const float raduisUp = 75;
        const float raduisUpSquare = raduisUp * raduisUp;
        const float velosity = 1; 
        public autoMove()
        {
            this.name += "ball";
            width = 40;
            height = 40;
            this.movability = new movable();
        }
        public autoMove(Vector2 pos) : this()
        {
            pos.X += 55;
            pos.Y += 55;
            this.setPosition(pos);
        }
        public override void setPosition(Vector2 pos)
        {
            pos.X += 55;
            pos.Y += 55;
            this.position = pos;
        }

        public override void move(string from, string to , Vector2 brickPos)
        {
            if (from == "st")
            {
                if (to == "right")
                    position += new Vector2(velosity, 0);
                else if (to == "left")
                    position += new Vector2(-velosity, 0);
                else if (to == "down")
                    position += new Vector2(0, velosity);
                else
                    position += new Vector2(0, -velosity);
            }
            else if (to == "en")
            {
                if (from == "left")
                    position += new Vector2(velosity, 0);
                else if (from == "right")
                    position += new Vector2(-velosity, 0);
                else if (from == "down")
                    position += new Vector2(0, -velosity);
                else
                    position += new Vector2(0, velosity);
            }
            else if (from == "left")
            {
                float xPos = (position.X - brickPos.X) + 20 ;
                if (to == "right" )
                    position += new Vector2(velosity, 0);
                else if (to == "up")
                {
                    if (xPos >= raduisUp)
                    {
                        position += new Vector2(0, -velosity);
                    }
                    else
                    {
                        float yPos = (float)Math.Sqrt(raduisUpSquare - xPos * xPos);
                        position += new Vector2(velosity, brickPos.Y + yPos - 20 - position.Y);
                        position.X = Math.Max(position.X, 75);
                    }
                }
                else if (to == "down")
                {
                    if (xPos >= raduisUp)
                        position += new Vector2(0, velosity);
                    else
                    {
                        float yPos = (float)Math.Sqrt(raduisUpSquare - xPos * xPos);
                        position += new Vector2(velosity, brickPos.Y + brick.brickHeight - yPos - 20 - position.Y);
                    }
                }
            }
            else if (from == "right")
            {
                float xPos = brick.brickWidth - (position.X - brickPos.X + 20);
                if (to == "left" )
                    position += new Vector2(-velosity, 0);
                else if (to == "up")
                {
                    if (xPos >= raduisUp)
                    {
                        position += new Vector2(0, -velosity);
                    }
                    else
                    {
                        float yPos = (float)Math.Sqrt(raduisUpSquare - xPos * xPos);
                        position += new Vector2(-velosity, brickPos.Y + yPos - 20 - position.Y);
                    }
                }
                else if (to == "down")
                {
                    if (xPos >= raduisUp)
                        position += new Vector2(0, velosity);
                    else
                    {
                        float yPos = (float)Math.Sqrt(raduisUpSquare - xPos * xPos);
                        position += new Vector2(-velosity, brickPos.Y + brick.brickHeight - yPos - 20 - position.Y);
                    }
                }
            }
            else if (from == "down")
            {
                float xPos = position.X - brickPos.X + 20;
                if (to == "up" )
                    position += new Vector2(0, -velosity);
                else if (to == "left")
                {
                    if (xPos >= raduisUp)
                    {
                        position += new Vector2(-velosity, 0);
                    }
                    else
                    {
                        float yPos = (float)Math.Sqrt(raduisUpSquare - xPos * xPos);
                        float newY = brickPos.Y + brick.brickHeight - yPos - 20 ;
                        position += new Vector2(-velosity, brickPos.Y + brick.brickHeight - yPos - 20 - position.Y);
                    }
                }
                else if (to == "right")
                {
                    if (xPos >= raduisUp)
                        position += new Vector2(velosity, 0);
                    else
                    {
                        float yPos = (float)Math.Sqrt(raduisUpSquare - xPos * xPos);
                        position += new Vector2(velosity, brickPos.Y + brick.brickHeight - yPos - 20 - position.Y);
                    }
                }
            }
            else if (from == "up")
            {
                if (to == "down")
                    position += new Vector2(0, velosity);
                else if (to == "left")
                {
                    float xPos = position.X - brickPos.X + 20;
                    if (xPos >= raduisUp)
                    {
                        position += new Vector2(-velosity, 0);
                    }
                    else
                    {
                        float yPos = (float)Math.Sqrt(raduisUpSquare - xPos * xPos);
                        float newY = brickPos.Y + yPos - 20 ;
                        if (newY < brickPos.Y)
                            newY += 20; 
                        position += new Vector2(-velosity, newY - position.Y);
                    }
                }
                else if (to == "right")
                {
                    float xPos = brick.brickWidth - (position.X - brickPos.X + 20); // problem lvl3 , right lvl2 !!
                    if (xPos >= raduisUp)
                        position += new Vector2(velosity, velosity);
                    else
                    {
                        float yPos = (float)Math.Sqrt(raduisUpSquare - xPos * xPos);
                        float newY = brickPos.Y + yPos - 20;
                        if (newY < brickPos.Y)
                            newY += 20;
                        position += new Vector2(velosity, brickPos.Y + yPos - 20 - position.Y);
                    }
                }
            }
        }
    }
}
 