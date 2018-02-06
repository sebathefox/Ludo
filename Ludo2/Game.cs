using System;
using System.Linq;

namespace Ludo2
{
    //defining the different enums used in the program
    public enum GameColor { Yellow, Blue, Red, Green, None}; //The colors that can be used --notice 'None' is used for fields
    public enum GameState { InPlay, Finished}; //Tells if the game is finished or if it is currently running

    public class Game
    {
        
        private GameState state; //Defines a gamestate variable

        private bool hasMoveSucceded; //Debug purpose

        private readonly int numberOfPlayers; //Defines the number of players in the game
        private Player[] players; //defines the array of players
        private int plrArrayId = 0; //Defines the player's turn
        private Field[] fields; //Defines the fields in the game

        Dice die = new Dice(); //Makes an object of the class 'Dice'

        //---------------- Constructor ----------------
        public Game()
        {
            MusicHandler.DeathSound();
            

            this.numberOfPlayers = SetNumberOfPlayers(); //Sets the number of players before the game begins
            CreatePlayers(); //This method creates the players
            CreateField(); //Creates the fields used in the game
            GetPlayers(); //Debug
            GetField();
            this.state = GameState.InPlay; //Changes the gamestate to play since we're actually beginning to play the game
            Turn(); //Begins player one's turn
        }


        //
        //The initialisation of the game is below
        //

