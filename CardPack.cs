using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace card_game_tools
{
    internal class CardPack
    {
        public List<Card> Cards { get; private set; }
        public CardPack()
        {
            Cards = new List<Card>();
            CreateDeck();
        }

        public void CreateDeck()
        {
            int i = 0;
            int maxCardVal = 13;
            string[] cSuits = { "Heart", "Diamond", "Club", "Spade" };
            for (int cVal = 1; cVal <= maxCardVal; cVal++)
            {
                foreach (string suit in cSuits)
                {
                    Cards.Add(new Card(suit,cVal));
                }
            }
        }
    }
}
