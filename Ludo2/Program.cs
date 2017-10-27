using System;
using System.IO;
using System.Linq;

namespace Ludo2
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args != null)
            {
                foreach(string ar in args)
                {
                    if (ar == "-m" || ar == "-M")
                    {
                        MusicGenerator();
                    }
                }
            }
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
