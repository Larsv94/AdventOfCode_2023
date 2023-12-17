using AdventOfCode_2023.Console.Config;

namespace AdventOfCode_2023.Console.Day13;
public class Day13Solution(Func<int, string[]> getInput) : BaseSolution(getInput)
{
    public override int Day => 13;

    public override Answer SolvePart1()
    {
        var grids = GetGrids(_input);

        var answer = grids.Select(Solve1).Sum();
        return answer;
    }

    public override Answer SolvePart2()
    {
        var grids = GetGrids(_input);

        var answer = grids.Select(Solve2).Sum();
        return answer;
    }
    private int Solve2(string[] grid)
    {
        if (TryGetImperfectReflectionIndex(grid, out var index))
        {
            return (index + 1) * 100;
        }
        if (TryGetImperfectReflectionIndex(grid.Pivot().ToArray(), out index))
        {
            return index + 1;
        }
        throw new Exception("No solution found");
    }

    private bool TryGetImperfectReflectionIndex(string[] grid, out int index)
    {
        var temp = FindIdenticalNeighbours(grid, 1).ToArray(); ;
        var possibleReflections = FindIdenticalNeighbours(grid, 1)
           .Where(x => !IsPerfectReflection(grid, x))
           .Where(x => IsPerfectReflection(grid, x, 1, 1))
           .ToArray();
        if (possibleReflections.Length == 1)
        {
            index = possibleReflections[0];
            return true;
        }
        index = -1;
        return false;
    }

    private int Solve1(string[] grid)
    {
        if (TryGetPerfectReflectionIndex(grid, out var index))
        {
            return (index + 1) * 100;
        }
        if (TryGetPerfectReflectionIndex(grid.Pivot().ToArray(), out index))
        {
            return index + 1;
        }
        throw new Exception("No solution found");
    }

    private bool TryGetPerfectReflectionIndex(string[] grid, out int index)
    {
        var possibleReflections = FindIdenticalNeighbours(grid)
            .Where(x => IsPerfectReflection(grid, x))
            .ToArray();
        if (possibleReflections.Length == 1)
        {
            index = possibleReflections[0];
            return true;
        }
        index = -1;
        return false;
    }

    public IEnumerable<int> FindIdenticalNeighbours(string[] patterns, int errorMargin = 0)
    {
        for (var i = 0; i < patterns.Length - 1; i++)
        {
            var difference = patterns.GetDifference(i, i + 1);
            if (difference <= errorMargin)
            {
                yield return i;
            }
        }
    }

    public bool IsPerfectReflection(string[] patterns, int index, int errorMargin = 0, int errorAllowence = 0)
    {
        if (index == -1) return false;
        var left = index;
        var right = index + 1;

        var errors = 0;

        while (left >= 0 && right < patterns.Length)
        {
            var difference = patterns.GetDifference(left, right);
            if (difference > errorMargin)
            {
                errors++;
                if (errors >= errorAllowence)
                {
                    return false;
                }
            }
            left--;
            right++;
        }
        return true;
    }

    private IEnumerable<string[]> GetGrids(IEnumerable<string> input)
    {
        List<string> grid = [];
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                yield return grid.ToArray();
                grid.Clear();
            }
            else
            {
                grid.Add(line);
            }
        }
        yield return grid.ToArray();
    }
}

public static class Day13SolutionExtensions
{
    public static IEnumerable<string> Pivot(this IEnumerable<string> originalList)
    {
        if (originalList == null || !originalList.Any())
            yield break;

        var length = originalList.First().Length;

        for (var columnIndex = 0; columnIndex < length; columnIndex++)
        {
            yield return new string(originalList.Select(row => row[columnIndex]).ToArray());
        }
    }

    public static int GetDifference(this string[] pattern, int indexRight, int indexLeft)
    {
        return pattern[indexLeft].Zip(pattern[indexRight]).Count(x => x.First != x.Second);
    }
}
