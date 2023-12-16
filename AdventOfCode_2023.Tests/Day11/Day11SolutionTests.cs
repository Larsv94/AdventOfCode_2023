using AdventOfCode_2023.Console.Day11;

namespace AdventOfCode_2023.Tests.Day11;
public class Day11SolutionTests
{
    private readonly Func<int, string[]> getInput = """
        ...#......
        .......#..
        #.........
        ..........
        ......#...
        .#........
        .........#
        ..........
        .......#..
        #...#.....
        """.ToTestFunc();

    [Fact]
    public void SolvePart1()
    {
        var solution = new Day11Solution(getInput);
        var answer = solution.SolvePart1();
        answer.Should().BeString(374.ToString());
    }

}
