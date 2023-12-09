using AdventOfCode_2023.Console.Day9;

namespace AdventOfCode_2023.Tests.Day9;
public class Day9SolutionTests
{
    public Func<int, string[]> GetTestInput = """
        0 3 6 9 12 15
        1 3 6 10 15 21
        10 13 16 21 30 45
        """.ToTestFunc();

    [Fact]
    public void SolvePart1()
    {
        var solution = new Day9Solution(GetTestInput);
        var result = solution.SolvePart1();

        result.Should().BeInt(114);
    }

    [Fact]
    public void SolvePart2()
    {
        var solution = new Day9Solution(GetTestInput);
        var result = solution.SolvePart2();

        result.Should().BeInt(2);
    }

}
