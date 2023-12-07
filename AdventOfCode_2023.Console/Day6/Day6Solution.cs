using AdventOfCode_2023.Console.Config;

namespace AdventOfCode_2023.Console.Day6;
public class Day6Solution : BaseSolution
{
    private readonly List<(long Time, long Distance)> _TimeDistancePairs;

    public Day6Solution(Func<int, string[]> getInput) : base(getInput)
    {
        _TimeDistancePairs = GetTimeDistancePairs();
    }

    public override int Day => 6;

    public override Answer SolvePart1()
    {
        var answer = _TimeDistancePairs
            .Select(GetMargin)
            .Aggregate((x, y) => x * y);

        return new Answer(answer.ToString());
    }

    public override Answer SolvePart2()
    {
        var time = long.Parse(string.Join("", _TimeDistancePairs.Select(x => x.Time.ToString())));
        var distance = long.Parse(string.Join("", _TimeDistancePairs.Select(x => x.Distance.ToString())));
        System.Console.WriteLine($"We're racing {time}ms and need to beat {distance}mm");
        var answer = GetMargin((time, distance));
        return new Answer(answer.ToString());
    }

    public long GetMargin((long Time, long Distance) pair)
    {
        (var time, var distance) = pair;
        var middle = (long) Math.Round(time / 2d);
        var min = GetMinimum(0, time, time, distance) + 1;
        var margin = (middle - min) * 2;
        return long.IsEvenInteger(time) ? margin + 1 : margin;
    }
    public long GetMinimum(long min, long max, long time, long distance)
    {
        var val = (min + max) / 2;
        var distanceOverTime = val * (time - val);

        if (val == min || val == max) return val;

        if (distanceOverTime < distance)
        {
            return GetMinimum(val, max, time, distance);
        }
        else if (distanceOverTime > distance)
        {
            return GetMinimum(min, val, time, distance);
        }
        return val;
    }


    private List<(long Time, long Distance)> GetTimeDistancePairs()
    {
        var lists = _input.Select(line =>
            line.Split(':', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(long.Parse))
            .ToArray();
        var pairs = lists[0].Zip(lists[1]);

        return pairs.ToList();
    }
}
