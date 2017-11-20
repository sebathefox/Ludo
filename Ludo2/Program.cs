using System;
using System.IO;
using System.Linq;

namespace Ludo2
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args != null) //Will only check the args variable if it has any data
            {
                foreach(string ar in args) //Will read the commandline arguments if any
                {
                   if(ar == "-h" || ar == "-H" || ar == "--help")
                    {
                        Help();
                        System.Diagnostics.Process.GetCurrentProcess().Kill(); //Kills this process so that the user can read the help documentation
                    }
                    else if (ar == "-m" || ar == "-M")
                    {
                        MusicGenerator();
                        continue;
                    }
                }
            }
            //Main Game Object
            Game Ludo = new Game(); //The only line of code we need for the game to work

            Console.Read();
        }

        //Makes some awesome background music
        static void MusicGenerator()
        {
            //Makes a new instance of the SoundPlayer class
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Directory.GetCurrentDirectory() + "/Music/Awesome.wav");
            player.Play();
        }

        //Shows the possible commandline arguments for this program
        static void Help()
        {
            Console.WriteLine("\t----- Ludo2 -----\n\n");
            Console.WriteLine("# Ludo2 [ARGUMENTS]\n\n");
            Console.WriteLine("# [ARGUMENTS]\n");
            Console.WriteLine("# -m or -M  -  Plays the game with music");
            Console.WriteLine("# -h, -H or --help  -  Shows this help");
        }
    }
}
