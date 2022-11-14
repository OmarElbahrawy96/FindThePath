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
    class level
    {
        protected static int height = 4;
        protected static int width = 4;
        protected brick[,] bricks;
        protected string[,] levelMap; // to be checked 
        protected List<string> refer;
        protected int levelNumber;
        public level()
        {
            bricks = new brick[height, width];
            levelMap = new string[height, width];
            refer = new List<string>();
            refer.Add("EMPTY");
            set_preferces();
        }
        void set_preferces()
        {
            FileStream fs = new FileStream("Reference.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string file = sr.ReadToEnd();
            string[] fileLines = file.Split('\n');

            for (int i = 0; i < fileLines.Length; i++)
            {
                string[] split = fileLines[i].Split('-');
                if ( split.Length > 1 ) 
                    refer.Add(split[1]);
            }
            sr.Close();
            fs.Close();
        }
        public brick[,] getBricks()
        {
            return bricks;
        }
        public int getWidth()
        {
            return width;
        }
        public int getHeight()
        {
            return height;
        }
        public int getLeveLNumber()
        {
            return levelNumber;
        }
        
    }
}
