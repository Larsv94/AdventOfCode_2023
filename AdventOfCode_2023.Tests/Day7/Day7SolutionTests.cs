using AdventOfCode_2023.Console.Day7;
using FluentAssertions;

namespace AdventOfCode_2023.Tests.Day7;
public class Day7SolutionTests
{
    [Fact]
    public async Task SolvePart1Async()
    {
        var getInput = """
            32T3K 765
            T55J5 684
            KK677 28
            KTJJT 220
            QQQJA 483
            """.ToTestFunc();
        var solution = new Day7Solution(getInput);
        var result = await solution.SolvePart1();

        result.Should().BeInt(6440);
    }
    [Fact]
    public async Task SolvePart2Async()
    {
        var getInput = """
            32T3K 765
            T55J5 684
            KK677 28
            KTJJT 220
            QQQJA 483
            """.ToTestFunc();
        var solution = new Day7Solution(getInput);
        var result = await solution.SolvePart2();

        result.Should().BeInt(5905);
    }
}
