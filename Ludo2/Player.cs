using System;

namespace Ludo2
{
    public class Player
    {
        private readonly Token[] tokens; //A array with the tokens the player uses in the game

        /// <summary>
        /// Creates a new Player object that can be used in the game
        /// </summary>
        public Player(string name, int playerId, Token[] tokens)
        {
            this.Name = name;
            this.Id = playerId;
            this.tokens = tokens;
        }

        #region Properties/GetterMethods

        /// <summary>
        /// Gets the id of the player object
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Gets the name of the player object
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the color of the player object
        /// </summary>
        public GameColor Color { get => this.tokens[0].Color; }
        
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
