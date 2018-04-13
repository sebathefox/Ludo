using System;
using System.Diagnostics;

namespace Ludo2
{
    public enum GameColor { Yellow, Blue, Red, Green, White}; //The colors that can be used --notice 'None' is used for fields

    public class Game
    {

        #region Fields
        private readonly int numberOfPlayers; //Defines the number of players in the game
        private readonly int delay = 120; //The General delay for output
        private int playerTurn = 0; //Defines the player's turn
        private int tries = 0;

        private Player[] players; //defines the array of players
        private Field[] fields; //Defines the fields in the game
        private Dice die = new Dice(); //Makes an object of the class 'Dice'

        #endregion
        
        public Game()
        {
            #if DEBUG
                Debug.WriteLine("DEBUGMODE");
            #endif

            Design.Clear(500);
            this.numberOfPlayers = SetNumberOfPlayers(); //Sets the number of players before the game begins
            CreatePlayers(); //This method creates the players
            CreateField(); //Creates the fields used in the game
            GetPlayers();
            Turn(); //Begins player one's turn
        }

        #region Initialization
        
        private int SetNumberOfPlayers()
        {
            int numOfPlayers = 0;

            Console.Write("How many players?: "); //Asks for how many players there will be in this game

            while (numOfPlayers < 2 || numOfPlayers > 4) //Checks if there is less than 2 or more than 4
            {
                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out numOfPlayers)) //Tries to save the input as 'this.numberOfPlayers'
                {
                    Console.WriteLine(); //Makes a blank space
                    Design.Clear(160);
                    Console.Write("Unknown input, choose between 2 and 4 players: ");
                }
            }

            PrintLog("NumberOfPlayers: " + numOfPlayers);

