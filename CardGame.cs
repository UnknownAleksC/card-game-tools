using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

enum GameState
{
    DealerTurn = 0,
    PlayerTurn = 1,
    Lost = 2,
    Won = 3,
    Draw = 4
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
            _gameStatus = GameState.PlayerTurn;
            _dealer = new Dealer();
            _player = new Player();
            _deck = new CardPack(4);

            //Test();
            Game();
        }

        public void Game()
        {
            Introduction();
            _isActive = true;
            while (_isActive)
            {
                Console.Clear();
                Round();
                Interlude();
            }
        }

        public void Introduction()
        {
            Console.Clear();
            Console.WriteLine("Hello and welcome to Blackjack with card counting!");
            Console.Write("To get started, just press Enter.");
            Console.ReadLine();
            Console.Clear();
        }

        public void Round()
        {
            //Inserting bet
            Console.WriteLine($"Your current balance is {_player.Balance}");
            Console.WriteLine("Please insert bet:");
            _playerBet = Int32.Parse(Console.ReadLine());
            int bjOdds = _playerBet + (_playerBet * 3 / 2);

            //Dealing cards
            DealCards();

            //Check hand for 21
            if (_playerScore == 21)
            {
                Console.WriteLine();
                Console.WriteLine("You hit BlackJack, congratulations!");
                Console.WriteLine($"You won {bjOdds}");
                Console.ReadLine();
                _player.Balance += bjOdds;
                _gameStatus = GameState.Won;
            }

            //Players turn
            while (_gameStatus == GameState.PlayerTurn)
            {
                Console.WriteLine();
                Console.WriteLine("Please select your next action");
                string command = Console.ReadLine();

                if (command.ToLower() == "hit")
                {
                    //Player hits
                    Hit(_player);
                }
                else if (command.ToLower() == "double")
                {
                    //Players doubles and hits
                    Double();
                }
                else if (command.ToLower() == "stand")
                {
                    //Player stands
                    Stand();
                }
                else
                {
                    Console.WriteLine("Invalid command");

                }
                if (_playerScore > 21)
                {
                    PlayerLost();
                }
            }

            //Dealers turn
            //Check Dealers hand
            if (_gameStatus == GameState.DealerTurn)
            {
                Console.WriteLine();
                Console.WriteLine("Dealers turn:");
                GetScore(_dealer);
                while (_gameStatus == GameState.DealerTurn)
                {
                    Console.Read();
                    if (_dealerScore > 21)
                    {
                        PlayerWon();
                    } 
                    else if (_dealerScore >= 17)
                    {
                        if (_dealerScore == _playerScore)
                        {
                            PlayerDraw();
                        } 
                        else if (_dealerScore < _playerScore)
                        {
                            PlayerWon();
                        } 
                        else if (_dealerScore > _playerScore)
                        {
                            PlayerLost();
                        }
                    }
                    else
                    {
                        Hit(_dealer);
                    }
                }
            }
        }

        public void Interlude()
        {
            bool answer = true;
            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine($"Your new balance is {_player.Balance}. Want to continue?");
                if (keyInfo.Key == ConsoleKey.UpArrow || keyInfo.Key == ConsoleKey.DownArrow)
                    answer = !answer;
                if (answer)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine("Yes");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("No");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("Yes");
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.Write("No");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                keyInfo = Console.ReadKey();
            }
            _isActive = answer;
        }

        public void Hit(Player player)
        {
            Console.WriteLine();
            Console.WriteLine($"{player.Name} hits");
            player.AddCard(_deck.DrawCard());
            GetScore(player);
        }

        public void Double()
        {
            if (_playerBet * 2 <= _player.Balance && _player.Hand.Count == 2)
            {
                Hit(_player);
                _playerBet *= 2;
                _gameStatus = GameState.DealerTurn;
            }
            else
            {
                Console.WriteLine("You can't double this bet!");
                Console.ReadLine();
            }

        }

        private void DealCards()
        {
            _gameStatus = GameState.PlayerTurn;

            Console.WriteLine();
            int i = 0;
            while (i < 2)
            {
                _player.AddCard(_deck.DrawCard());
                _dealer.AddCard(_deck.DrawCard());
                i++;
            }

            GetScore(_player);
        }

        private void GetScore(Player player)
        {
            bool hasAce = false;
            int sum = 0;
            Console.WriteLine($"{player.Name}'s deck consist of the following cards:");
            foreach (Card c in player.Hand)
            {
                Console.WriteLine(c.Value.ToString());
                if (hasAce && c.Value == CValue.Ace)
                    sum++;
                else if (sum + (int)c.Value > 21 && hasAce ||
                         sum + (int)c.Value > 21 && c.Value == CValue.Ace)
                {
                    sum -= 10 - (int)c.Value;
                }
                else sum += (int)c.Value;
                if (c.Value == CValue.Ace) hasAce = true;
            }

            if (player.Name == "Dealer") _dealerScore = sum;
            else _playerScore = sum;

            Console.WriteLine($"This gives a total value of {sum}");
            Console.WriteLine($"Your card counter is currently at {CardCounter.GetCount()}");
            if (_gameStatus == GameState.PlayerTurn) Console.WriteLine($"Dealers first card is {_dealer.Hand.First().Value}.");
        }

        public void PlayerLost()
        {
            Console.WriteLine();
            Console.WriteLine($"Sorry, you lost {_playerBet}$! Better luck next time.");
            Console.ReadLine();
            _player.Balance -= _playerBet;
            _gameStatus = GameState.Lost;
            Reset();
        }

        public void PlayerWon()
        {
            Console.WriteLine();
            Console.WriteLine($"Congratulations, you won {_playerBet*2}! Keep it up.");
            Console.ReadLine();
            _player.Balance += _playerBet*2;
            _gameStatus = GameState.Won;
            Reset();
        }

        public void PlayerDraw()
        {
            Console.WriteLine();
            Console.WriteLine("Seems your game ended in a draw. Better luck next time!");
            Console.ReadLine();
            _gameStatus = GameState.Draw;
            Reset();
        }

        public void Stand()
        {
            Console.WriteLine();
            Console.WriteLine("Dealers turn");
            Console.ReadLine();
            _gameStatus = GameState.DealerTurn;
        }

        public void Reset()
        {
            _dealerScore = 0;
            _playerScore = 0;
            _player.ClearHand();
            _dealer.ClearHand();
        }

        public void Test()
        {
            GetScore(_player);
        }
    }
}
