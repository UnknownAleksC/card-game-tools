using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace casino_game_tools
{
    enum CardSystems
    {
        HiLo,
        HiOpt1,
        HiOpt2,
        Ko,
        Omega2,
        Red7,
        ZenCount,
        TenCount
    }
    internal static class CardCounter
    {
        private static int _count;
        private static int[] _cardVals;
        static CardCounter()
        {
            _count = 0;
            _cardVals = GetCardValues(CardSystems.HiLo);
        }

        public static void UpdateCount(int cVal)
        {
            _count += _cardVals[cVal];
        }

        public static void ResetCount()
        {
            _count = 0;
        }

        public static int GetCount()
        {
            return _count;
        }

        private static int[] GetCardValues(CardSystems i)
        {
            var cardSystems = new Dictionary<CardSystems, int[]> {
                { CardSystems.HiLo, new int[] {
                    0,-1,1,1,1,1,1,0,0,0,-1
                }},
                { CardSystems.HiOpt1, new int[] {
                    0,0,0,1,1,1,1,0,0,0,-1
                }},
                { CardSystems.HiOpt2, new int[] {
                    0,0,1,1,2,2,1,1,0,0,-2
                }},
                { CardSystems.Ko, new int[] {
                    0,-1,1,1,1,1,1,1,0,0,-1
                }},
                { CardSystems.Omega2, new int[] {
                    0,0,1,1,2,2,2,1,0,-1,-2
                }},
                { CardSystems.Red7, new int[] {
                    0,-1,1,1,1,1,1,1,0,0,-1
                }},
                { CardSystems.ZenCount, new int[] {
                    0,-1,1,1,2,2,2,1,0,0,-2
                }},
                { CardSystems.TenCount, new int[] {
                    0,1,1,1,1,1,1,1,1,1,-2
                }}
            };
            return cardSystems[i];
        }
    }
}
