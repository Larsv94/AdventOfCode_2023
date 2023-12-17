using AdventOfCode_2023.Console.Day13;

namespace AdventOfCode_2023.Tests.Day13;
public class Day13SolutionTests
{

    [Fact]
    public void Test_SolvePart1()
    {
        var getInput = """
            #.##..##.
            ..#.##.#.
            ##......#
            ##......#
            ..#.##.#.
            ..##..##.
            #.#.##.#.

            #...##..#
            #....#..#
            ..##..###
            #####.##.
            #####.##.
            ..##..###
            #....#..#
            """.ToTestFunc();
        var day13Solution = new Day13Solution(getInput);
        var answer = day13Solution.SolvePart1();
        answer.Should().BeInt(405);
    }

    [Fact]
    public void Test_SolvePart2()
    {
        var getInput = """
            #.##..##.
            ..#.##.#.
            ##......#
            ##......#
            ..#.##.#.
            ..##..##.
            #.#.##.#.

            #...##..#
            #....#..#
            ..##..###
            #####.##.
            #####.##.
            ..##..###
            #....#..#
            """.ToTestFunc();
        var day13Solution = new Day13Solution(getInput);
        var answer = day13Solution.SolvePart2();
        answer.Should().BeInt(400);
    }
}
