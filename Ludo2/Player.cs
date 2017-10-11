using System;
using System.Linq;

namespace Ludo2
{
    public class Player
    {
        private readonly string name;
        private readonly int playerId;
        private readonly Token[] tokens;
        private GameColor color;

        public Player(string playerName, int plrId, Token[] tokens)
        {
            this.name = playerName;
            this.playerId = plrId;
            this.tokens = tokens;
            this.color = this.tokens[0].GetColor();
        }

        public string GetName()
        {
            return this.name;
        }

        public GameColor GetColor()
        {
                return this.color;
        }

        public Token[] GetTokens()
        {
            return this.tokens;
        }
    }
}
