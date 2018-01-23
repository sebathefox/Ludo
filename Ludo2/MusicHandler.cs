using System;
using System.IO;

namespace Ludo2
{
    static class MusicHandler
    {
        //Soundtrack to play through the game
        public static void SoundTrack()
        {
            System.Media.SoundPlayer soundTrack = new System.Media.SoundPlayer(Directory.GetCurrentDirectory() + "/Music/SoundTrack.wav");
            soundTrack.PlayLooping();
        }

        //Sound to play when a token gets killed
        public static void DeathSound()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Directory.GetCurrentDirectory() + "/Music/Death.wav");
            player.Play();
        }
    }
}
