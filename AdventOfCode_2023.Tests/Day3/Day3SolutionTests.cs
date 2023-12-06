using AdventOfCode_2023.Console.Day3;
using FluentAssertions;

namespace AdventOfCode_2023.Tests.Day3;
public class Day3SolutionTests
{
    private readonly Day3Solution _solution;

    public Day3SolutionTests()
    {
        var input = """
            467..114..
            ...*......
            ..35..633.
            ......#..*
            617*......
            ....++.58.
            ..592.....
            ......755.
            ...$+*....
            .664.598..
            """.ToTestFunc();
        _solution = new Day3Solution(input);
    }

    [Fact]
    public async Task Part1InputTestAsync()
    {
        var result = await _solution.SolvePart1();

        result.Should().BeInt(4361);
    }

    [Fact]
    public async Task Part2InputTestAsync()
    {
        var result = await _solution.SolvePart2();

        result.Should().BeInt(467835);
    }
}
