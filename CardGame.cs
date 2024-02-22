using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casino_game_tools
{
    internal class CardGame
    {
        private CardPack _deck;
        private Dealer _dealer;
        private Player _participant;

        public CardGame()
        {
            _dealer = new Dealer();
            _participant = new Player();
            Game();
        }

        public void Game()
        {
            bool isActive = true;
            while (isActive)
            {
                Round();
            }
        }

        public void Round()
        {
        }

        public void CreateCardPack()
        {
            int deckCount = -1;
            while (deckCount is < 1 or > 5)
            {
                Console.Write("Number of card decks: ");
                deckCount = int.Parse(Console.ReadLine() ?? "-1");
            }
        }
    }
}
