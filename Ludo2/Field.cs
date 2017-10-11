using System;
using System.Linq;

namespace Ludo2
{

    public class Field
    {
        private GameColor color;
        private int fieldId;
        private Token[] tokens = new Token[2];

        //---------------- Constructor ----------------
        public Field(int id, GameColor clr)
        {
            this.fieldId = id;
            this.color = clr;
        }

       public bool PlaceToken(Token tkn, GameColor color)
        {
            if(tokens.Any())
            {
                if(tkn.GetColor() == color)
                {
                    this.color = color;
                    //TODO
                    return true;
                }
                else
                {
                    tokens[0] = tkn;
                    return false;
                }
            }
            else
            {
                tokens[0] = tkn;
                return false;
            }
        }

        //---------------- Getters ----------------
        
        public GameColor GetFieldColor()
        {
            return this.color;
        }

        public int GetFieldId()
        {
            return this.fieldId;
        }
    }
}
