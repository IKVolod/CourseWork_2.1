namespace CourseWork;

internal class Players
{
    private Player[] _players;

    public int Length
    {
        get { return _players.Length; }
    }

    public Player this[int index]
    {
        get { return _players[index]; }
        set { _players[index] = value; }
    }

    public Players()
    {
        //введення к-сті гравців
        Console.WriteLine("Введіть к-сть гравців");
        
        //створення масиву гравців
        _players = new Player[Method.GetCorrectPositiveIntNum()];
        
        //записуєм гравців
        for (int i = 0; i < _players.Length; i++)
        {
            _players[i] = new Player();
            Console.WriteLine($"\nХто гратиме за гравця №{i + 1}:\n1 - Гравець\n2 - Комп'ютер");
            if (Method.ReturnOneOrTwo() == 1)
            {
                Console.WriteLine("Обрано \"Гравця\"");
                _players[i].WhoPlays = "player";
                Console.WriteLine("Введіть ваше ім'я");
                _players[i].Name = Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Обрано \"Комп'ютер\"");
                _players[i].WhoPlays = "pc";
            }
        }
    }
}