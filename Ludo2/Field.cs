using System;
using System.Linq;
using System.Collections.Generic;

namespace Ludo2
{

    public class Field
    {
        #region Fields
        
        private readonly int fieldId; //Every field needs an id
        private List<Token> tokensList = new List<Token>(); //creates an List to hold up to four tokens at the same time

        #endregion

        //---------------- Constructor ----------------
        public Field(int fieldId, GameColor color = GameColor.White)
        {
            this.fieldId = fieldId;
            this.Color = color;
        }

        #region Properties/GetterMethods

        /// <summary>
        /// Gets the color of the field
        /// </summary>
        public GameColor Color { get; set; }

        /// <summary>
        /// Gets the id of the field
        /// </summary>
        /// <returns></returns>
        public int GetFieldId() => this.fieldId;

        /// <summary>
        /// Gets a List with the tokens currently positioned at this field
        /// </summary>
        public List<Token> TokensOnField { get => tokensList; set => tokensList = value; }

        #endregion

        public override string ToString()
        {
            return "FieldId: " + GetFieldId() + ", FieldColor: " + Color;
        }
    }
}