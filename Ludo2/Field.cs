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

       public bool PlaceToken(Token tkn, GameColor color)
        {
            if(tokens.Any())
            {
                //CODE
                if(tkn.GetColor() == color)
                {
                    //TODO
                    return true;
                }
                else
                {
                    tokens[0] = tkn;
                    return false;
                }



                
            } else
            {
                tokens[0] = tkn;
                return false;
            }
        }
    }
}
