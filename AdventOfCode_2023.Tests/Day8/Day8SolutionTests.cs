using AdventOfCode_2023.Console.Day8;
using FluentAssertions;

namespace AdventOfCode_2023.Tests.Day8;
public class Day8SolutionTests
{


    [Fact]
    public void SolvePart1()
    {
        var GetTestInput = """
        RL

        AAA = (BBB, CCC)
        BBB = (DDD, EEE)
        CCC = (ZZZ, GGG)
        DDD = (DDD, DDD)
        EEE = (EEE, EEE)
        GGG = (GGG, GGG)
        ZZZ = (ZZZ, ZZZ)
        """.ToTestFunc();
        var solution = new Day8Solution(GetTestInput);
        var result = solution.SolvePart1();

        result.Should().BeInt(2);
    }
    [Fact]
    public void SolvePart1_2()
    {
        var GetTestInput = """
        LLR

        AAA = (BBB, BBB)
        BBB = (AAA, ZZZ)
        ZZZ = (ZZZ, ZZZ)
        """.ToTestFunc();
        var solution = new Day8Solution(GetTestInput);
        var result = solution.SolvePart1();

        result.Should().BeInt(6);
    }

    [Fact]
    public void SolvePart2()
    {
        var GetTestInput = """
        LR

        11A = (11B, XXX)
        11B = (XXX, 11Z)
        11Z = (11B, XXX)
        22A = (22B, XXX)
        22B = (22C, 22C)
        22C = (22Z, 22Z)
        22Z = (22B, 22B)
        XXX = (XXX, XXX)
        """.ToTestFunc();
        var solution = new Day8Solution(GetTestInput);
        var result = solution.SolvePart2();

        result.Should().BeInt(6);
    }

    [Fact]
    public void SolvePart2_2()
    {
        var GetTestInput = """
        LR

        11A = (11B, XXX)
        11B = (XXX, 11Z)
        11Z = (11B, XXX)
        22A = (22B, XXX)
        22B = (22C, 22C)
        22C = (22Z, 22Z)
        22Z = (22B, 22B)
        33A = (33B, 33B)
        33B = (33C, 33C)
        33C = (33D, 33D)
        33D = (33Z, 33Z)
        33Z = (33B, 33B)
        XXX = (XXX, XXX)
        """.ToTestFunc();
        var solution = new Day8Solution(GetTestInput);
        var result = solution.SolvePart2();

        result.Should().BeInt(12);
    }

    [Fact]
    public void SolvePart2_3()
    {
        var GetTestInput = """
        LR

        11A = (11B, XXX)
        11B = (XXX, 11Z)
        11Z = (11B, XXX)
        22A = (22B, XXX)
        22B = (22C, 22C)
        22C = (22Z, 22Z)
        22Z = (22B, 22B)
        33A = (33B, 33B)
        33B = (33C, 33C)
        33C = (33D, 33D)
        33D = (33Z, 33Z)
        33Z = (33B, 33B)
        44A = (44B, 44B)
        44B = (44C, 44C)
        44C = (44D, 44D)
        44D = (44E, 44E)
        44E = (44Z, 44Z)
        44Z = (44B, 44B)
        XXX = (XXX, XXX)
        """.ToTestFunc();
        var solution = new Day8Solution(GetTestInput);
        var result = solution.SolvePart2();

        result.Should().BeInt(60);
    }

    [Fact]
    public void SolvePart2_4()
    {
        var GetTestInput = """
        LR

        11A = (11B, XXX)
        11B = (XXX, 11Z)
        11Z = (11B, XXX)
        22A = (22B, XXX)
        22B = (22C, 22C)
        22C = (22Z, 22Z)
        22Z = (22B, 22B)
        33A = (33B, 33B)
        33B = (33C, 33C)
        33C = (33D, 33D)
        33D = (33Z, 33Z)
        33Z = (33B, 33B)
        44A = (44B, 44B)
        44B = (44C, 44C)
        44C = (44D, 44D)
        44D = (44E, 44E)
        44E = (44Z, 44Z)
        44Z = (44B, 44B)
        55A = (55B, 55B)
        55B = (55C, 55C)
        55C = (55D, 55D)
        55D = (55E, 55E)
        55E = (55F, 55F)
        55F = (55G, 55G)
        55G = (55Z, 55Z)
        55Z = (55B, 55B)
        XXX = (XXX, XXX)
        """.ToTestFunc();
        var solution = new Day8Solution(GetTestInput);
        var result = solution.SolvePart2();

        result.Should().BeInt(420);
    }

    [Theory]
    [MemberData(nameof(TestLCMData))]
    public void TestLCM(long[] numbers, long expected)
    {
        var result = numbers.LCM();
        result.Should().Be(expected);
    }

    public static IEnumerable<object[]> TestLCMData()
    {
        yield return [new long[] { 2, 3 }, 6L];
        yield return [new long[] { 2, 3, 4 }, 12L];
        yield return [new long[] { 12, 15, 75 }, 300L];
        yield return [new long[] { 123, 1512, 759987 }, 5_234_790_456];
        yield return [new long[] { 123765, 151245, 759987754 }, 948_405_814_141_126_230];

    }
}
