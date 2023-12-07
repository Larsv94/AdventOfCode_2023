namespace AdventOfCode_2023.Console.Config;
public interface ISolution
{
    int Day { get; }

    Answer SolvePart1(string[] lines);
    Answer SolvePart2(string[] lines);
}
