using AdventOfCode_2023.Console.Day7;
using FluentAssertions;

namespace AdventOfCode_2023.Tests.Day7;
public class Day7SolutionTests
{
    [Fact]
    public void SolvePart1()
    {
        var getInput = """
            32T3K 765
            T55J5 684
            KK677 28
            KTJJT 220
            QQQJA 483
            """.ToTestFunc();
        var solution = new Day7Solution(getInput);
        var result = solution.SolvePart1();

        result.Should().BeInt(6440);
    }
    [Fact]
    public void SolvePart2()
    {
        var getInput = """
            32T3K 765
            T55J5 684
            KK677 28
            KTJJT 220
            QQQJA 483
            """.ToTestFunc();
        var solution = new Day7Solution(getInput);
        var result = solution.SolvePart2();

        result.Should().BeInt(5905);
    }
}
