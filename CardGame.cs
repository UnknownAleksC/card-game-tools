using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casino_game_tools
{
    internal class CardGame
    {
        private bool _isActive;
        private CardPack _deck;
        private Dealer _dealer;
        private Player _player;

        public CardGame()
        {
            _dealer = new Dealer();
            _player = new Player();
            _deck = new CardPack(4);
            Game();
        }

        public void Game()
        {
            _isActive = true;
            while (_isActive)
            {
                Round();
            }
        }

        public void Round()
        {
            //Dealing cards
            DealCards();
            //Check cards
            //Hit, Double or Stand
            //
        }

        private void DealCards()
        {
            while (_dealer.Hand.Count < 2 && _player.Hand.Count < 2)
            {
                _player.AddCard(_deck.DrawCard());
                _dealer.AddCard(_deck.DrawCard());
            }
        }
    }
}
