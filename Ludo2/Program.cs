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
            //MusicGenerator();

            //Main Game Object
            Game Ludo = new Game();

            Console.Read();
        }

        //Bonus
        static void MusicGenerator()
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Directory.GetCurrentDirectory() + "/Awesome.wav");
            player.Play();
        }
    }

}
