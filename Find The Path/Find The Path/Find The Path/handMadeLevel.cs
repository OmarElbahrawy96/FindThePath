using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Find_The_Path
{
    class handMadeLevel : level
    {
        public handMadeLevel()
        {
        }
        public handMadeLevel(int num)
        {
            levelNumber = num;
            readLevel(num);
        }
        /// <summary>
        /// 
        /// 
        ///@@@@@@@@@@@@@ check if level num exists ;
        /// if ( levelnum exists ) 
        /// try and catch maybe ;;;;;;;;
        /// </summary>
        /// <param name="num"></param>
        public void readLevel(int num)
        {
            int[,] stars = new int[4, 4];
            string fileName = "star" + num.ToString() + ".txt"; 
            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs) ; 
            for (int i = 0; i < 4; i++)
            {
                string line = sr.ReadLine();
                string[] nums = line.Split(' ');
                for (int j = 0; j < 4; j++)
                {
                    stars[i, j] = int.Parse(nums[j]);
                }
            }
            fileName = "level" + num.ToString() + ".txt";
            fs = new FileStream(fileName, FileMode.Open);
            sr = new StreamReader(fs);
            int row = 0; 
            while (sr.Peek() != -1)
            {
                string line = sr.ReadLine();
                string[] nums = line.Split(' ');

                for (int col = 0; col < width; col++)
                {
                    levelMap[row, col] = refer[int.Parse(nums[col])];
                    string tmpLevelMap = "";
                    for (int i = 0; i < levelMap[row, col].Length; i++)
                        if ( (levelMap[row, col][i] >= 'a' && levelMap[row, col][i] <= 'z') || levelMap[row, col][i] == '_' )
                        tmpLevelMap += levelMap[row, col][i];
                    levelMap[row, col] = tmpLevelMap;
                    string[] cutName = levelMap[row, col].Split('_');
                    String suffix = cutName[cutName.Length-1];
                    if (suffix.Equals("n"))
                    {
                        bricks[row, col] = new normalBrick(levelMap[row, col], new Vector2(col, row), stars[row, col] == 1);
                    }
                    else if (suffix.Equals("st"))
                    {
                        bricks[row, col] = new startBrick(levelMap[row, col], new Vector2(col, row));
                    }
                    else if (suffix.Equals("en"))
                    {
                        bricks[row, col] = new endBrick(levelMap[row, col], new Vector2(col, row));
                    }
                    else
                    {
                        bricks[row, col] = new fixedBrick(levelMap[row, col], new Vector2(col, row), stars[row, col] == 1);
                    }
                }
                row++;
            }

            sr.Close();
            fs.Close();
        }
    }
}
