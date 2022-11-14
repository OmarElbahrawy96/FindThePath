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
    class gui
    {
        private AudioManager audioManager;
        private SpriteBatch spriteBatch;
        private ContentManager content; 
        private List<object> objects;
        private List<pair<Rectangle, string>> rectangels;
        public gui(){}
        public gui(SpriteBatch spriteBatch ,ContentManager content, List<object> objects)
        {
            this.spriteBatch = spriteBatch;
            this.content = content; 
            this.objects = objects;
            rectangels = new List<pair<Rectangle,string>>();
            setAudio();
            loadContent();
            makeRectangles();

        }
        public void updateList(List<object> objects)
        {
            this.objects = objects;
        }
        public void loadContent()
        {
            foreach (entity ent in objects)
            {
                ent.setSoundEffect(content.Load<SoundEffect>("Sounds\\click")); 
                ent.setTexture(content.Load<Texture2D>(ent.getName()));
            }
            makeRectangles();
            audioManager.setSong(content.Load<Song>(audioManager.getSongPath()));
        }
        private void makeRectangles()
        {
            rectangels = new List<pair<Rectangle, string>>(); 
            foreach (object obj in objects)
            {
                if (obj.GetType().ToString().ToLower().Contains("button") == true)
                {
                    entity ent = (entity)obj ;
                    Rectangle rec = new Rectangle((int)ent.getPosition().X, (int)ent.getPosition().Y, (int)ent.getWidth(), (int)ent.getHeight());
                    rectangels.Add(new pair<Rectangle, string>(rec, ent.getName()));
                }
            }
        }
        public void draw()
        {
            foreach (object obj in objects)
            {
                if (obj.GetType().ToString().Contains("circle") == true)
                {
                    circle cir = (circle)obj ; 
                    spriteBatch.Draw(cir.getImage(),cir.getPosition(),null , cir.getColor(),cir.getRotate(), cir.getCenture(), 1f,SpriteEffects.None,0); 
                }
                else
                {
                    entity ent  = (entity)obj ;
                    spriteBatch.Draw(ent.getImage(), ent.getPosition(), ent.getColor());
                }
            }
        }
        public string click(int xPos , int yPos)
        {
            Rectangle mouseClickRect = new Rectangle(xPos, yPos, 1, 1);
            foreach (pair<Rectangle,string> rec in rectangels)
            {
                if (mouseClickRect.Intersects(rec.first) == true)
                {
                    return rec.second;
                }
            }
            return null;
        }
        public void setAudio()
        {
            if (Game1.gameState.Contains("playing") == true ) 
            {
                audioManager = new GamePlayBGS();
            }
            else if (Game1.gameState == "menu" || Game1.gameState == "choose" )
            {
                audioManager = new MenuBGS();
            }
            else if (Game1.gameState.Contains("loading") == true)
            {
                audioManager = new LoadingBGS();
            }
            else
            {
                audioManager = new LoadingBGS();
            
            }
        }
        public Song getAudio()
        {
            return audioManager.getSong();
        }
    }
}
