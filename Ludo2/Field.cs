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
            if(tokens.Any()) //checks if there is any tokens on the field
            {
                if(token.GetColor() != this.color) //REWRITE Tokens will currently always return false when trying to move
                {
                    //TODO Make the Kill function to send the enemy token home

                    if(tokens.Length > 1)
                    {
                        KillToken(token); //Kills the token that moved because there was more than 1 enemy token
                        return false;
                    }
                    else if(tokens.Length <= 1)
                    {
                        KillToken(this.tokens[1]); //Kills the already placed token

                        tokens[0] = token;
                        this.color = token.GetColor();

                        token.TokenPosition = fieldId + dieRoll;

                        return true;
                    }
                    return false;
                }
                else //No tokens found
                {
                    tokens[1] = token; //Insert the token into the array
                    return true;
                }
            }
            else //No tokens found
            {
                tokens[0] = token;
                this.color = token.GetColor();
                return true;
            }
        }

        public void RemoveToken()
        {
            MusicHandler.DeathSound();

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
