using System;
using System.IO;

namespace Ludo2
{
    static class MusicHandler
    {
        //Sound to play when a token gets killed
        public static void DeathSound()
        {
            //Creates a new audio player
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Directory.GetCurrentDirectory() + "/Music/Death.wav");
            player.Play(); //starts the audio
        }
    }
}
