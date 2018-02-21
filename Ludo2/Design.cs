using System;
using System.Threading;

namespace Ludo2
{
    static class Design //This class is used for the "frontend" code to make the game look nicer to the user
    {
        //Used to clear the console and write ludo on the top
        public static void Clear(int delay = 0, string header = "<--------------------- LUDO --------------------->")
        {
            Console.Clear(); //Clears the console
            Console.WriteLine(header); //Writes ludo to the console
            Console.WriteLine(); //makes a blank line
            Thread.Sleep(delay);
        }

        /// <summary>
        /// Output with the possibility of a delay
        /// </summary>
        public static void WriteLine(string text, int delay = 0)
        {
            Console.WriteLine(text);
            Thread.Sleep(delay); //Delays the next output
        }

        public static void Write(string text, int delay = 0)
        {
            Console.Write(text);
            Thread.Sleep(delay); //Delays the next output
        }

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
