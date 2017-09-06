using System;
using System.Linq;

namespace Ludo2
{
    public enum FieldType { Home, Safe, InPlay, Finish};

    public class Field
    {
        private GameColor color;
        private int fieldId;
        private Token[] tokens = new Token[2];

        public Field(int id, GameColor clr)
        {
            this.fieldId = id;
            this.color = clr;
        }

       /* public bool PlaceToken(Token tkn)
        {
            if(tokens.Any())
            {
                //CODE

            } else
            {
                //No tokens
                tokens[0] = tkn;
            }
        }*/
    }
}
