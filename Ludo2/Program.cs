using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Generates the background music
            //MusicGenerator();

            //Main Game Object
            Game Ludo = new Game();

            Console.Read();
        }

        //static void MusicGenerator()
        //{
        //    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Directory.GetCurrentDirectory() + "/Awesome.wav");
        //    player.Play();
        //}
    }

}
