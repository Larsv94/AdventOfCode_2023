using AdventOfCode_2023.Console.Day1;
using FluentAssertions;

namespace AdventOfCode_2023.Tests.Day1;
public class Day1SolutionTests
{
    [Fact]
    public void SolvePart1()
    {
        var input = """
            1abc2
            pqr3stu8vwx
            a1b2c3d4e5f
            treb7uchet
            """.ToTestFunc();

        var result = new Day1Solution(input).SolvePart1();

        result.Should().BeInt(142);
    }

    [Fact]
    public void SolvePart2()
    {
        var input = """
            two1nine
            eightwothree
            abcone2threexyz
            xtwone3four
            4nineeightseven2
            zoneight234
            7pqrstsixteen
            """.ToTestFunc();

        var result = new Day1Solution(input).SolvePart2();

        result.Should().BeInt(281);
    }

    [Theory]
    [InlineData("eightfivesssxxmgthreethreeone1sevenhnz", 87)]
    [InlineData("dblfhbt7sevenninesix2threethree", 73)]
    [InlineData("lsxlqlnsevenpstsbbzpkhphrkjdd42fbxqdmc7six", 76)]
    [InlineData("one99", 19)]
    [InlineData("fourseven8sixqmjrbh3xmrmjvdkfourgvlnlq", 44)]
    [InlineData("onemrxrxfcgtwozmnglpsixgvprxd5khnmqbdf4", 14)]
    [InlineData("onemrxrxfcgtwozmnglpsixgvprxd5khnmqbdf44", 14)]
    [InlineData("11122234896865432199", 19)]
    [InlineData("42", 42)]
    [InlineData("4422", 42)]
    [InlineData("four42", 42)]
    [InlineData("42two", 42)]
    [InlineData("four42two", 42)]
    [InlineData("fourfour2two", 42)]
    [InlineData("fourfourthreetwo", 42)]
    [InlineData("ninesevensrzxkzpmgz8kcjxsbdftwoner", 91)]
    public void SolvePart2Lines(string input, int expected)
    {
        var result = new Day1Solution(input.ToTestFunc()).SolvePart2();

        result.Should().BeInt(expected);
    }
}
