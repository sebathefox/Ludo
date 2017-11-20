using System;
using System.Linq;

namespace Ludo2
{
    public class Player
    {
        private readonly string name; //The name of the player
        private readonly int playerId; //A unique id to this player only
        private readonly Token[] tokens; //A array with the tokens the player uses in the game
        private readonly GameColor color; // the color of the player
        private readonly int startPoint; //Where to begin from in the field array

        //---------------- Constructor ----------------
        public Player(string name, int playerId, Token[] tokens, int startPoint)
        {
            //Self explanatory
            this.name = name;
            this.playerId = playerId;
            this.tokens = tokens;
            this.color = this.tokens[0].GetColor();
            this.startPoint = startPoint;
            SetTokenStartpoint(startPoint);
        }

        //Single time use
        private void SetTokenStartpoint(int x)
        {
            foreach(Token tkn in tokens) //Goes through each token in the token array
            {
                tkn.SetPosition(x); //Sets the position of each of the tokens
            }
        }
        //---------------- Getters ----------------

        //Gets the id of the currently selected user
        public int GetId() 
        {
            return this.playerId;
        }

        //Gets the name of the currently selected user
        public string GetName()
        {
            return this.name;
        }

        //Gets the color of the currently selected user
        public GameColor GetColor()
        {
                return this.color;
        }

        //Gets the tokens of the currently selected user
        public Token[] GetTokens()
        {
            return this.tokens;
        }

        //Gets a single token from the currently selected user
        public Token GetToken(int tknid)
        {
            return this.tokens[tknid];
        }

        //Gets the startpoint of the currently selected user
        public int GetStartpoint()
        {
            return this.startPoint;
        }
    }
}
