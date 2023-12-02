namespace AdventOfCode_2023.Console.Config;
public interface ISolution
{
    int Day { get; }

    ValueTask<Answer> SolvePart1(string[] lines);
    ValueTask<Answer> SolvePart2(string[] lines);
}
