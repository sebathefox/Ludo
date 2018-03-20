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

        //Places the token on the field
        public bool PlaceToken(Token token, GameColor color)
        {
            if (tokensList.Count > 1)
            {
                if (token.Color != this.Color)
                {
                    KillToken(token); //Kills the token that moved because there was more than 1 enemy token
                    return false;
                }
                else
                {
                    tokensList.Add(token);
                    return true;
                }
            }
            else if (tokensList.Count == 0)
            {
                if (token.Color != this.Color && this.Color != GameColor.White)
                {
                    KillToken(this.tokensList.ElementAt(0)); //Kills the already placed token
                    RemoveToken();

                    PlToken(token);
                    return true;
                }
            }
            PlToken(token);
            return true;
        }

        public void PlToken(Token token)
        {
            tokensList.Add(token);
            this.Color = token.Color;
            token.Position = this.fieldId;
        }

        public void RemoveToken()
        {
            this.Color = GameColor.White;
            for (int i = 0; i < (this.tokensList.Count); i++)
            {
                this.tokensList.RemoveAt(i);
            }
        }

        public void KillToken(Token token)
        {
            MusicHandler.DeathSound();

            token.Position = token.StartPosition;
            token.State = TokenState.Home;
            token.Counter = 0;
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