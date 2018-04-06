using System;

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
        public TokenState State { get; set; }

        /// <summary>
        /// Gets the current position
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Gets the startposition
        /// </summary>
        public int StartPosition { get; }

        /// <summary>
        /// Count up to 52? then moves token to safe
        /// </summary>
        public int Counter { get; set; }

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
