using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomDataGenerator.FieldOptions;
using RandomDataGenerator.Randomizers;

namespace casino_game_tools
{
    internal class CardPack
    {
        private int _topCard;
        private FieldOptionsInteger _shuffler;
        private List<Card> _cards;
        public CardPack(int deckAmount)
        {
            _shuffler = new FieldOptionsInteger();
            _shuffler.Max = 52 * deckAmount;
            _shuffler.Min = 0;
            _cards = new List<Card>();
            CreateDeck(deckAmount);
        }

        private void CreateDeck(int numOfDecks)
        {
            int i = 0;
            while (i < numOfDecks)
            {
                foreach (CValue val in (CValue[]) Enum.GetValues(typeof(CValue)))
                {
                    foreach (CSuit suit in (CSuit[])Enum.GetValues(typeof(CSuit)))
                    {
                        _cards.Add(new Card(suit, val));
                    }
                }
                i++;
            }

            Shuffle();
        }

        public void Shuffle()
        {
            for (int i = 0; i < _cards.Count*4; i++)
            {
                var randomizer = RandomizerFactory.GetRandomizer(_shuffler);
                int? card1 = randomizer.Generate();
                int? card2 = randomizer.Generate();
                (_cards[(int)card1], _cards[(int)card2]) = (_cards[(int)card2], _cards[(int)card1]);
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
