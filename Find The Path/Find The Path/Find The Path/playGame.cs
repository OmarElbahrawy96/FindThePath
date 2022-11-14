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
    class playGame 
    {
        player plyer; // @@@
        level lvl;
        List<object> objects; 
        brick[,] bricks;
        Vector2 startPos;
        Vector2 endPos; 

        public playGame()
        { 
        }
        public void initialize(int level)
        {
            lvl = new handMadeLevel(level);
            bricks = lvl.getBricks();
            startPos = new Vector2();
            endPos = new Vector2();
            
            //edit
            plyer = new autoMove();
            setPositions();
            updateList();
        }
        private void setPositions()
        {
            foreach (entity ent in bricks)
            {
                if (ent.getName().Contains("_en") == true)
                {
                    endPos = ent.getPosition();
                }
                else if (ent.getName().Contains("_st") == true)
                {
                    startPos = ent.getPosition();
                }
            }
            plyer.setPosition(startPos);
                
            startPos.X = startPos.X / brick.brickWidth;
            startPos.Y = startPos.Y / brick.brickHeight;
            endPos.X = endPos.X / brick.brickWidth;
            endPos.Y = endPos.Y / brick.brickHeight;
            entity.swap(ref startPos.X, ref startPos.Y);
            entity.swap(ref endPos.X, ref endPos.Y);
 
        }

        private void updateList()
        {
            objects = new List<object>();
            foreach (object obj in bricks)
            {
                objects.Add(obj);
                brick br = (brick)obj;
                if (br.hasStar())
                {
                    objects.Add(br.getStar());
                }
            }
            objects.Add(plyer);
        }
        public List<object> getList()
        {
            return objects; 
        }
        bool isValidPos(Vector2 pos)
        {
            return !(pos.X < 0 || pos.X >= lvl.getHeight() || pos.Y < 0 || pos.Y >= lvl.getWidth());
        }
        public List<pair<brick, pair<string,string>>> findPath() //Brick , from , to
        {
            int height = lvl.getHeight();
            int width = lvl.getWidth();
            bool[,] vis = new bool[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    vis[i, j] = false;
                }
            }
            pair<Vector2, string> result = new pair<Vector2, string>(startPos, "st");
            List<pair<brick, pair<string, string>>> path = new List<pair<brick, pair<string, string>>>();

            while (true)
            {
                if (result == null)
                {
                    return null;
                }
                if ( isValidPos(result.first) == false )
                {
                    return null; 
                }
                if (vis[(int)result.first.X, (int)result.first.Y] == true)
                {
                    return null; 
                }
                vis[(int)result.first.X, (int)result.first.Y] = true;
                path.Add(new pair<brick,pair<string,string>>(bricks[(int)result.first.X,(int)result.first.Y],new pair<string,string>(result.second,getComplement(result.first,result.second))));

                if (result.first == endPos)
                {
                    return path;
                }
                result = goNext(result.first, result.second);
            }
        }
        string getOpposite(string direction)
        {
            if (direction == "left") return "right";
            else if (direction == "right") return "left";
            else if (direction == "down") return "up";
            else if (direction == "up") return "down";
            else return "";
        }
        string getComplement(Vector2 current , string direction)
        {
            string name = bricks[(int)current.X, (int)current.Y].getName();
            name = name.Split('\\')[1];
            string[] split = name.Split('_');
            if (split[0] == direction) return split[1];
            return split[0]; 
        }
        pair <Vector2, string> goNext(Vector2 current, string direction)
        {
            if ( isValidPos(current) == false)
            {
                return null;
            }
            string complement = getComplement(current, direction);
            string opposite = getOpposite(complement);
            if (complement == "left")
            {
                current.Y--; 
            }
            else if (complement == "right")
            {
                current.Y++;
            }
            else if (complement == "up")
            {
                current.X--;
            }
            else if (complement == "down")
            {
                current.X++;
            }
            else
            {
                return null;
            }
            if ( isValidPos(current) == false )
            {
                return null;
            }
            if (bricks[(int)current.X, (int)current.Y].getName().Contains(opposite) == false)
            {
                return null;
            }
            return new pair<Vector2, string>(current, opposite);
        }
        public Vector2 setPrevPos(int x, int y)
        {
            if (bricks[y, x] != null)
            {
                string brickName = bricks[y, x].getName();
                if (bricks[y, x].isMovable() && brickName.Contains("back") == false)
                {
                    bricks[y, x].changeSelectState();
                    bricks[y, x].changeStarSelectState();
                    return new Vector2(bricks[y, x].getPosition().X / brick.brickWidth, bricks[y, x].getPosition().Y / brick.brickHeight);
                }
            }
            return new Vector2(-1, -1);
        }
        public void brickMove(int x, int y, Vector2 prev)
        {
            bricks[(int)prev.Y, (int)prev.X].changeSelectState();
            bricks[(int)prev.Y, (int)prev.X].changeStarSelectState();
            if (Math.Abs(prev.X - x) + Math.Abs(prev.Y - y) != 1)
            {
                return; 
            }
            if (bricks[y, x] != null)
            {
                string brickName = bricks[y, x].getName();
                
                if (brickName.Contains("back_n") == true)
                {
                    bricks[y, x].move(new Vector2(prev.X * brick.brickWidth, prev.Y * brick.brickHeight));
                    bricks[y, x].updateStarPos();
                    bricks[(int)prev.Y, (int)prev.X].move(new Vector2(x * brick.brickWidth, y * brick.brickHeight));
                    bricks[(int)prev.Y, (int)prev.X].updateStarPos();
                    entity.swap(ref bricks[y, x], ref bricks[(int)prev.Y, (int)prev.X]);
                    bricks[y, x].playSoundEffect(); 
                    updateList();
                
                }
            }

        }
        public void playerMovement(List<pair<brick, pair<string, string>>> win)
        {
            int xPosition =(int) ((int)(plyer.getPosition().X  / brick.brickWidth) * brick.brickWidth);
            int yPosition = (int)((int)(plyer.getPosition().Y  / brick.brickHeight) * brick.brickHeight);

            foreach (pair<brick, pair<string, string>> item in win)
            {
                if (item.first.getPosition() == new Vector2(xPosition, yPosition))
                {
                    if (item.first.getName().Contains("en") == true)
                    {
                        Game1.gameState = "nextLevel";

                    }
                    plyer.move(item.second.first, item.second.second,item.first.getPosition());
                    break;
                }

            }
        }
        public int getLevelNumber()
        {
            return lvl.getLeveLNumber();
        }
        public void getHint()
        {
            brick[,] copyBricks = new brick[lvl.getHeight(), lvl.getWidth()];

            for (int i = 0; i < lvl.getHeight(); i++)
            {
                for (int j = 0; j < lvl.getWidth(); j++)
                {
                    brick b = bricks[i, j];
                    copyBricks[i, j] = new brick(bricks[i, j]);
                }
            }

            Dictionary<string, bool> permutations = new Dictionary<string, bool>();
            List<pair<int, int>> empty = new List<pair<int, int>>();
            for (int i = 0; i < lvl.getHeight(); i++)
            {
                for (int j = 0; j < lvl.getWidth(); j++)
                {
                    if (copyBricks[i, j].getName().Contains("back") == true)
                    {
                        empty.Add(new pair<int, int>(i, j));
                    }
                }
            }
            while (findPath() == null)
            {

            }
        }
    }
}
