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
        public List<Card> Hand { get; private set; }

        public Player()
        {
            Name = "Bob";
            Hand = new List<Card>();
        }

        public void DrawCard(Card card)
        {
            Hand.Add(card);
        }
    }
}
