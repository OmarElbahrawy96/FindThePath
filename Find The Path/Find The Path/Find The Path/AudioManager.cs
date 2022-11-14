using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Find_The_Path
{
    class AudioManager
    {
        public AudioManager()
        { }
        protected string AudioPath;
        protected Song audio;

        //protected virtual void LoadSong();

        public Song getSong()
        {
            return audio;
        }
        public string getSongPath()
        {
            return AudioPath;
        }

        public void setSong(Song song)
        {
            audio = song;
        }
    }
}
