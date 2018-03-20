using System;
using System.Threading;

namespace Ludo2
{
    static class Design //This class is used for the "frontend" code to make the game look nicer to the user
    {
        /// <summary>
        /// Makes a nice header
        /// </summary>
        /// <param name="delay">Delay in milliseconds</param>
        /// <param name="header">data to write in string</param>
        public static void Clear(int delay = 0, string header = "<--------------------- LUDO --------------------->")
        {
            Console.Clear(); //Clears the console
            Console.WriteLine(header); //Writes ludo to the console
            Console.WriteLine(); //makes a blank line
            Thread.Sleep(delay);
        }

        /// <summary>
        /// Console.WriteLine but with the possibility of a delay
        /// </summary>
        /// <param name="text">Data to write</param>
        /// <param name="delay">Delay in milliseconds</param>
        public static void WriteLine(string text, int delay = 0)
        {
            Console.WriteLine(text);
            Thread.Sleep(delay); //Delays the next output
        }

        /// <summary>
        /// Console.Write but with the possibility of a delay
        /// </summary>
        /// <param name="text">Data to write</param>
        /// <param name="delay">Delay in milliseconds</param>
        public static void Write(string text, int delay = 0)
        {
            Console.Write(text);
            Thread.Sleep(delay); //Delays the next output
        }

        /// <summary>
        /// Slowly prints text to the screen
        /// </summary>
        /// <param name="input">Data to write</param>
        /// <param name="delay">Delay in milliseconds</param>
        public static void SlowPrint(string input, int delay)
        {
            input.ToCharArray();

            foreach (char cha in input)
            {
                Write(cha.ToString());
                Thread.Sleep(delay);
            }
        }
    }
}
