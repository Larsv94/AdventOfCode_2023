namespace AdventOfCode_2023.Console.Config;
public abstract class BaseSolution
{
    protected readonly string[] _input;

    public BaseSolution(string[] input)
    {
        _input = input;
    }

    public abstract int Day { get; }
    public abstract ValueTask<Answer> SolvePart1();
    public abstract ValueTask<Answer> SolvePart2();

}
