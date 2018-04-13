using System;
using System.Diagnostics;

namespace Ludo2
{
    public enum TokenState { Home, InPlay, Safe, Finished }; //Used to define if the token is on the main gameboard

    public class Token
    {
        //---------------- Constructor ----------------
        public Token(int tokenId, GameColor color, int startPos)
        {
            this.Id = tokenId;
            this.Color = color;
            this.State = TokenState.Home; //sets the default state to 'Home'
            this.StartPosition = this.Position =  startPos;
            this.CanMove = false;
        }

        public void MoveToken(ref Field[] fields, int dieValue)
        {
            ref Field currentField = ref fields[this.Position];

            if (this.Counter + dieValue > 56)
            {
                currentField.TokensOnField.Remove(this);
                currentField.Color = GameColor.White; //Clears the currentField

                UpdateTokenMovement(0, TokenState.Finished);

                return;
            }

            
            ref Field fieldToMove = ref fields[this.Position + dieValue]; //Field to move token to

            
            currentField.TokensOnField.Remove(this);
            currentField.Color = GameColor.White; //Clears the currentField


            if (this.Position + dieValue > 51 && this.State != TokenState.Safe)
            {
                this.Position -= 52;
                
            }

            if (this.State == TokenState.Home)
            {
                UpdateTokenMovement(0); //Sets the token on the board
                fieldToMove.TokensOnField.Add(this);
                fieldToMove.Color = this.Color;
            }
            else
            {

                if (fieldToMove.TokensOnField.Count > 0)
                {
                    //Token(s) on field further validate
                    if (fieldToMove.Color != this.Color && fieldToMove.TokensOnField.Count > 1)
                    {
                        ResetToken(this);
                    }
                    else if (fieldToMove.Color == this.Color)
                    {
                        UpdateTokenMovement(dieValue);
                        fieldToMove.TokensOnField.Add(this);
                        fieldToMove.Color = this.Color;

                    }
                }
                //TODO Move
                UpdateTokenMovement(dieValue);
                fieldToMove.TokensOnField.Add(this);
                fieldToMove.Color = this.Color;
            }
        }

        private void UpdateTokenMovement(int dieValue, TokenState state = TokenState.InPlay)
        {
            this.Position += dieValue;
            this.Counter += dieValue;
            this.State = state;
            Debug.WriteLine(this.Counter);
        }

        private void ResetToken(Token token)
        {
            MusicHandler.DeathSound();

            token.Position = token.StartPosition;
            token.State = TokenState.Home;
            token.Counter = 0;
        }

        #region Properties

        //Gets the id of the currently selected token
        public int Id { get; private set; }

        /// <summary>
        /// Gets the current color returned in GameColor (ReadOnly)
        /// </summary>
        public GameColor Color { get; private set; }

        /// <summary>
        /// Gets the current state returned in TokenState
        /// </summary>
        public TokenState State { get; private set; }

        /// <summary>
        /// Gets the current position
        /// </summary>
        public int Position { get; private set; }

        /// <summary>
        /// Gets the startposition
        /// </summary>
        public int StartPosition { get; private set; }

        /// <summary>
        /// Count up to 52? then moves token to safe
        /// </summary>
        public int Counter { get; private set; }

        /// <summary>
        /// If true the token can move else false
        /// </summary>
        public bool CanMove { get; set; }

        #endregion

        public override string ToString()
        {
            return "PieceId: " + Id + ", Color: " + Color;
        }
    }
}
