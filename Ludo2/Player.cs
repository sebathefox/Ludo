using System;
using System.Linq;

namespace Ludo2
{
    public class Player
    {
        private readonly string name;
        private readonly int playerId;
        private GameColor playerColor;

        public Player(string playerName, int plrId, GameColor playerColor)
        {
            this.name = playerName;
            this.playerId = plrId;
            this.playerColor = playerColor;
        }

        public string GetName
        {
            get
            {
                return this.name;
            }
        }

        public GameColor GetColor
        {
            get
            {
                return this.playerColor;
            }
        }
    }
}
