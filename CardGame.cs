using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

enum PlayerState
{
    Playing = 0,
    Lost = 1,
    Won = 2
}

namespace casino_game_tools
{
    internal class CardGame
    {
        private bool _isActive;
        private CardPack _deck;
        private int _dealerScore;
        private int _playerScore;
        private int _playerBet;
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
            _playerBet = Int32.Parse(Console.ReadLine());
            int bjOdds = _playerBet + (_playerBet * 3 / 2);
            bool playersTurn = true;

            //Dealing cards
            DealCards();

            //Check cards
            GetScore(_player);
            GetScore(_dealer);

            Console.WriteLine("What do you want to do now?");
            if (_playerScore == 21)
            {
                Console.Clear();
                Console.WriteLine("You hit 21, congratulations!");
                Console.WriteLine($"You won {bjOdds}");
                _player.Balance += bjOdds;
                playersTurn = false;
            }

            while (playersTurn)
            {
                string command = Console.ReadLine();
                if (command.ToLower() == "hit")
                {
                    PlayerHit();
                    GetScore(_player);
                } else if (command.ToLower() == "double")
                {
                    PlayerDouble();
                    GetScore(_player);
                } else if (command.ToLower() == "stand")
                {
                    playersTurn = false;
                }

                if (_playerScore > 21)
                {
                    PlayerLost();
                    playersTurn = false;
                }
            }
            //Hit, Double, Split or Stand
            // PlayerHit();
            // playerBet = PlayerDouble(playerBet);
            // PlayerStand();

            //
        }

        public void PlayerHit()
        {
            _player.AddCard(_deck.DrawCard());
            GetScore(_player);
        }

        public int PlayerDouble()
        {
            if (_playerBet * 2 <= _player.Balance)
            {
                PlayerHit();
                return _playerBet * 2;
            }

            Console.Clear();
            Console.WriteLine("You can't double this bet!");
            Console.ReadLine();
            return _playerBet;
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
            Console.WriteLine("Your deck consist of the following cards:");
            foreach (Card c in player.Hand)
            {
                Console.WriteLine(c.Value);
                if (hasAce && c.Value == CValue.Ace)
                    sum++;
                else sum += (int)c.Value;
                if (c.Value == CValue.Ace) hasAce = true;
            }

            _playerScore = sum;
            Console.WriteLine($"This gives a total value of {_playerScore}");
        }

        public void PlayerLost()
        {
            Console.WriteLine("Sorry, you lost! Better luck next time.");
            _player.Balance -= _playerBet;
        }
    }
}
