using System;
using System.Threading;

namespace Ludo2
{
    static class Design //This class is used for the "frontend" code to make the game look nicer to the user
    {
        //Used to clear the console and write ludo on the top
        public static void Clear(string header = "<--- LUDO --->")
        {
            Console.Clear(); //Clears the console
            Console.WriteLine(header); //Writes ludo to the console
            Console.WriteLine(); //makes a blank line
        }

        /// <summary>
        /// Output with the possibility of a delay
        /// </summary>
        public static void WriteLine(string text, int delay = 0)
        {
            Console.WriteLine(text);
            Thread.Sleep(delay); //Delays the next output
        }
    }
}
