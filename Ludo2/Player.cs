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

        /// <summary>
        /// Creates a new Player object that can be used in the game
        /// </summary>
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
                tkn.TokenPosition = x; //Sets the position of each of the tokens
            }
        }
        //---------------- Getters ----------------

        /// <summary>
        /// Gets the id of the player object
        /// </summary>
        public int GetId() 
        {
            return this.playerId;
        }

        /// <summary>
        /// Gets the name of the player object
        /// </summary>
        public string GetName()
        {
            return this.name;
        }

        /// <summary>
        /// Gets the color of the player object
        /// </summary>
        public GameColor GetColor()
        {
                return this.color;
        }

        /// <summary>
        /// Gets the array with the players tokens
        /// </summary>
        public Token[] GetTokens()
        {
            return this.tokens;
        }

        /// <summary>
        /// Gets a single array from the token array
        /// </summary>
        public Token GetToken(int tknid)
        {
            return this.tokens[tknid];
        }

        /// <summary>
        /// Gets the startpoint Do NOT Work~
        /// </summary>
        public int GetStartpoint()
        {
            return this.startPoint;
        }
    }
}
