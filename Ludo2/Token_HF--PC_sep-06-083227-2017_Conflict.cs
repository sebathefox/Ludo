using System;
using System.Linq;

namespace Ludo2
{
    public class Token
    {
        private GameColor color;
        private FieldType enumPosition; //TODO

        private readonly int tokenId;
        private readonly int playerId;

        //---------------- Constructor ----------------
        public Token(GameColor clr, int tokenId, int playerId)
        {
            this.color = clr;
            this.tokenId = tokenId;
            this.playerId = playerId;
            enumPosition = FieldType.Home;
        }

        //----------------Getter ----------------
        public string GetTokenId
        {
            get
            {
                return this.tokenId.ToString();
            }
        }

        //---------------- Getter ----------------
        public int GetPlayerId
        {
            get { return this.playerId; }
        }


        //Getter
        public GameColor GetColor()
        {
            return this.color;
        }
    }
}
