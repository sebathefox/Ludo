using System;
using System.Linq;

namespace Ludo2
{
    public enum TokenState { Home, InPlay, Safe }; //Used to define if the token is on the main gameboard

    public class Token
    {
        private readonly int tokenId; // the unique id of the token
        private readonly GameColor color; //The color of the token
        private TokenState state; //Defines if the token is Home, safe or in play
        private int positionId = 0; //defines the position of the tokens //FIX needs better definition...
        private readonly int startPosition;

        //---------------- Constructor ----------------
        public Token(int tokenId, GameColor color, int startPosition)
        {
            //Self explanatory
            this.tokenId = tokenId;
            this.color = color;
            this.state = TokenState.Home; //sets the default state to 'Home'
            this.startPosition = startPosition;
        }

        //---------------- Getters ----------------

        //Gets the id of the currently selected token
        public int GetTokenId()
        {
            return this.tokenId;
        }

        //Gets the color of the currently selected token
        public GameColor GetColor()
        {
            return this.color;
        }

        //Gets the state of the currently selected token
        public TokenState GetState()
        {
            return this.state;
        }

        //Sets the state of the currently selected token
        public void SetState(TokenState ts)
        {
            this.state = ts;
        }

        //Gets the position of the currently selected token
        public int GetPosition()
        {
            return positionId;
        }

        //Sets the position of the currently selected token
        public void SetPosition(int pos)
        {
            this.positionId = pos;
        }

        public int getStartposition()
        {
            return startPosition;
        }
    }
}
