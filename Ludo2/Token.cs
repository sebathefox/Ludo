 using System;
using System.Linq;

namespace Ludo2
{
    public class Token
    {
        private GameColor color;

        private int numberOfTokens = 4; //Needed Later
        private readonly int tokenId;
        private readonly int playerId;
        private Token[] tokens;

        //Constructor
        public Token(GameColor clr, int tokenId, int playerId)
        {
            this.color = clr;
            this.tokenId = tokenId;
            this.playerId = playerId;
        }

        //Getter
        public string GetToken()
        {
                return this.tokens.ToString();
        }

        //Getter
        public string GetTokenId()
        {
                return this.tokenId.ToString();
        }

        //Getter
        public int GetPlayerId()
        {
            return this.playerId;
        }


        //Getter
        public GameColor GetColor()
        {
            return this.color;
        }
    }
}
