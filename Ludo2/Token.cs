using System;
using System.Linq;

namespace Ludo2
{
    public enum TokenState { Home, InPlay, Safe, Finished }; //Used to define if the token is on the main gameboard

    public class Token
    {
        private readonly int tokenId; // the unique id of the token
        private readonly GameColor color; //The color of the token
        private TokenState state; //Defines if the token is Home, safe or in play
        private int positionId; //defines the position of the tokens
        private readonly int startPosition;
        private int positionCounter = 0;
        private bool canMove;

        //---------------- Constructor ----------------
        public Token(int tokenId, GameColor color, int startPos, int SafePos)
        {
            this.tokenId = tokenId;
            this.color = color;
            this.state = TokenState.Home; //sets the default state to 'Home'
            this.startPosition = this.positionId =  startPos;
            this.canMove = false;
        }

        #region Properties/GetterMethods

        //Gets the id of the currently selected token
        public int GetTokenId() => this.tokenId;

        //Gets the color of the currently selected token
        public GameColor GetColor() => this.color;

        public TokenState TokenState { get => state; set => state = value; }

        public int TokenPosition { get => positionId; set => positionId = value; }

        public int StartPosition { get => startPosition; }

        public int Counter { get => this.positionCounter; set => this.positionCounter = value; }

        public bool CanMove { get => this.canMove; set => this.canMove = value; }

        #endregion

        public override string ToString()
        {
            return "PieceId: " + GetTokenId() + ", Color: " + GetColor();
        }
    }
}
