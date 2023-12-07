using AdventOfCode_2023.Console.Day6;
using FluentAssertions;

namespace AdventOfCode_2023.Tests.Day6;
public class Day6SolutionTests
{
    [Fact]
    public async Task SolvePart1()
    {
        var getInput = """
            Time:      7  15   30
            Distance:  9  40  200
            """.ToTestFunc();

        var solution = new Day6Solution(getInput);
        var result = await solution.SolvePart1();

        result.Should().BeString(288.ToString());
    }

    [Fact]
    public async Task SolvePart2()
    {
        var getInput = """
            Time:      7  15   30
            Distance:  9  40  200
            """.ToTestFunc();

        var solution = new Day6Solution(getInput);
        var result = await solution.SolvePart2();

        result.Should().BeString(71503.ToString());
    }
}
