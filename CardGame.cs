using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace casino_game_tools
{
    internal class CardGame
    {
        private bool _isActive;
        private CardPack _deck;
        private int _dealerScore;
        private int _playerScore;
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
                _isActive = false;
            }
        }

        public void Round()
        {
            //Inserting bet
            Console.WriteLine("Please insert bet:");
            int playerBet = Int32.Parse(Console.ReadLine());
            int bjOdds = playerBet + (playerBet * 3 / 2);

            //Dealing cards
            DealCards();

            //Check cards
            GetScore(_player);
            GetScore(_dealer);

            if (_playerScore == 21)
            {
                Console.Clear();
                Console.WriteLine($"You won {bjOdds}");
                _player.Balance += bjOdds;
            }

            Console.WriteLine("What do you want to do now?");

            //Hit, Double, Split or Stand
            PlayerHit();
            playerBet = PlayerDouble(playerBet);
            PlayerStand();

            //
        }

        public void PlayerHit()
        {
            _player.AddCard(_deck.DrawCard());
            GetScore(_player);
        }

        public int PlayerDouble(int playerBet)
        {
            if (playerBet * 2 <= _player.Balance)
            {
                PlayerHit();
                return playerBet * 2;
            }

            Console.Clear();
            Console.WriteLine("You can't double this bet!");
            return playerBet;
        }

        private void DealCards()
        {
            int i = 0;
            while (i < 2)
            {
                _player.AddCard(_deck.DrawCard());
                _dealer.AddCard(_deck.DrawCard());
                i++;
            }
        }

        private void GetScore(Player player)
        {
            bool hasAce = false;
            int sum = 0;
            foreach (Card c in player.Hand)
            {
                if (hasAce && c.Value == CValue.Ace)
                    sum++;
                else sum += (int)c.Value;
                if (c.Value == CValue.Ace) hasAce = true;
            }

            _playerScore = sum;
        }
    }
}
