namespace casino_game_tools
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CardPack pack = new CardPack(4);
            pack.Shuffle();
        }
    }
}
