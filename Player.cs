using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casino_game_tools
{
    internal class Player
    {
        public string Name;
        public int Balance;
        public List<Card> Hand { get; private set; }

        public Player()
        {
            Name = "Player(You)";
            Hand = new List<Card>();
            Balance = 1000000;
        }

        public void AddCard(Card card)
        {
            Hand.Add(card);
        }
    }
}
