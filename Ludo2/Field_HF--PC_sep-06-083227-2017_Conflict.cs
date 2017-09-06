using System;
using System.Linq;

namespace Ludo2
{
    public enum FieldType { Home, Safe, InPlay, Finish};

    public class Field
    {
        private int fieldId;
        private FieldType fieldType; //TODO

        //---------------- Constructor ----------------
        public Field(int id)
        {
            this.fieldId = id;
            this.fieldType = FieldType.Home;
        }

        public int GetId()
        {
            return this.fieldId;
        }


        //---------------- Snippet from teacher ----------------
        /*public bool PlaceToken(Token tkn)
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
