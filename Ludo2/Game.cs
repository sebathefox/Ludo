using System;
using System.Linq;
using System.Threading;

namespace Ludo2
{
    //defining the different enums used in the program
    public enum GameColor { Yellow, Blue, Red, Green, None}; //The colors that can be used --notice 'None' is used for fields
    public enum GameState { InPlay, Finished}; //Tells if the game is finished or if it is currently running

    public class Game
    {
        
        private GameState state; //Defines a gamestate variable

        private int numberOfPlayers; //Defines the number of players in the game
        private Player[] players; //defines the array of players
        private int plrArrayId = 0; //Defines the player's turn
        private Field[] fields; //Defines the fields in the game

        Dice die = new Dice(); //Makes an object of the class 'Dice'

        //---------------- Constructor ----------------
        public Game()
        {
            SetNumberOfPlayers(); //Sets the number of players before the game begins
            CreatePlayers(); //This method creates the players
            CreateField(); //Creates the fields used in the game
            //GetField(); //DEBUG Uncomment when needed
            GetPlayers();
            this.state = GameState.InPlay; //Changes the gamestate to play since we're actually beginning to play the game
            Turn(); //Begins player one's turn
        }


        //Used to clear the console and write ludo on the top
        private void Clear()
        {
            Console.Clear(); //Clears the console
            Console.WriteLine("Ludo"); //Writes ludo to the console
            Console.WriteLine(this.players[plrArrayId].ToString());
            Console.WriteLine(); //makes a blank line
        }

        //Useless..................................................
        private void MainMenu()
        {
            Console.WriteLine("Velkommen til ludo");
        }

