using System;
using System.Linq;
using System.Collections.Generic;

namespace Ludo2
{

    public class Field
    {
        private GameColor color; //used if there is a token on the field
        private readonly int fieldId; //Every field needs an id
        private List<Token> tokensList = new List<Token>(); //creates an List to hold up to four tokens at the same time




        //---------------- Constructor ----------------
        public Field(int fieldId, GameColor color = GameColor.None)
        {
            this.fieldId = fieldId;
            this.color = color;
        }

        //Places the token on the field
        public bool PlaceToken(Token token, GameColor color, int dieRoll)
        {
            if (token.TokenState == TokenState.Safe)
            {
                //TODO make innerFields move method
                if (token.TokenPosition + dieRoll >= 5)
                {
                    RemoveToken();
                    token.TokenState = TokenState.Finished;
                    return true;
                }
                else
                {
                    tokensList.Add(token);
                    Console.WriteLine("ERROR: INNERFIELDS");
                    return true;

                }
            }

            if (tokensList.Count > 1)
            {
                if (token.GetColor() != this.color)
                {
                    KillToken(token); //Kills the token that moved because there was more than 1 enemy token
                    Console.WriteLine("ERROR: NOTTHISCOLOR");
                    return false;
                }
                else
                {
                    tokensList.Add(token);
                    Console.WriteLine("ERROR: THISCOLOR");
                    return true;
                }
            }
            else if (tokensList.Count == 0)
            {
                if (token.GetColor() != this.color && this.color != GameColor.None)
                {
                    KillToken(this.tokensList.ElementAt(0)); //Kills the already placed token

                    PlToken(token);
                    tokensList.RemoveAt(0);
                    Console.WriteLine("ERROR: BACKINCONTROL");
                    return true;
                }
            }
            PlToken(token);
            Console.WriteLine("ERROR: THISISNOTTHEERRORYOURELOOKINGFOR");
            return true;

        }

        private void PlToken(Token token)
        {
            tokensList.Add(token);
            this.color = token.GetColor();
            token.TokenPosition = this.fieldId;
        }

        public void RemoveToken()
        {
            this.color = GameColor.None;
            for (int i = 0; i < (this.tokensList.Count); i++)
            {
                this.tokensList.RemoveAt(i);
            }
        }

        private void KillToken(Token token)
        {
            RemoveToken();

            MusicHandler.DeathSound();

            token.TokenPosition = token.StartPosition;
            token.TokenState = TokenState.Home;
        }

        //---------------- Getters ----------------

        //Gets the color of the field
        public GameColor GetFieldColor() => this.color;

        //Gets the id of the field
        public int GetFieldId() => this.fieldId;
    }
}