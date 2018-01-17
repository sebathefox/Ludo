using System;
using System.Linq;

namespace Ludo2
{

    public class Field
    {
        private GameColor color; //used if there is a token on the field
        private readonly int fieldId; //Every field needs an id
        private Token[] tokens = new Token[4]; //creates an array to hold up to four tokens at the same time

        //---------------- Constructor ----------------
        public Field(int fieldId, GameColor color = GameColor.None)
        {
            //Self explanatory
            this.fieldId = fieldId;
            this.color = color;
        }

        //Places the token on the field
        public bool PlaceToken(Token token, GameColor color, int dieRoll)
        {
            //(tokens.Any()) //checks if there is any tokens on the field
            //{
                //REWRITE Tokens will currently always return false when trying to move

                    if (tokens.Length > 1)
                    {
                        if (token.GetColor() != this.color)
                        {

                            KillToken(token); //Kills the token that moved because there was more than 1 enemy token
                            return false;
                        }
                    }
                    else if (tokens.Length <= 1)
                    {
                        if (token.GetColor() != this.color)
                        {
                            KillToken(this.tokens[0]); //Kills the already placed token

                            tokens[0] = token;
                            this.color = token.GetColor();

                            token.TokenPosition = this.fieldId; //HACK TESTZZZ

                            return true;
                        }
                        else
                        {
                            
                        }
                    }
                    else //No tokens found
                    {
                        tokens[0] = token;
                        this.color = token.GetColor();
                        return true;
                    }
                return false;

            //}
            
        }

        public void RemoveToken()
        {
            //MusicHandler.DeathSound();

            this.color = GameColor.None;
            for (int i = 0; i < (this.tokens.Length - 1); i++)
            {
                this.tokens[i] = null;
            }
        }

        private void KillToken(Token token)
        {
            RemoveToken();

            token.TokenPosition = token.StartPosition;
            token.TokenState = TokenState.Home;
        }

        //---------------- Getters ----------------
        
        //Gets the color of the field
        public GameColor GetFieldColor()
        {
            return this.color;
        }

        //Gets the id of the field
        public int GetFieldId()
        {
            return this.fieldId;
        }

        //HACK Not The Best I Think
        //Checks if there is a token on the field
        public bool IsTokenPlaced()
        {
            switch(color)
            {
                case (GameColor.None):
                    return false;
                default:
                    return true;
            }
        }
    }
}
