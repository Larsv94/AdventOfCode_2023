namespace AdventOfCode_2023.Console.Config;
public abstract class BaseSolution
{
    protected readonly string[] _input;

    public BaseSolution(Func<int, string[]> getInput)
    {
        _input = getInput(Day);
    }

    public abstract int Day { get; }
    public abstract ValueTask<Answer> SolvePart1();
    public abstract ValueTask<Answer> SolvePart2();

}
