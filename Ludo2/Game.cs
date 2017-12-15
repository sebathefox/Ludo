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

        bool debug;

        private readonly int numberOfPlayers; //Defines the number of players in the game
        private Player[] players; //defines the array of players
        private int plrArrayId = 0; //Defines the player's turn
        private Field[] fields; //Defines the fields in the game

        Dice die = new Dice(); //Makes an object of the class 'Dice'

        //---------------- Constructor ----------------
        public Game()
        {
            this.numberOfPlayers = SetNumberOfPlayers(); //Sets the number of players before the game begins
            CreatePlayers(); //This method creates the players
            CreateField(); //Creates the fields used in the game
            GetPlayers();
            this.state = GameState.InPlay; //Changes the gamestate to play since we're actually beginning to play the game
            Turn(); //Begins player one's turn
        }


        //
        //The initialisation of the game is below
        //

        //Sets the number of players before the game begins
        private int SetNumberOfPlayers()
        {
            int players = 0;

            Console.Write("Hvor mange spillere?: "); //Asks for how many players there will be in this game

            while(players < 2 || players > 4) //hecks if there is less than 2 or more than 4
            {
                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out players)) //Tries to save the input as 'this.numberOfPlayers'
                {
                    Console.WriteLine(); //Makes a blank space
                    Console.WriteLine("Ugyldig værdi, vælg et tal mellem 2 og 4");
                }
            }
            return players;
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

                int startpointAssign = StartpointAssignment(i); //Assigns the startpoint for each of the users
                Token[] token = TokenAssign(i); //Assigns the tokens for the different users

                players[i] = new Player(name, (i + 1), token, startpointAssign); //Defines the players
            }
        }

        //--------------------- DIVIDER ---------------------

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

        //--------------------- DIVIDER ---------------------

        //Assigns the startpoints used by the different users
        private int StartpointAssignment(int i)
        {
            int startPoint = 0; //the startpoint used by the player
            switch (i)
            {
                case 0:
                    startPoint = 0; //Startpoints is calculated like an array AKA begins with 0
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

        //Creates the fields used in the game
        private void CreateField()
        {
            fields = new Field[52]; //creates the field array

            for(int i = 0; i < 52; i++)
            {
                fields[i] = new Field(i + 1); //gives the fields the correct data
            }
        }

        //--------------------- DIVIDER ---------------------

        //
        //Begins the gameplay
        //

        //The players turn
        private void Turn()
        {
            while(state == GameState.InPlay) //Checks if the game is on
            {
                Player turn = players[(plrArrayId)]; //Finds the player in the array
                Console.WriteLine("Det er " + turn.GetName() + "'s tur"); //Some 'nice' output
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
                    case TokenState.InPlay:
                        Console.Write(" <- Kan spilles");
                        choice++;
                        break;
                    case TokenState.Safe:
                        Console.Write(" <- Kan spilles");
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

            Console.WriteLine("Skifter spiller");
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
                Console.WriteLine(pl.GetName() + " " + pl.GetId());
            }

            int tknId = ChooseTokenToMove();
           // int plrId = players[(numberOfPlayers - 1)].GetId(); //Gets the id of the player
            int plrArrayId = numberOfPlayers - 1; //Instead of writing (plrId - 1) everywhere
            int fieldToMove = plr[plrArrayId].GetToken(tknId).GetPosition() + die.GetValue(); //Calculates the field to move the token to

            bool isUsed = fields[fieldToMove].IsTokenPlaced(); //Checks if there is any tokens

            int startPos = plr[plrArrayId].GetStartpoint(); //Gets the starting position of each individual player

            //
            //WARNING If any token has moved Then all tokens can move freely
            //

            if(isUsed == false)
            {
                plr[plrArrayId].GetToken(tknId).SetState(TokenState.InPlay);
                switch (dieRoll)
                {
                    case (6): //OPTIMIZE
                        if (plr[plrArrayId].GetTokens().Equals(TokenState.InPlay))
                        {
                            debug = fields[(plr[plrArrayId].GetToken(tknId).GetPosition() + 6)].PlaceToken(plr[plrArrayId].GetToken(tknId),
                            plr[plrArrayId].GetColor(), die.GetValue());

                            plr[plrArrayId].GetToken(tknId).SetPosition(die.GetValue());
                        }
                        else
                        {
                            MoveToken(plr, tknId, fieldToMove);
                            plr[plrArrayId].GetToken(tknId).SetPosition(plr[plrArrayId].GetStartpoint());

                            plr[plrArrayId].GetToken(tknId).SetPosition(die.GetValue());
                        }
                        break;
                    default:
                        MoveToken(plr, tknId, fieldToMove);

                        plr[plrArrayId].GetToken(tknId).SetPosition(die.GetValue());
                        break;
                }
                Console.WriteLine(debug);
            }
        }

        //--------------------- DIVIDER ---------------------

        //Moves the token
        private void MoveToken(Player[] plr, int tknId, int fieldToMove)
        {
            debug = fields[fieldToMove].PlaceToken(plr[plrArrayId].GetToken(tknId), plr[plrArrayId].GetColor(), die.GetValue());
        }

        //--------------------- DIVIDER ---------------------

        //---------------- Getters ----------------

        //Gets a lisst of all the players
        private void GetPlayers()
        {
            foreach(Player pl in players)
            {
                Console.WriteLine("#" + pl.GetName() + " - " + pl.GetColor() + " - " + pl.GetTokens() + " - " + pl.GetStartpoint());
            }
        }
        
        //Gets a list of all the fields DEBUG ONLY NOT RELEASE
        private void GetField()
        {
            foreach(Field fi in this.fields)
            {
                Console.WriteLine(fi.GetFieldId() + " - " + fi.GetFieldColor());
            }
        }
    }
}
