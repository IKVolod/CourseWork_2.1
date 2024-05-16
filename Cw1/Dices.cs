namespace CourseWork;

internal class Dices
{
    private Dice[] _dices = new Dice[5];
    private int _rerollCount = 5;
    private List<int> _sum;
    private Dice[] _concreteDiceCounter = new Dice[6];

    public int RerollCount
    {
        get { return _rerollCount; }
        set { _rerollCount = value; }
    }

    public List<int> Sum
    {
        get { return _sum; }
        set { _sum = value; }
    }

    public Dice[] ConcreteDiceCounter
    {
        get { return _concreteDiceCounter; }
    }

    public int Length
    {
        get { return _dices.Length; }
    }

    public Dice this[int index]
    {
        get { return _dices[index]; }
        set { _dices[index] = value; }
    }

    public Dices()
    {
        InitializeDices();
        InitializeConcreteDiceCounter();
    }

    private void InitializeDices()
    {
        for (int i = 0; i < _dices.Length; i++) _dices[i] = new Dice();
    }

    private void InitializeConcreteDiceCounter()
    {
        for (int i = 0; i < 6; i++) _concreteDiceCounter[i] = new Dice();
    }

    public void SetRandomDices(int rerollCount)
    {
        _dices = new Dice[rerollCount];
        InitializeDices();
        for (int i = 0; i < rerollCount; i++) _dices[i].Number = new Random().Next(1, 7);
        ShowDices();
        if (rerollCount == 5) _sum = new List<int>();
    }
    
    private void ShowDices()
    {
        Console.WriteLine("\n\nОсь які кубики випали");
        for (int i = 0; i < _dices.Length; i++) Console.WriteLine($"Кубик №{i + 1}: {_dices[i]}");
    }
}