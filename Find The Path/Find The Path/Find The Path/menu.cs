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
    class menu
    {
        /// <summary>
        /// identefy the position of selected brick and it's distenation
        /// </summary>
        private Vector2 _prevPos;
        private int _clickNumber = 0;
        List<pair<brick, pair<string, string>>> win;

        private List<object> objects;
        private entity backGroundMenu;
        private entity loadingPicture;
        private entity chooseLevelPicture;
        private List<chooseLevelButton> chooseLevelButtons; 
        private circle loadingCircle;
        private playButton playBtn;
        private exitButton exitBtn;
        private creditsButton creditsBtn;
        private settingsButton settingsBtn;
        private gui guiFunction;
        private MouseState mouseState;
        private MouseState previousMouseState;
        private playGame game; 
        private const int levelCount = 5 ; 
        public menu() { }
        public menu(SpriteBatch spriteBatch , ContentManager content )
        {
            backGroundMenu = new entity("backGroundMenu", new Vector2(0,0), 990f, 150*4f);
            loadingCircle = new circle("loadingCircle", new Vector2(850, 290), 166f, 166f);
            loadingPicture = new entity("loadingPicture", new Vector2(0, 0), 990f, 150 * 4f);
            chooseLevelPicture = new entity("chooseLevelPicture", new Vector2(0, 0), 990f, 150 * 4f);
            playBtn = new playButton("play", new Vector2(280, 160), 300f, 100f);
            exitBtn = new exitButton("exit", new Vector2(280, 290), 300f, 100f);
            creditsBtn = new creditsButton();
            settingsBtn = new settingsButton();
            chooseLevelButtons = new List<chooseLevelButton>(); 
            objects = new List<object>();
            updateListMenu();
            guiFunction = new gui(spriteBatch, content , objects);
            mouseState = Mouse.GetState();
            game = new playGame();
            MediaPlayer.Volume = 0.1f;
        }
        private void changeAudio()
        {
            MediaPlayer.Stop();
            guiFunction.setAudio();
        }
        private void reinitialize()
        {
            objects = game.getList();
            guiFunction.updateList(objects);
            guiFunction.loadContent();
        }
        private void updateListMenu()
        {
            objects = new List<object>();
            objects.Add(backGroundMenu);
            objects.Add(playBtn);
            objects.Add(exitBtn);
        }
        private void updateListLoading()
        {
            objects = new List<object>();
            objects.Add(loadingPicture);
            objects.Add(loadingCircle);
        }
        private void updateChooseLevel()
        {
            objects = new List<object>();
            objects.Add(chooseLevelPicture);
            objects.Add(new chooseLevelButton("level1",new Vector2(100 , 200 ) , 214f,138f));
            objects.Add(new chooseLevelButton("level2",new Vector2(400 , 200 ) , 214f,138f));
            objects.Add(new chooseLevelButton("level3", new Vector2(700, 200), 214f, 138f));
            objects.Add(new chooseLevelButton("level4", new Vector2(100, 400), 214f, 138f));
            objects.Add(new chooseLevelButton("level5", new Vector2(400, 400), 214f, 138f));
            objects.Add(new chooseLevelButton("levelR", new Vector2(700, 400), 214f, 138f));
            
        }
        public void draw()
        {
            guiFunction.draw();   
        }
        public void Update()
        {
            previousMouseState = mouseState;
            mouseState = Mouse.GetState();
            if (MediaPlayer.State != MediaState.Playing)
            {
                if (Game1.gameState.Contains("loading") == true)
                {
                    string levelNum = Game1.gameState ; 
                    levelNum = levelNum.Remove(0,7);
                    Game1.gameState = "playing" + levelNum;
                    levelNum = levelNum.Remove(0, 5);
                    changeAudio();
                    if (levelNum != "R")
                    {
                        game.initialize(int.Parse(levelNum));
                        reinitialize();
                    }
                }

                MediaPlayer.Play(guiFunction.getAudio());
            }

            if (Game1.gameState.Contains("playing") == true )
            {

                objects = game.getList();
                win = game.findPath();
                if (win != null) // @@ 
                {
                    Game1.gameState = "win";
                }
            }
            else if (Game1.gameState.Contains("loading") == true ) 
            {
                loadingCircle.increaseRotate();
                updateListLoading();
                guiFunction.updateList(objects);
            }
            else if (Game1.gameState == "menu")
            {
                updateListMenu();
            }
            else if (Game1.gameState == "win")
            {
                game.playerMovement(win);
            }
            else if (Game1.gameState == "nextLevel")
            {
                if (game.getLevelNumber() + 1 > 5)
                {
                    Game1.gameState = "menu";
                    updateListMenu();
                    guiFunction.updateList(objects);
                    guiFunction.loadContent();
                }
                else
                {
                    game.initialize(game.getLevelNumber() + 1);
                    reinitialize();
                    Game1.gameState = "playinglevel" + (game.getLevelNumber()+1).ToString(); // wronnnng 
                }
            }
            guiFunction.updateList(objects);
            
            if (previousMouseState.LeftButton == ButtonState.Pressed &&
                mouseState.LeftButton == ButtonState.Released)
            {
                if (Game1.gameState.Contains("playing") == true ) 
                {

                    //Selecting and moving brack code
                    int xPosition = (mouseState.X / (int)brick.brickWidth);
                    int yPosition = (mouseState.Y / (int)brick.brickHeight);

                    if (xPosition >= 0 && xPosition < 4 && yPosition >= 0 && yPosition < 4)
                    {
                        if (_clickNumber == 0)
                        {
                            _prevPos = game.setPrevPos(xPosition, yPosition);
                                                        
                            if (_prevPos.X != -1 && _prevPos.Y != -1)
                                _clickNumber++;
                        }
                        else if (_clickNumber > 0)
                        {
                            _clickNumber = 0;

                            game.brickMove(xPosition, yPosition, _prevPos);
                        }
                    }
                }

                string respone = guiFunction.click(mouseState.X, mouseState.Y);
                if (respone != null)
                {
                    foreach (object obj in objects)
                    {
                        if ( obj.GetType().ToString().ToLower().Contains("button") == true )
                        {
                            button b = (button)obj ; 
                            if (b.getName() == respone)
                            {
                                b.work();
                                changeAudio();
                                
                                if (Game1.gameState == "choose")
                                {
                                    updateChooseLevel();
                                    guiFunction.updateList(objects);
                                    guiFunction.loadContent();
                                        
                                }
                                else if (Game1.gameState.Contains("loading") == true ) 
                                {
                                    updateListLoading();
                                    guiFunction.updateList(objects);
                                    guiFunction.loadContent();
                                    MediaPlayer.Play(guiFunction.getAudio());
                                }
                            }
                        }
                        
                    }
                }

                
            }

        }
        
    }
}
