namespace CourseWork;

internal class Dice
{
    private int _number;
    private bool _isUsed;

    public int Number
    {
        get { return _number; }
        set { _number = value; }
    }

    public bool IsUsed
    {
        get { return _isUsed; }
        set { _isUsed = value; }
    }
}