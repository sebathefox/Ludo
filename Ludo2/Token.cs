 using System;
using System.Linq;

namespace Ludo2
{
    public enum TokenState { Home, InPlay, Safe };

    public class Token
    {
        private readonly int tokenId;
        private GameColor color;
        private TokenState state;

        //Constructor
        public Token(GameColor clr, int tokenId)
        {
            this.color = clr;
            this.tokenId = tokenId;
            this.state = TokenState.Home;
        }

        //Getter
        public int GetTokenId()
        {
            return this.tokenId;
        }

        //Getter
        public GameColor GetColor()
        {
            return this.color;
        }

        public TokenState GetState()
        {
            return this.state;
        }
    }
}
