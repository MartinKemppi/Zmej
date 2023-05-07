using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.ComponentModel;
using System.Numerics;
using WMPLib;

namespace Zmej
{
    public class Sounds
    {

        WindowsMediaPlayer player = new WindowsMediaPlayer();
        private string pathToMedia;

        public Sounds(string pathToResources)
        {
            pathToMedia = pathToResources;
        }

        public void Play()
        {
            player.URL = pathToMedia + "Snake Music.mp3";
            player.settings.volume = 30;
            player.controls.play();
            player.settings.setMode("loop", true);
        }

        public void Play(string songName)
        {
            player.URL = pathToMedia + songName + ".mp3";
            player.controls.play();
        }

        public void PlayEat()
        {
            player.URL = pathToMedia + "Eat.wav";
            player.settings.volume = 30;
            player.controls.play();
        }

        public void PlayGameOver()
        {
            player.URL = pathToMedia + "gameover.mp3";
            player.controls.play();
        }
    }

}