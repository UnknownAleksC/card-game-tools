﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casino_game_tools
{
    enum CSuit
    {
        Heart,
        Diamond,
        Club,
        Spade
    }
    enum CValue
    {
        Ace = 11,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 10,
        Queen = 10,
        King = 10
    }
    internal class Card
    {
        public CSuit Suit { get; private set; }
        public CValue Value { get; private set; }

        public Card(CSuit cardSuit, CValue cardVal)
        {
            Suit = cardSuit;
            Value = cardVal;
        }
    }
}
