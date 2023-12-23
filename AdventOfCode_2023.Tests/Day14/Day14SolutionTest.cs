using AdventOfCode_2023.Console.Day14;

namespace AdventOfCode_2023.Tests.Day14;
public class Day14SolutionTest
{
    [Fact]
    public void Test_SolvePart1()
    {
        var getInput = """
            O....#....
            O.OO#....#
            .....##...
            OO.#O....O
            .O.....O#.
            O.#..O.#.#
            ..O..#O..O
            .......O..
            #....###..
            #OO..#....
            """.ToTestFunc();

        var solution = new Day14Solution(getInput);
        var result = solution.SolvePart1();

        result.Should().BeInt(136);
    }

    [Fact]
    public void Test_SolvePart2()
    {
        var getInput = """
            O....#....
            O.OO#....#
            .....##...
            OO.#O....O
            .O.....O#.
            O.#..O.#.#
            ..O..#O..O
            .......O..
            #....###..
            #OO..#....
            """.ToTestFunc();

        var solution = new Day14Solution(getInput);
        var result = solution.SolvePart2();

        result.Should().BeInt(136);
    }
}