            return numOfPlayers;
        }
        
        private void CreatePlayers()
        {
            this.players = new Player[this.numberOfPlayers]; //Initializes the players array

            Console.WriteLine();
            for (int i = 0; i < this.numberOfPlayers; i++) //Runs until all users have names
            {
                Design.Clear(300);
                Console.Write("What is the name of player {0}: ", (i+1)); //Asks for the players name
                string name = Console.ReadLine(); //saves the name as a temporary variable called 'name'
                
                Token[] token = TokenAssign(i); //Assigns the tokens for the different users

                players[i] = new Player(name, (i + 1), token); //Initalizes each player in the array

                PrintLog("Player " + i + " name: " + name);

            }
        }

        //Assigns the tokens -- used in the method above
        private Token[] TokenAssign(int index)
        {
            Token[] tokens = new Token[4]; //Makes a new temporary array
            
            for (int i = 0; i <= 3; i++) //runs four times
            {
                switch (index) //gives the same color to the tokens as the player
                {
                    case 0:
                        tokens[i] = new Token((i + 1), GameColor.Yellow, 0); //Defines the color for the token
                        break;
                    case 1:
                        tokens[i] = new Token((i + 1), GameColor.Blue, 13); //Defines the color for the token
                        break;
                    case 2:
                        tokens[i] = new Token((i + 1), GameColor.Red, 26); //Defines the color for the token
                        break;
                    case 3:
                        tokens[i] = new Token((i + 1), GameColor.Green, 39); //Defines the color for the token
                        break;
                }
            }
            return tokens;
        }

        //Creates the fields used in the game
        private void CreateField()
        {
            fields = new Field[57]; //creates the field array

            for(int i = 0; i < 57; i++)
            {
                fields[i] = new Field(i + 1); //gives the fields the correct data
            }
        }

        #endregion

        #region MainGameplay

        //Each players turn
        private void Turn()
        {
            while(true) //Checks if the game is on
            {
                Player turn = players[(playerTurn)]; //Finds the player in the array
                Design.Clear(delay);
                Console.WriteLine("It is " + turn.Name + "'s turn\n"); //Some 'nice' output
                do
                {
                    Console.Write("Press 'K' to roll the die: ");
                }
                while (Console.ReadKey().KeyChar != 'k');
                Console.WriteLine();
                Design.Clear(delay);
                Console.WriteLine("You got: " + die.ThrowDice());
                Console.WriteLine();
                CanMove(turn); //Checks if the player can move
            }
        }

        //OPTIMIZE CanMove
        //Checks if the player can move
        private void CanMove(Player turn)
        {
            Token[] tokens = turn.GetTokens();

            int choice = 0; //How many tokens can the player move
            int finish = 0;

            Console.WriteLine("Here are your pieces:");
            Design.WriteLine("", delay);
            foreach (Token tkn in tokens) //Begins to write the tokens of the player
            {
                Console.Write("piece number: " + tkn.Id + " are placed: " + tkn.State); //Writes the id and state of each of the tokens
                switch (tkn.State) //Begins to check if the player can do anything with his/hers tokens
                {
                    case TokenState.Home:
                        if(die.GetValue == 6)
                        {
                            Console.Write(" - Can move");
                            choice++; //Can move this token AKA a choice
                            tkn.CanMove = true;
                        }
                        else
                        {
                            Console.Write(" - Can not move");
                            tkn.CanMove = false;
                        }
                        break;
                    case TokenState.Finished:
                        Console.Write(" <- Is finished");
                        tkn.CanMove = false;
                        finish++;
                        break;
                    default:
                        Console.Write(" <- Can move : " + tkn.Position + " ");
                        choice++;
                        tkn.CanMove = true;
                        break;
                }
                Design.WriteLine("", delay);
            }

            if (finish >= 4)
            {
                Finish(turn); //Finishes the game
            }

            Design.WriteLine("<------------------------------------------------>");
            Console.WriteLine(tokens[0].Color.ToString() + " have " + choice.ToString() + " options in this turn\n");

            tries++;

            if (tries < 3 && die.GetValue < 6 && choice == 0)
            {
                Turn();
            }

            tries = 0;

            if(choice == 0) //Cant do anything this turn skips the player
            {
                ChangeTurn();
            }
            else
            {
                MoveToField(players[playerTurn]);
                ChangeTurn();
            }
        }

        //Changes the turn to the next player
        private void ChangeTurn()
        {
            Console.WriteLine();
            if(playerTurn == (numberOfPlayers - 1))
            {
                playerTurn = 0;
            }
            else
            {
                playerTurn++;
            }

            Console.WriteLine("Changing player\n");
            Turn();
        }

        #endregion

        #region Movement

        //Lets the player choose the token to move
        private int ChooseTokenToMove()
        {
            int tokenToMove = 0; //Temporary variable

            Console.WriteLine("Choose a piece to move (Use a number between 1 and 4)");
            while (tokenToMove < 1 || tokenToMove > 4)
            {
                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out tokenToMove))
                {
                    Console.WriteLine();
                    Console.WriteLine("Unknown input, Use a number between 1 and 4");
                }
            }
            return tokenToMove -1;
        }

        //Moves the token
        //OPTIMIZE MoveToField
        private void MoveToField(Player player)
        {

            Token turnToken = player.GetToken(ChooseTokenToMove());

            if (!turnToken.CanMove)
            {
                MoveToField(player);
            }
            else
            {
                
                    turnToken.MoveToken(ref fields, die.GetValue);
            }
        }

        #endregion

        private void Finish(Player winner)
        {
            Design.SlowPrint("Player" + winner.Name + "Has won the game!", 70);
            Design.WriteLine("Press 'Any Key' to exit (ONLY that key will work)");
            Console.ReadKey();

            Environment.Exit(0);
        }

        [Conditional("DEBUG")]
        private void PrintLog(string dataLog)
        {
            Debug.WriteLine(dataLog);
        }

        #region Getters

        //Gets a list of all the players (Currently not in use)
        private void GetPlayers()
        {
            foreach(Player pl in players)
            {
                Console.WriteLine("#" + pl.Name + " - " + pl.GetToken(1).Color + " - " + pl.GetToken(1).StartPosition);

                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine(pl.GetToken(i));
                }
            }
        }
        #endregion
    }
}