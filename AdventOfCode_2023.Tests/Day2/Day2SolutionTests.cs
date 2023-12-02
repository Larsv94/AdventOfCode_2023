using AdventOfCode_2023.Console.Day2;
using FluentAssertions;

namespace AdventOfCode_2023.Tests.Day2;
public class Day2SolutionTests
{
    private readonly ISolution _solution;

    public Day2SolutionTests()
    {
        _solution = new Day2Solution();
    }

    [Fact]
    public async Task Part1InputTest()
    {
        var input = """
            Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
            Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
            Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
            Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
            """.ToLines();

        var result = await _solution.SolvePart1(input);

        result.Should().BeInt(8);
    }

    [Fact]
    public async Task Part2InputTest()
    {
        var input = """
            Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
            Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
            Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
            Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green
            """.ToLines();

        var result = await _solution.SolvePart2(input);

        result.Should().BeInt(2286);
    }
}
