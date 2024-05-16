namespace CourseWork;

internal class Player
{
    private string _name = "Pc";
    private string _whoPlays;
    private int _points;
    private bool _passport;

    public string Name
    {
        get { return _name; }
        set 
        {
            while (String.IsNullOrWhiteSpace(value))
            {
                Console.WriteLine("Введіть ім'я хоча б з якимись знаками");
                value = Console.ReadLine();
            }

            _name = value;
        }
    }

    public string WhoPlays
    {
        get { return _whoPlays; }
        set { _whoPlays = value; }
    }

    public int Points
    {
        get { return _points; }
        set
        {
            if (_points + value <= 1000) _points += value;
            else
            {
                Console.WriteLine("\nВаша загальна сума очок строго більша тисячі (враховуючи цей раунд)," +
                                  "\nтому очки за цей раунд згорають");
            }
        }
    }

    public bool Passport
    {
        get { return _passport; }
        set { _passport = value; }
    }
}