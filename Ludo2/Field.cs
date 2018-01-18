using System;
using System.Linq;
using System.Collections.Generic;

namespace Ludo2
{

    public class Field
    {
        private GameColor color; //used if there is a token on the field
        private readonly int fieldId; //Every field needs an id
        private List<Token> tokensList = new List<Token>(); //creates an List to hold up to four tokens at the same time




        //---------------- Constructor ----------------
        public Field(int fieldId, GameColor color = GameColor.None)
        {
            //Self explanatory
            this.fieldId = fieldId;
            this.color = color;
        }

        //Places the token on the field
        public bool PlaceToken(Token token, GameColor color, int dieRoll)
        {
            if (tokensList.Count > 1)
            {

                Console.WriteLine("LENGTH: " + tokensList.Count);
                Console.WriteLine("ANY: " + tokensList.Any());
                Console.WriteLine("COUNT: " + tokensList.Count());

                if (token.GetColor() != this.color)
                {

                    KillToken(token); //Kills the token that moved because there was more than 1 enemy token
                    Console.WriteLine("ERROR: Field.cs, LINE: 32?");
                    return false;
                }
                else
                {
                    tokensList.Add(token);

                    Console.WriteLine("ERROR: Field.cs, LINE: 39?");
                    return true;
                }
            }
            else if (tokensList.Count < 0)
            {
                /*if (tokensList.Any())
                {
                    tokensList.Add(token);
                    this.color = token.GetColor();
                    Console.WriteLine("ERROR: Field.cs, LINE: 66?");
                    return true;
                }
                else */if (token.GetColor() != this.color)
                {
                    //Console.WriteLine();

                    KillToken(this.tokensList.ElementAt(0)); //Kills the already placed token

                    tokensList.Add(token);
                    this.color = token.GetColor();
                    tokensList.RemoveAt(0);

                    token.TokenPosition = this.fieldId; //HACK TESTZZZ

                    Console.WriteLine("ERROR: Field.cs, LINE: 54?");
                    return true;
                }
                //else
                //{

                //}
            }
            else //No tokens found
            {
                tokensList.Add(token);
                this.color = token.GetColor();
                Console.WriteLine("ERROR: Field.cs, LINE: 66?");
                return true;
            }
        Console.WriteLine("ERROR: Field.cs, LINE: 69?");
        return false;
        }

        public void RemoveToken()
        {
            this.color = GameColor.None;
            for (int i = 0; i < (this.tokensList.Count - 1); i++)
            {
                this.tokensList[i] = null;
            }
        }

        private void KillToken(Token token)
        {
            RemoveToken();

            //MusicHandler.DeathSound();

            token.TokenPosition = token.StartPosition;
            token.TokenState = TokenState.Home;
        }

        //---------------- Getters ----------------

        //Gets the color of the field
        public GameColor GetFieldColor()
        {
            return this.color;
        }

        //Gets the id of the field
        public int GetFieldId()
        {
            return this.fieldId;
        }
    }
}
