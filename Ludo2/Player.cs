using System;
using System.Linq;

namespace Ludo2
{
    public class Player
    {
        private readonly string name;
        private readonly int playerId;
        private readonly Token[] tokens;
        private readonly GameColor color;
        private readonly int startpoint; //Where to begin from in the field array W.I.P

        //---------------- Constructor ----------------
        public Player(string playerName, int plrId, Token[] tokens, int sp)
        {
            this.name = playerName;
            this.playerId = plrId;
            this.tokens = tokens;
            this.color = this.tokens[0].GetColor();
            this.startpoint = sp;
        }

        //---------------- Getters ----------------

        public int GetId()
        {
            return this.playerId;
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
        public Token GetToken(int tknid)
        {
            return this.tokens[tknid];
        }

        public int GetStartpoint()
        {
            return this.startpoint;
        }
    }
}
