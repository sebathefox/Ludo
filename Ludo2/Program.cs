using System;
using System.IO;
using System.Linq;

namespace Ludo2
{
    class Program
    {
        static void Main(string[] args)
        {
            //MusicGenerator(); //Uncomment for background music through the game;)

            //Main Game Object
            Game Ludo = new Game();

            Console.Read();
        }

        //Makes some awesome background music
        static void MusicGenerator()
        {
            //Makes a new instance of the SoundPlayer class
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Directory.GetCurrentDirectory() + "/Awesome.wav");
            player.Play();
        }
    }

}
