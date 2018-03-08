using System;
using System.Linq;

namespace Ludo2
{
    public class Player
    {
        #region Fields

        private readonly string name; //The name of the player
        private readonly int playerId; //A unique id to this player only
        private readonly GameColor color; // the color of the player

        private readonly Token[] tokens; //A array with the tokens the player uses in the game

        #endregion

        /// <summary>
        /// Creates a new Player object that can be used in the game
        /// </summary>
        public Player(string name, int playerId, Token[] tokens)
        {
            this.name = name;
            this.playerId = playerId;
            this.tokens = tokens;
            this.color = this.tokens[0].GetColor();
        }

        #region Properties/GetterMethods

        /// <summary>
        /// Gets the id of the player object
        /// </summary>
        public int GetId() => this.playerId;

        /// <summary>
        /// Gets the name of the player object
        /// </summary>
        public string GetName() => this.name;

        /// <summary>
        /// Gets the color of the player object
        /// </summary>
        public GameColor GetColor() => this.color;
        
        /// <summary>
        /// Gets the array with the players tokens
        /// </summary>
        public Token[] GetTokens() => this.tokens;

        /// <summary>
        /// Gets a single array from the token array
        /// </summary>
        public Token GetToken(int tknid) => this.tokens[tknid];

        #endregion
    }
}