        //Sets the number of players before the game begins
        private int SetNumberOfPlayers()
        {
            int numOfPlayers = 0;

            Console.Write("How many players?: "); //Asks for how many players there will be in this game

            while(numOfPlayers < 2 || numOfPlayers > 4) //hecks if there is less than 2 or more than 4
            {
                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out numOfPlayers)) //Tries to save the input as 'this.numberOfPlayers'
                {
                    Console.WriteLine(); //Makes a blank space
                    Console.WriteLine("Unknown input, choose between 2 and 4\n");
                }
            }
            return numOfPlayers;
        }

        //--------------------- DIVIDER ---------------------

        //This method creates the players
        private void CreatePlayers()
        {
            this.players = new Player[this.numberOfPlayers]; //makes the player array

            Console.WriteLine();
            for (int i = 0; i < this.numberOfPlayers; i++) //Runs until all users have names
            {
                Console.Write("What is the name of player {0}: ", (i+1)); //Asks for the players name
                string name = Console.ReadLine(); //saves the name as a temporary variable called 'name'

                //int startpointAssign = StartpointAssignment(i); //Assigns the startpoint for each of the users
                Token[] token = TokenAssign(i); //Assigns the tokens for the different users

                players[i] = new Player(name, (i + 1), token, token[i].StartPosition); //Defines the players
            }
        }

        //--------------------- DIVIDER ---------------------

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

        //--------------------- DIVIDER ---------------------

        //Assigns the tokens -- used in the method above
        private Token[] TokenAssign(int index)
        {
            Token[] tokens = new Token[4]; //Makes a new array

            int startPos = StartpointAssignment(index);

            int endPos = startPos - 2;

            if (endPos < 0)
            {
                endPos = 50;
            }

            for (int i = 0; i <= 3; i++) //runs four times
            {
                switch (index) //gives the same color to the tokens as the player
                {
                    case 0:
                        tokens[i] = new Token((i + 1), GameColor.Yellow, startPos, endPos); //Defines the color for the token
                        break;
                    case 1:
                        tokens[i] = new Token((i + 1), GameColor.Blue, startPos, endPos); //Defines the color for the token
                        break;
                    case 2:
                        tokens[i] = new Token((i + 1), GameColor.Red, startPos, endPos); //Defines the color for the token
                        break;
                    case 3:
                        tokens[i] = new Token((i + 1), GameColor.Green, startPos, endPos); //Defines the color for the token
                        break;
                }
            }
            return tokens;
        }

        //--------------------- DIVIDER ---------------------

        //Creates the fields used in the game
        private void CreateField()
        {
            //TODO make innerfields
            fields = new Field[57]; //creates the field array

            for(int i = 0; i < 57; i++)
            {
                fields[i] = new Field(i + 1); //gives the fields the correct data
            }
        }

        //--------------------- DIVIDER ---------------------

        //Each players turn
        private void Turn()
        {
            while(state == GameState.InPlay) //Checks if the game is on
            {
                Player turn = players[(plrArrayId)]; //Finds the player in the array
                Console.WriteLine("It is " + turn.GetName() + "'s turn\n"); //Some 'nice' output
                do
                {
                    Console.Write("Press 'K' to roll the die: ");
                }
                while (Console.ReadKey().KeyChar != 'k');
                Console.WriteLine();
                Console.WriteLine("You got: " + die.ThrowDice().ToString());
                Console.WriteLine();
                CanIMove(turn.GetTokens()); //Checks if the player can move
            }
        }

        //--------------------- DIVIDER ---------------------

        //Checks if the player can move
        private void CanIMove(Token[] tokens)
        {
            int choice = 0; //How many tokens can the player move

            Console.WriteLine("Here are your tokens:\n");
            foreach(Token tkn in tokens) //Begins to write the tokens of the player
            {
                Console.Write("Token number: " + tkn.GetTokenId() + " are placed: " + tkn.TokenState); //Writes the id and state of each of the tokens

                switch(tkn.TokenState) //Begins to check if the player can do anything with his/hers tokens
                {
                    case TokenState.Home:
                        if(die.GetValue() == 6)
                        {
                            Console.Write(" - Can move");
                            choice++; //Can move this token AKA a choice
                        }
                        else
                        {
                            Console.Write(" - Can not move");
                        }
                        break;
                    case TokenState.Finished:
                        Console.Write(" <- Is finished");
                        break;
                    default:
                        Console.Write(" <- Can move : " + tkn.TokenPosition + " ");
                        choice++;
                        break;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("You have " + choice.ToString() + " options in this turn\n");

            if(choice == 0) //Cant do anything this turn skips the player
            {
                ChangeTurn();
            }
            else
            {
                MoveToField(players , die.GetValue());
                ChangeTurn();
            }
        }

        //--------------------- DIVIDER ---------------------

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

            Console.WriteLine("Changing player\n");
            Turn();
        }

        //--------------------- DIVIDER ---------------------

        //Lets the player choose the token to move
        private int ChooseTokenToMove()
        {
            int tokenToMove = 0; //Only used in this method

            Console.WriteLine("Choose a token to move (Use a number between 1 and 4)");
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

        //--------------------- DIVIDER ---------------------

        //Moves the token
        private void MoveToField(Player[] plr, int dieRoll)
        {
            foreach (Player pl in plr)
            {
                Console.WriteLine("Player: " + pl.GetName() + " " + pl.GetId());
            }

            int tknId = ChooseTokenToMove(); //Gets the id of the token to move

            Token turnToken = plr[plrArrayId].GetToken(tknId); //Saves the token in a temporary object

            int fieldToRemove = turnToken.TokenPosition; //The field to remove the token from
            int fieldToMove = turnToken.TokenPosition + (die.GetValue() - 1); //The field the token should move to

            int startPos = turnToken.StartPosition; //The startposition of this token (all of this players tokens have the same startposition)

            //TODO make innerfields code

            if (turnToken.Counter + dieRoll > 52)
            {

                switch(turnToken.GetColor())
                {
                    case GameColor.Yellow:
                        int tempz = dieRoll + (turnToken.TokenPosition - 52);
                        fieldToMove = 52 + tempz;

                        MoveToken(turnToken, fieldToMove, fieldToRemove);

                        break;
                }

                //TODO actually place the token in the inner fields

                //turnToken.TokenState = TokenState.Safe;
                //int tempPosition = turnToken.TokenPosition - 56;
                //fieldToMove = 56 + tempPosition;
                //MoveToken(turnToken, fieldToMove, fieldToRemove);
            }
            else
            {

                if (fieldToMove >= 52 && turnToken.TokenState == TokenState.InPlay)
                {
                    fieldToMove = turnToken.TokenPosition + (die.GetValue() - 51);
                    MoveToken(turnToken, fieldToMove, fieldToRemove);
                }
                else
                {
                    turnToken.TokenState = TokenState.InPlay;

                    switch (dieRoll)
                    {
                    case (6):
                        if (turnToken.TokenState.Equals(TokenState.InPlay))
                        {
                            MoveToken(turnToken, fieldToMove, fieldToRemove);
                        }
                        else if (turnToken.TokenState.Equals(TokenState.Home))
                        {
                            MoveToken(turnToken, startPos, fieldToRemove);
                        }
                        else
                        {
                            MoveToken(turnToken, fieldToMove, fieldToRemove);
                        }
                        break;
                    default:
                        if (turnToken.TokenState == TokenState.InPlay || turnToken.TokenState == TokenState.Safe)
                        {
                            MoveToken(turnToken, fieldToMove, fieldToRemove);
                        }
                        else
                        {
                            Console.WriteLine("This Token Can't move.");
                        }
                        break;
                    }
                }
            }
            Console.WriteLine("\n" + hasMoveSucceded); //Uncomment for debug
        }

        //--------------------- DIVIDER ---------------------

        //Moves the token
        private void MoveToken(Token token, int fieldToMove, int fieldToRemove)
        {
            token.Counter += die.GetValue();

            fields[fieldToRemove].RemoveToken();

            hasMoveSucceded = fields[fieldToMove].PlaceToken(token, token.GetColor(), die.GetValue());
            //}
        }

        //Gets a list of all the players
        private void GetPlayers()
        {
            foreach(Player pl in players)
            {
                Console.WriteLine("#" + pl.GetName() + " - " + pl.GetColor() + " - " + pl.GetStartpoint());

                for (int i = 0; i < 4; i++)
                {
                    Console.WriteLine(pl.GetToken(i));
                }
            }
        }
        
        //Gets a list of all the fields USED FOR DEBUGGING
        private void GetField()
        {
            foreach(Field fi in this.fields)
            {
                Console.WriteLine(fi.GetFieldId() + " - " + fi.GetFieldColor());
            }
        }
    }
}