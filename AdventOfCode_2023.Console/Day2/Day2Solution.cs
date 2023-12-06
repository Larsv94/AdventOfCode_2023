using AdventOfCode_2023.Console.Config;
using System.Text.RegularExpressions;

namespace AdventOfCode_2023.Console.Day2;
public partial class Day2Solution(Func<int, string[]> getInput) : BaseSolution(getInput)
{
    public override int Day => 2;

    public override ValueTask<Answer> SolvePart1()
    {
        var cubes = new Dictionary<string, int>
        {
            { "red", 12 },
            { "green", 13 },
            { "blue", 14 },
        };

        var games = _input
            .Select(x => GameRegex()
                .Match(x).Groups)
            .Where(groups => groups[5].Captures
                .Zip(groups[6].Captures)
                .All(tuple =>
                {
                    var max = cubes[tuple.Second.Value];
                    return int.Parse(tuple.First.Value) <= max;
                }))
            .Sum(x => int.Parse(x[1].Value));

        return new Answer(games);
    }

    public override ValueTask<Answer> SolvePart2()
    {
        var power = _input
            .Select(x => GameRegex()
                .Match(x).Groups)
            .Select(groups => GetSetPower(groups[5].Captures.Zip(groups[6].Captures)))
            .Sum();
        return new Answer(power);
    }

    private int GetSetPower(IEnumerable<(Capture First, Capture Second)> randomPicks)
    {
        var minimalCubes = new Dictionary<string, int>
        {
            { "red", 0 },
            { "green", 0 },
            { "blue", 0 },
        };

        foreach (var (First, Second) in randomPicks)
        {
            var val = int.Parse(First.Value);
            var color = Second.Value;
            minimalCubes[color] = int.Max(val, minimalCubes[color]);
        }
        return minimalCubes.Values.Aggregate((x, y) => x * y);
    }

    [GeneratedRegex(@"Game (\d+):(( ((\d{1,2}) (\w+))[,;]?)+)", RegexOptions.IgnoreCase)]
    private static partial Regex GameRegex();
}
