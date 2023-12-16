using AdventOfCode_2023.Console.Day10;

namespace AdventOfCode_2023.Tests.Day10;
public class Day10SolutionTests
{

    [Fact]
    public void SolvePart1_Simple()
    {
        var getInput = """
        .....
        .S-7.
        .|.|.
        .L-J.
        .....
        """.ToTestFunc();
        var solution = new Day10Solution(getInput);
        var answer = solution.SolvePart1();
        answer.Should().BeInt(4);
    }

    [Fact]
    public void SolvePart1_Complex()
    {
        var getInput = """
        7-F7-
        .FJ|7
        SJLL7
        |F--J
        LJ.LJ
        """.ToTestFunc();
        var solution = new Day10Solution(getInput);
        var answer = solution.SolvePart1();
        answer.Should().BeInt(8);
    }

    [Fact]
    public void SolvePart2_Simple()
    {
        var getInput = """
        ...........
        .S-------7.
        .|F-----7|.
        .||.....||.
        .||.....||.
        .|L-7.F-J|.
        .|..|.|..|.
        .L--J.L--J.
        ...........
        """.ToTestFunc();
        var solution = new Day10Solution(getInput);
        var answer = solution.SolvePart2();
        answer.Should().BeInt(4);
    }
    [Fact]
    public void SolvePart2_Simple_2()
    {
        var getInput = """
        ..........
        .S------7.
        .|F----7|.
        .||....||.
        .||....||.
        .|L-7F-J|.
        .|..||..|.
        .L--JL--J.
        ..........
        """.ToTestFunc();
        var solution = new Day10Solution(getInput);
        var answer = solution.SolvePart2();
        answer.Should().BeInt(4);
    }

    [Fact]
    public void SolvePart2_Complex()
    {
        var getInput = """
        FF7FSF7F7F7F7F7F---7
        L|LJ||||||||||||F--J
        FL-7LJLJ||||||LJL-77
        F--JF--7||LJLJ7F7FJ-
        L---JF-JLJ.||-FJLJJ7
        |F|F-JF---7F7-L7L|7|
        |FFJF7L7F-JF7|JL---7
        7-L-JL7||F7|L7F-7F7|
        L.L7LFJ|||||FJL7||LJ
        L7JLJL-JLJLJL--JLJ.L
        """.ToTestFunc();
        var solution = new Day10Solution(getInput);
        var answer = solution.SolvePart2();
        answer.Should().BeInt(10);
    }
}
