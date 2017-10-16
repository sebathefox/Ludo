using System;
using System.Linq;
using System.Threading;

namespace Ludo2
{
    //defining the different enums used in the program
    public enum GameColor { Yellow, Blue, Red, Green, None};
    public enum GameState { InPlay, Finished};

    public class Game
    {
        
        private GameState state;

        private int numberOfPlayers;
        private Player[] players;
        private int playerTurn = 1;
        private Field[] fields;

        Dice die = new Dice();

        //---------------- Constructor ----------------
        public Game()
        {
            Clear();
            SetNumberOfPlayers();
            CreatePlayers();
            CreateField();
            GetPlayers();
            this.state = GameState.InPlay;
            Turn();


            /*
             * TODO 1 Make the methods needed to move a token to a specific field
             * 
             * TODO 2 Make it possible to win the game
             * 
             * TODO 3 Make Stars and 'globusses'?!?
             */

            //PlaceToken(players[playerTurn], players[playerTurn].GetId, players[playerTurn].GetToken[tknId], FieldHere);

            // FIX tknId 
            // FIX FieldHere

            //GetField(); //DEBUG Uncomment when needed

        }

        private void Clear()
        {
            Console.Clear();
            Console.WriteLine("Ludo");
            Console.WriteLine();
        }

        //---------------- Method ----------------
        private void MainMenu()
        {
            Console.WriteLine("Velkommen til ludo");
        }

        //---------------- Method ----------------
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

        //---------------- Method ----------------
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

        //---------------- Method ----------------
        private Token[] TokenAssign(int colorIndex)
        {
            Token[] tokens = new Token[4];

            for (int i = 0; i <= 3; i++)
            {
                switch (colorIndex)
                {
                    case 0:
                        tokens[i] = new Token((i + 1), GameColor.Yellow);
                        break;
                    case 1:
                        tokens[i] = new Token((i + 1), GameColor.Blue);
                        break;
                    case 2:
                        tokens[i] = new Token((i + 1), GameColor.Red);
                        break;
                    case 3:
                        tokens[i] = new Token((i + 1), GameColor.Green);
                        break;
                }
            }
            return tokens;
        }

        //---------------- Method ----------------
        private void CreateField()
        {
            this.fields = new Field[52];

            for(int i = 0; i < 52; i++)
            {
                fields[i] = new Field(i + 1, GameColor.None);
            }
        }

        //---------------- Method ----------------
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

        //---------------- Method ----------------
        private void Turn()
        {
            while(this.state == GameState.InPlay)
            {
                Player turn = players[(playerTurn-1)];
                Console.WriteLine("Det er " + turn.GetName() + "'s tur");
                do
                {
                    Console.WriteLine("Tryk 'k' for at kaste med terningen");
                }
                while (Console.ReadKey().KeyChar != 'k');
                Console.WriteLine("Du slog: " + die.ThrowDice().ToString());
                Console.WriteLine();
                CanIMove(turn.GetTokens());
            }
        }

        //---------------- Method ----------------
        private void CanIMove(Token[] tokens)
        {
            int choice = 0;

            Console.WriteLine("Her er dine brikker:");
            foreach(Token tkn in tokens)
            {
                Console.Write("Brik nr " + tkn.GetTokenId() + ": er placeret: " + tkn.GetState());

                switch(tkn.GetState())
                {
                    case TokenState.Home:
                        if(die.GetValue() == 6)
                        {
                            Console.Write(" - Kan spilles");
                            choice++;
                        }
                        else
                        {
                            Console.Write(" - Kan ikke spilles");
                        }
                        break;
                    default:
                        Console.Write("Kan spilles");
                        choice++;
                        break;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Du har " + choice.ToString() + " muligheder i den her tur");

            if(choice == 0)
            {
                this.ChangeTurn();
            }
            else
            {
                ChooseTokenToMove();
            }
        }

        //---------------- Method ----------------
        private void ChangeTurn()
        {
            Console.WriteLine();
            if(playerTurn == numberOfPlayers)
            {
                playerTurn = 1;
            }
            else
            {
                playerTurn++;
            }

            Console.WriteLine("Skifter spiller");
            Thread.Sleep(8000);
            Clear();
            Turn();
        }

        private int ChooseTokenToMove()
        {
            int tokenToMove = 0; //Only used in this method

            Clear();
            Console.WriteLine("Vælg den brik du vil rykke (Brug et tal fra 1 til 4)");
            while (tokenToMove < 1 || tokenToMove > 4)
            {
                if (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out tokenToMove))
                {
                    Console.WriteLine();
                    Console.WriteLine("Ugyldig værdi, vælg et tal mellem 1 og 4");
                }
            }
            return tokenToMove;
        }

        //---------------- Method ----------------
        private void FieldCalc(GameColor plrColor)
        {
            //CODE
            foreach(Field fi in this.fields)
            {
                if (fi.GetFieldColor() == plrColor)

                {

                }
            }
        }

        //---------------- Method ----------------
        private void PlaceToken(Player plr, int fieldToMoveTo)
        {
            int tknId = ChooseTokenToMove();
            int plrId = players[playerTurn].GetId();

            fields[fieldToMoveTo].PlaceToken(players[plrId].GetToken(tknId) , plr.GetColor());
        }

        //---------------- Getters ----------------

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
