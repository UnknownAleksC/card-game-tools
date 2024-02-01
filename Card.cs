using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace card_game_tools
{
    internal class Card
    {
        public string Suit { get; private set; }
        public int Value { get; private set; }

        public Card(string cardSuit, int cardVal)
        {
            Suit = cardSuit;
            Value = cardVal;
        }
    }
}