        //Sets the number of players before the game begins
        private void SetNumberOfPlayers()
        {
            Console.Write("Hvor mange spillere?: "); //Asks for how many players there will be in this game

            while(this.numberOfPlayers < 2 || this.numberOfPlayers > 4) //hecks if there is less than 2 or more than 4
            {
                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out this.numberOfPlayers)) //Tries to save the input as 'this.numberOfPlayers'
                {
                    Console.WriteLine(); //Makes a blank space
                    Console.WriteLine("Ugyldig værdi, vælg et tal mellem 2 og 4");
                }
            }
        }

        //This method creates the players
        private void CreatePlayers()
        {
            this.players = new Player[this.numberOfPlayers]; //makes the player array

            Console.WriteLine();
            for (int i = 0; i < this.numberOfPlayers; i++) //Runs until all users have names
            {
                Console.Write("Hvad hedder spiller {0}: ", (i+1)); //Asks for the players name
                string name = Console.ReadLine(); //saves the name as a temporary variable called 'name'

                int startpointAssign = StartpointAssignment(i); //Assigns the startpoint for each of the users
                Token[] token = TokenAssign(i); //Assigns the tokens for the different users

                players[i] = new Player(name, (i + 1), token, startpointAssign); //Defines the players
            }
        }

        //Assigns the tokens -- used in the method above
        private Token[] TokenAssign(int colorIndex)
        {
            Token[] tokens = new Token[4]; //Makes a new array

            for (int i = 0; i <= 3; i++) //runs four times
            {
                switch (colorIndex) //gives the same color to the tokens as the player
                {
                    case 0:
                        tokens[i] = new Token((i + 1), GameColor.Yellow); //Defines the color for the token
                        break;
                    case 1:
                        tokens[i] = new Token((i + 1), GameColor.Blue); //Defines the color for the token
                        break;
                    case 2:
                        tokens[i] = new Token((i + 1), GameColor.Red); //Defines the color for the token
                        break;
                    case 3:
                        tokens[i] = new Token((i + 1), GameColor.Green); //Defines the color for the token
                        break;
                }
            }
            return tokens;
        }
        
        //Assigns the startpoints used by the different users
        private int StartpointAssignment(int i)
        {
            int startPoint = 0; //the startpoint used by the player
            switch (i)
            {
                case 0:
                    startPoint = 0;
                    break;
                case 1:
                    startPoint = 13;
                    break;
                case 2:
                    startPoint = 26;
                    break;
                case 3:
                    startPoint = 39;
                    break;
            }
            return startPoint;
        }

        //Creates the fields used in the game
        private void CreateField()
        {
            fields = new Field[52]; //creates the field array

            for(int i = 0; i < 52; i++)
            {
                fields[i] = new Field(i + 1, GameColor.None); //gives the fields the correct data
            }
        }

        //The players turn
        private void Turn()
        {
            while(state == GameState.InPlay) //Checks if the game is on
            {
                Player turn = players[(plrArrayId)]; //Finds the player in the array
                Console.WriteLine("Det er " + turn.GetName() + "'s tur"); //Some 'nice' output
                do
                {
                    Console.WriteLine("Tryk 'k' for at kaste med terningen");
                }
                while (Console.ReadKey().KeyChar != 'k');
                //Clear(); //makes the console nicer
                Console.WriteLine();
                Console.WriteLine("Du slog: " + die.ThrowDice().ToString());
                Console.WriteLine();
                CanIMove(turn.GetTokens()); //Checks if the player can move
            }
        }

        //Checks if the player can move
        private void CanIMove(Token[] tokens)
        {
            int choice = 0; //How many tokens can the player move

            Console.WriteLine("Her er dine brikker:");
            foreach(Token tkn in tokens) //Begins to write the tokens of the player
            {
                Console.Write("Brik nr " + tkn.GetTokenId() + ": er placeret: " + tkn.GetState()); //Writes the id and state of each of the tokens

                switch(tkn.GetState()) //Begins to check if the player can do anything with his/hers tokens
                {
                    case TokenState.Home:
                        if(die.GetValue() == 6)
                        {
                            Console.Write(" - Kan spilles");
                            choice++; //Can move this token AKA a choice
                        }
                        else
                        {
                            Console.Write(" - Kan ikke spilles");
                        }
                        break;
                    default: //If token not home you can move
                        Console.Write("Kan spilles");
                        choice++;
                        break;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Du har " + choice.ToString() + " muligheder i den her tur");

            if(choice == 0) //Cant do anything this turn skips the player
            {
                ChangeTurn();
            }
            else
            {
                MoveToField(players , die.GetValue());
            }
        }

        //Changes the turn to the next player
        private void ChangeTurn()
        {
            Console.WriteLine();
            if(plrArrayId == (numberOfPlayers - 1))
            {
                plrArrayId = 0;
            }
            else
            {
                plrArrayId++;
            }

            Console.WriteLine("Skifter spiller");
            //Thread.Sleep(8000); //Improvised wait Use with care
            //Clear();
            Turn();
        }

        //Lets the player choose the token to move
        private int ChooseTokenToMove()
        {
            int tokenToMove = 0; //Only used in this method

            Clear(); //Makes the console look nicer
            Console.WriteLine("Vælg den brik du vil rykke (Brug et tal fra 1 til 4)");
            while (tokenToMove < 1 || tokenToMove > 4)
            {
                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out tokenToMove))
                {
                    Console.WriteLine();
                    Console.WriteLine("Ugyldig værdi, vælg et tal mellem 1 og 4");
                }
            }
            return tokenToMove -1;
        }

        //Moves the token
        private void MoveToField(Player[] plr, int dieRoll)
        {

            foreach(Player pl in plr)
            {
                Console.WriteLine(pl.GetName() + " " + pl.GetId());
            }

            int tknId = ChooseTokenToMove();
           // int plrId = players[(numberOfPlayers - 1)].GetId(); //Gets the id of the player
            int plrArrayId = numberOfPlayers - 1; //Instead of writing (plrId - 1) everywhere

            //Start Of FIX
            int fieldToMove = plr[plrArrayId].GetToken(tknId).GetPosition() + die.GetValue();

            //End Of FIX
            bool isUsed = fields[fieldToMove].IsTokenPlaced(); //Checks if there is any tokens

            int startPos = plr[plrArrayId].GetStartpoint();

            if(isUsed == false)
            {
                plr[plrArrayId].GetToken(tknId).SetState(TokenState.InPlay);
                switch (dieRoll)
                {
                    case (6): //OPTIMIZE
                        if (plr[plrArrayId].GetTokens().Equals(TokenState.InPlay))
                        {
                            fields[(plr[plrArrayId].GetToken(tknId).GetPosition() + 6)].PlaceToken(plr[plrArrayId].GetToken(tknId),
                            plr[plrArrayId].GetColor());
                        }
                        else
                        {
                            fields[startPos].PlaceToken(plr[plrArrayId].GetToken(tknId), plr[plrArrayId].GetColor());
                            plr[plrArrayId].GetToken(tknId).SetPosition(plr[plrArrayId].GetStartpoint());
                        }
                        break;
                    case (5):
                        MoveToken(plr, tknId, fieldToMove);
                        break;
                    case (4):
                        MoveToken(plr, tknId, fieldToMove);
                        break;
                    case (3):
                        MoveToken(plr, tknId, fieldToMove);
                        break;
                    case (2):
                        MoveToken(plr, tknId, fieldToMove);
                        break;
                    case (1):
                        MoveToken(plr, tknId, fieldToMove);
                        break;
                }
            }
        }

        //Moves the token
        private void MoveToken(Player[] plr, int tknId, int fieldToMove)
        {
            fields[fieldToMove].PlaceToken(plr[plrArrayId].GetToken(tknId), plr[plrArrayId].GetColor());
        }

        //---------------- Getters ----------------

        //Gets a lisst of all the players
        private void GetPlayers()
        {
            foreach(Player pl in players)
            {
                Console.WriteLine("#" + pl.GetName() + " - " + pl.GetColor() + " - " + pl.GetTokens() + " - " + pl.GetStartpoint());
            }
        }
        
        //Gets a list of all the fields DEBUG ONLY
        public void GetField()
        {
            foreach(Field fi in this.fields)
            {
                Console.WriteLine(fi.GetFieldId() + " - " + fi.GetFieldColor());
            }
        }
    }
}
