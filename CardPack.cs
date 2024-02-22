using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casino_game_tools
{
    internal class CardPack
    {
        private int _topCard;
        private Random _shuffler;
        private List<Card> _cards;
        public CardPack(int n)
        {
            _shuffler = new Random();
            _cards = new List<Card>();
            CreateDeck(n);
        }

        private void CreateDeck(int numOfDecks)
        {
            int i = 0;
            while (i < numOfDecks)
            {
                int maxCardVal = 13;
                string[] cSuits = { "Heart", "Diamond", "Club", "Spade" };
                for (int cVal = 1; cVal <= maxCardVal; cVal++)
                {
                    foreach (string suit in cSuits)
                    {
                        _cards.Add(new Card(suit, cVal));
                    }
                }
                i++;
            }
        }

        public void Shuffle()
        {
            for (int i = 0; i < _cards.Count*4; i++)
            {
                int card1 = _shuffler.Next(0, _cards.Count);
                int card2 = _shuffler.Next(0, _cards.Count);
                (_cards[card1], _cards[card2]) = (_cards[card2], _cards[card1]);
            }

            _topCard = 0;
        }

        public Card DrawCard()
        {
            if (_topCard == _cards.Count) Shuffle();
            int n = _topCard;
            _topCard++;
            return _cards[n];
        }
    }
}
