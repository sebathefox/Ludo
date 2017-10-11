using System;
using System.Linq;

namespace Ludo2
{
    //The different colors the users can have
    public enum GameColor { Yellow, Blue, Red, Green, None};
    public enum FieldType { Home, Safe, InPlay, Finish };
    public enum GameState { InPlay, Finished};

    public class Game
    {
        
        private GameState state;


        //Player initialization
        private int numberOfPlayers;
        private Player[] players;

        //Field initialization
        private Field[] fields;

        //Misc
        Dice die = new Dice();

        //---------------- Constructor ----------------
        public Game()
        {
            Clear();
            SetNumberOfPlayers();
            CreatePlayers();
            GetPlayers();
            this.state = GameState.InPlay;


            //CreateField();
            //GetField(); //DEBUG Uncomment when needed

        }

        private void Clear()
        {
            Console.Clear();
            Console.WriteLine("Ludo");
            Console.WriteLine();
        }

        private void MainMenu()
        {
            Console.WriteLine("Velkommen til ludo");
        }

        /*
         * ---------------- Beginning of some sort of methods;) ----------------
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

                Token[] token = TokenAssign(i);

                players[i] = new Player(name, (i + 1), token);
            }
        }

        private Token[] TokenAssign(int colorIndex)
        {
            Token[] tokens = new Token[4];

            for (int i = 0; i <= 3; i++)
            {
                switch (colorIndex)
                {
                    case 0:
                        tokens[i] = new Token(GameColor.Yellow, (i + 1));
                        break;
                    case 1:
                        tokens[i] = new Token(GameColor.Blue, (i + 1));
                        break;
                    case 2:
                        tokens[i] = new Token(GameColor.Red, (i + 1));
                        break;
                    case 3:
                        tokens[i] = new Token(GameColor.Green, (i + 1));
                        break;
                }
            }
            return tokens;
        }

        //---------------- Creates the tokens used by each player ----------------
        /*private void CreateTokens()
        {
            this.tokens = new Token[this.numberOfTokens];

            Console.WriteLine();
            for (int i = 0; i < this.numberOfTokens; i++)
            {
                tokens[i] = new Token(this.color, i + 1, playerId);
            }
        }*/

        private void CreateField()
        {
            this.fields = new Field[52];

            for(int i = 0; i < 52; i++)
            {
                fields[i] = new Field(i + 1, GameColor.None);
            }
        }

        //---------------- Getters, Setters and misc ----------------

        private int ThrowTest()
        {
            string inpt = Console.ReadLine();
            while(true) {
                inpt.ToLower();
                if (inpt == "r" || inpt == "roll")
                {
                    die.ThrowDice();
                    Console.WriteLine("You got: " + die.GetValue());
                    return die.GetValue();
                }
            }
        }

        //TODO
        private void Move()
        {

        }

        private void GetPlayers()
        {
            foreach(Player pl in this.players)
            {
                Console.WriteLine("#" + pl.GetName() + " - " + pl.GetColor() + " - " + pl.GetTokens());
            }
        }
        
        public void GetField()
        {
            foreach(Field fi in this.fields)
            {
                Console.WriteLine(fi.GetFieldId() + " - " + fi.GetFieldColor());
            }
        }
    }
}
