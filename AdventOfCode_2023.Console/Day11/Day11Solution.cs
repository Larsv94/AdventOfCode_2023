using AdventOfCode_2023.Console.Config;

namespace AdventOfCode_2023.Console.Day11;
public class Day11Solution : BaseSolution
{
    public Day11Solution(Func<int, string[]> getInput) : base(getInput)
    {
    }

    public override int Day => 11;

    public override Answer SolvePart1()
    {
        return Solve();
    }

    private Answer Solve(int expansion = 1)
    {
        expansion = expansion == 1 ? 1 : expansion - 1;
        List<(long y, long x)> galaxies = [];
        var yIncrease = 0;
        var galaxiesOnColumn = new long[_input.Length];
        for (var y = 0; y < _input.Length; y++)
        {
            var line = _input[y];
            var galaxyOnRow = 0;
            for (var x = 0; x < line.Length; x++)
            {
                var c = line[x];
                if (c != '.')
                {
                    galaxyOnRow++;
                    galaxiesOnColumn[x]++;
                    galaxies.Add((y + yIncrease, x));
                }
            }
            if (galaxyOnRow == 0) { yIncrease += expansion; }
        }

        var count = 0;
        var xIncreaseMap = galaxiesOnColumn.Select(x => x != 0 ? count : count += expansion).ToArray();


        var answer = galaxies
            .Select(g => (g.y, g.x + xIncreaseMap[g.x]))
            .GetPairs()
            .Select(p => GetDistance(p.first, p.second))
            .Sum();
        return answer.ToString();
    }

    public override Answer SolvePart2()
    {
        return Solve(1_000_000);
    }



    public static long GetDistance((long x, long y) p1, (long x, long y) p2)
    {
        return Math.Abs(p2.x - p1.x) + Math.Abs(p2.y - p1.y);
    }
}

public static class Extensions
{
    public static IEnumerable<(T first, T second)> GetPairs<T>(this IEnumerable<T> input)
    {
        var inputArr = input.ToArray();
        for (var i = 0; i < inputArr.Length; i++)
        {
            for (var j = i + 1; j < inputArr.Length; j++)
            {
                yield return (inputArr[i], inputArr[j]);
            }
        }
    }
}