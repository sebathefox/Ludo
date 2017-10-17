 using System;
using System.Linq;

namespace Ludo2
{
    public enum TokenState { Home, InPlay, Safe };

    public class Token
    {
        private readonly int tokenId;
        private readonly GameColor color;
        private TokenState state;
        private int positionId = 0;

        //---------------- Constructor ----------------
        public Token(int id, GameColor clr)
        {
            this.tokenId = id;
            this.color = clr;
            this.state = TokenState.Home;
        }

        //---------------- Getters ----------------

        public int GetTokenId()
        {
            return this.tokenId;
        }

        public GameColor GetColor()
        {
            return this.color;
        }

        public TokenState GetState()
        {
            return this.state;
        }

        public void SetState(TokenState ts)
        {
            this.state = ts;
        }

        public void SetPosition(int pos)
        {
            this.positionId = pos;
        }

        public int GetPosition()
        {
            return positionId;
        }
    }
}
