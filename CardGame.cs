using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

enum GameState
{
    Playing = 0,
    Lost = 1,
    Won = 2
}

namespace casino_game_tools
{
    internal class CardGame
    {
        private GameState _gameStatus;
        private bool _isActive;
        private CardPack _deck;
        private int _dealerScore;
        private int _playerScore;
        private int _playerBet;
        private Dealer _dealer;
        private Player _player;

        public CardGame()
        {
            _gameStatus = GameState.Playing;
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

            if (_playerScore == 21)
            {
                Console.Clear();
                Console.WriteLine("You hit BlackJack, congratulations!");
                Console.WriteLine($"You won {bjOdds}");
                _player.Balance += bjOdds;
                playersTurn = false;
            }

            //Players turn
            while (playersTurn)
            {
                Console.WriteLine();
                Console.WriteLine("Please select your next action");
                string command = Console.ReadLine();
                if (command.ToLower() == "hit")
                {
                    //Player hits
                    PlayerHit();
                } else if (command.ToLower() == "double")
                {
                    //Players doubles and hits
                    PlayerDouble();
                    playersTurn = false;
                }
                else if (command.ToLower() == "stand")
                {
                    //Player stands
                    playersTurn = false;
                }
                else
                {
                    Console.WriteLine("Invalid command");
                }

                if (_playerScore > 21)
                {
                    PlayerLost();
                    playersTurn = false;
                    _gameStatus = GameState.Lost;
                }
            }

            //Dealers turn


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
            Console.WriteLine($"Sorry, you lost {_playerBet}$! Better luck next time.");
            _player.Balance -= _playerBet;
        }
    }
}
