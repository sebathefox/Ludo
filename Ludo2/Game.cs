using System;
using System.Linq;

namespace Ludo2
{
    //The different colors the users can have
    public enum GameColor { Yellow, Blue, Red, Green};

    public class Game
    {
        
        private GameColor color;

        //Token initilization
        private int numberOfTokens = 4;
        //private int tokenId;
        private int playerId;
        private Token[] tokens;

        //Player initialization
        private int numberOfPlayers;
        private Player[] players;

        //Misc
        Dice die = new Dice();
        private string inpt;

        //Git Test

        //---------------- Constructor ----------------
        public Game()
        {
            MainMenu();

            SetNumberOfPlayers();
            CreatePlayers();
            ShowPlayers();

            //Begins Token stuff...

            CreateTokens();
            ShowTokens();


            //Now The Game Begins

            Console.WriteLine("The users now hit the die to see who starts");
            Console.WriteLine("Use (r)oll to throw the die");
            inpt = Console.ReadLine();
        }

        /*
         * ---------------- Beginning of Front End ----------------
         */

        //Make MainMenu
        private void MainMenu()
        {
            Console.WriteLine("------------------------------ Velkommen til ludo ------------------------------"); //Screen is 80 characters wide

            //TODO Need Design Optimization
            
        }


        
        /*
         * ---------------- End of Front End ----------------
         */

        //The user can choose to play a game with 2 to 4 users
        private void SetNumberOfPlayers()
        {
            Console.Write("Hvor mange spillere?: ");

            while(this.numberOfPlayers < 2 || this.numberOfPlayers > 4)
            {
                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out this.numberOfPlayers))
                {
                    Console.WriteLine();
                    Console.WriteLine("Ugyldig værdi, vælg et tal mellem 2 og 4");
                }
            }
        }

        //This actually makes the players and inserts them into an object array
        private void CreatePlayers()
        {
            this.players = new Player[this.numberOfPlayers];

            Console.WriteLine();
            for (int i = 0; i < this.numberOfPlayers; i++)
            {
                
                Console.Write("Hvad hedder spiller {0}: ", (i+1));
                string name = Console.ReadLine();

                switch (i)
                {
                    case (0):
                        this.color = GameColor.Yellow;
                        break;
                    case (1):
                        this.color = GameColor.Blue;
                        break;
                    case (2):
                        this.color = GameColor.Red;
                        break;
                    case (3):
                        this.color = GameColor.Green;
                        break;
                }

                players[i] = new Player(name, i + 1, color);
                this.playerId = i + 1;
                CreateTokens();
                ShowTokens();
            }
            
        }

        private void CreateTokens()
        {

            this.tokens = new Token[this.numberOfTokens];

            Console.WriteLine();
            for (int i = 0; i < this.numberOfTokens; i++)
            {
                tokens[i] = new Token(this.color, i + 1, playerId);
            }
        }

        private void ShowPlayers()
        {
            Console.WriteLine();
            Console.WriteLine("Okay, her er dine spillere:");
            foreach(Player pl in this.players)
            {
                Console.WriteLine("#" + pl.GetName());
                Console.WriteLine("#" + pl.GetColor());
            }
        }

        private void ShowTokens()
        {
            Console.WriteLine();
            Console.WriteLine("Okay, her er dine Tokens:");
            foreach (Token tok in this.tokens)
            {
                Console.WriteLine("TokenID: " + tok.GetTokenId());
                Console.WriteLine("Color: " + tok.GetColor());
                Console.WriteLine("PlayerID: " + tok.GetPlayerId());
            }
        }
    }
}
