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

        bool hasMoveSucceded;

        private readonly int numberOfPlayers; //Defines the number of players in the game
        private Player[] players; //defines the array of players
        private int plrArrayId = 0; //Defines the player's turn
        private Field[] fields; //Defines the fields in the game
        private Field[] innerFields;

        Dice die = new Dice(); //Makes an object of the class 'Dice'

        //---------------- Constructor ----------------
        public Game()
        {
            //MusicHandler.DeathSound();
            

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

            Console.Write("Hvor mange spillere?: "); //Asks for how many players there will be in this game

            while(numOfPlayers < 2 || numOfPlayers > 4) //hecks if there is less than 2 or more than 4
            {
                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out numOfPlayers)) //Tries to save the input as 'this.numberOfPlayers'
                {
                    Console.WriteLine(); //Makes a blank space
                    Console.WriteLine("Ugyldig værdi, vælg et tal mellem 2 og 4\n");
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
                Console.Write("Hvad hedder spiller {0}: ", (i+1)); //Asks for the players name
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

            for (int i = 0; i <= 3; i++) //runs four times
            {
                switch (index) //gives the same color to the tokens as the player
                {
                    case 0:
                        tokens[i] = new Token((i + 1), GameColor.Yellow, startPos); //Defines the color for the token
                        break;
                    case 1:
                        tokens[i] = new Token((i + 1), GameColor.Blue, startPos); //Defines the color for the token
                        break;
                    case 2:
                        tokens[i] = new Token((i + 1), GameColor.Red, startPos); //Defines the color for the token
                        break;
                    case 3:
                        tokens[i] = new Token((i + 1), GameColor.Green, startPos); //Defines the color for the token
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

            fields = new Field[52]; //creates the field array
            innerFields = new Field[5];

            for(int i = 0; i < 52; i++)
            {
                fields[i] = new Field(i + 1); //gives the fields the correct data

                if(i < 5)
                {
                    innerFields[i] = new Field(i);
                }
            }
        }

        //--------------------- DIVIDER ---------------------

        //Each players turn
        private void Turn()
        {
            while(state == GameState.InPlay) //Checks if the game is on
            {
                Player turn = players[(plrArrayId)]; //Finds the player in the array
                Console.WriteLine("Det er " + turn.GetName() + "'s tur\n"); //Some 'nice' output
                do
                {
                    Console.Write("Tryk 'k' for at kaste med terningen: ");
                }
                while (Console.ReadKey().KeyChar != 'k');
                Console.WriteLine();
                Console.WriteLine("Du slog: " + die.ThrowDice().ToString());
                Console.WriteLine();
                CanIMove(turn.GetTokens()); //Checks if the player can move
            }
        }

        //--------------------- DIVIDER ---------------------

        //Checks if the player can move
        private void CanIMove(Token[] tokens)
        {
            int choice = 0; //How many tokens can the player move

            Console.WriteLine("Her er dine brikker:\n");
            foreach(Token tkn in tokens) //Begins to write the tokens of the player
            {
                Console.Write("Brik nr: " + tkn.GetTokenId() + "   er placeret: " + tkn.TokenState); //Writes the id and state of each of the tokens

                switch(tkn.TokenState) //Begins to check if the player can do anything with his/hers tokens
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
                    case TokenState.Finished:
                        Console.Write(" <- Er ved mål");
                        break;
                    default:
                        Console.Write(" <- Kan spilles : " + tkn.TokenPosition + " ");
                        choice++;
                        break;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Du har " + choice.ToString() + " muligheder i den her tur\n");

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

            Console.WriteLine("Skifter spiller\n");
            Turn();
        }

        //--------------------- DIVIDER ---------------------

        //Lets the player choose the token to move
        private int ChooseTokenToMove()
        {
            int tokenToMove = 0; //Only used in this method

            //Clear(); //Makes the console look nicer
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

        //--------------------- DIVIDER ---------------------


        //Moves the token
        private void MoveToField(Player[] plr, int dieRoll)
        {

            foreach(Player pl in plr)
            {
                Console.WriteLine("Spiller: " + pl.GetName() + " " + pl.GetId());
            }

            int tknId = ChooseTokenToMove();
            int fieldToMove = plr[plrArrayId].GetToken(tknId).TokenPosition + die.GetValue(); //Calculates the field to move the token to

            int startPos = plr[plrArrayId].GetStartpoint(); //Gets the starting position of each individual player

                plr[plrArrayId].GetToken(tknId).TokenState = TokenState.InPlay;
                switch (dieRoll)
                {
                    case (6): //OPTIMIZE
                        if (plr[plrArrayId].GetToken(tknId).Equals(TokenState.InPlay))
                        {
                            hasMoveSucceded = fields[(plr[plrArrayId].GetToken(tknId).TokenPosition + 6)].PlaceToken(plr[plrArrayId].GetToken(tknId),
                            plr[plrArrayId].GetColor(), die.GetValue());

                            plr[plrArrayId].GetToken(tknId).TokenPosition = die.GetValue();
                        }
                        else
                        {
                            MoveToken(plr, tknId, fieldToMove);
                            plr[plrArrayId].GetToken(tknId).TokenPosition = plr[plrArrayId].GetStartpoint();

                            plr[plrArrayId].GetToken(tknId).TokenPosition = plr[plrArrayId].GetStartpoint() + die.GetValue();
                        }
                        break;
                    default:
                        if (plr[plrArrayId].GetToken(tknId).TokenState == TokenState.InPlay || plr[plrArrayId].GetToken(tknId).TokenState == TokenState.Safe)
                        {
                        MoveToken(plr, tknId, fieldToMove);

                        plr[plrArrayId].GetToken(tknId).TokenPosition += die.GetValue();
                        }
                        else
                        {
                            Console.WriteLine("This Token Can't move.");
                        }
                        break;
                }
                Console.WriteLine("\n" + hasMoveSucceded);
        }

        //--------------------- DIVIDER ---------------------

        //Moves the token
        private void MoveToken(Player[] plr, int tknId, int fieldToMove)
        {
            //TODO make Fully working movement

            hasMoveSucceded = fields[fieldToMove].PlaceToken(plr[plrArrayId].GetToken(tknId), plr[plrArrayId].GetColor(), die.GetValue());
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
        
        //Gets a list of all the fields
        private void GetField()
        {
            foreach(Field fi in this.fields)
            {
                Console.WriteLine(fi.GetFieldId() + " - " + fi.GetFieldColor());
            }
        }
    }
}