using AdventOfCode_2023.Console.Config;

namespace AdventOfCode_2023.Console.Day5;
public class Day5Solution : BaseSolution
{
    private readonly long[] _seeds;
    private readonly Dictionary<string, List<Mapping>> _maps;

    public Day5Solution(Func<int, string[]> getInput) : base(getInput)
    {
        _seeds = _input[0]
                    .Split(":")[1]
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(long.Parse).ToArray();
        _maps = GetMaps(_input.Skip(2));
    }
    public override int Day => 5;

    public override Answer SolvePart1()
    {

        var answer = _seeds
            .Select(seed => GetSeedLocation(seed, _maps))
            .Min();
        return new Answer(answer.ToString());
    }


    public override Answer SolvePart2()
    {
        var ranges = _seeds.Chunk(2).ToArray();
        foreach (var mappings in _maps)
        {
            List<IEnumerable<long[]>> newRanges = [];
            foreach (var range in ranges)
            {
                var start = range[0];
                var end = range[0] + range[1] - 1;
                var validMaps = mappings.Value.Where(m => m.HasOverlap(start, end));
                var validRanges = MapsToRanges(validMaps, start, end).ToArray();
                newRanges.Add(validRanges);
            }
            ranges = newRanges.SelectMany(x => x).ToArray();
        }
        var answer = ranges.MinBy(r => r[0])![0];


        return new Answer(answer.ToString());
    }

    private IEnumerable<long[]> MapsToRanges(IEnumerable<Mapping> mappings, long start, long end)
    {
        if (!mappings.Any())
        {
            yield return [start, end];
            yield break;
        }

        var prevMapping = mappings.First();
        yield return prevMapping.GetOverlappingRange(start, end);

        foreach (var map in mappings.Skip(1))
        {
            if (prevMapping.SrcEnd + 1 != map.Src)
            {
                yield return [prevMapping.SrcEnd + 1, map.Src];
            }
            yield return map.GetOverlappingRange(start, end);
            prevMapping = map;
        }
    }

    private long GetSeedLocation(long seed, Dictionary<string, List<Mapping>> maps)
    {
        var src = seed;
        foreach (var map in maps)
        {
            var range = map.Value.Where(mapping => mapping.InRange(src)).FirstOrDefault();
            src = range is null ? src : range.Map(src);
        }
        return src;
    }
    private Dictionary<string, List<Mapping>> GetMaps(IEnumerable<string> lines)
    {
        var currentMap = string.Empty;
        Dictionary<string, List<Mapping>> maps = [];
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line)) currentMap = string.Empty;
            else if (char.IsNumber(line[0]))
            {
                var values = line.Split(' ').Select(long.Parse).ToArray();
                maps[currentMap].Add(new Mapping(values[0], values[1], values[2]));
            }
            else
            {
                if (maps.TryGetValue(currentMap, out var value))
                {
                    maps[currentMap] = [.. value.OrderBy(m => m.Src)];
                }

                currentMap = line.Split(' ')[0];
                maps[currentMap] = [];
            }
        }
        return maps;
    }
}

public record class Mapping(long Dest, long Src, long Range)
{
    public long SrcEnd => Src + Range;
    public long RangeEnd => Dest + Range;

    public bool InRange(long val)
    {
        return val >= Src && val < Src + Range;
    }

    public long Map(long val)
    {
        return val.Map(Src, Src + Range - 1, Dest, Dest + Range - 1);
    }

    public bool HasOverlap(long start, long end)
    {
        return (start >= Src && start <= SrcEnd) || (end >= Src && end <= SrcEnd);
    }

    public long[] GetOverlappingRange(long start, long end)
    {
        var mapStart = InRange(start) ? Map(start) : Map(Src);
        var mapEnd = InRange(end) ? Map(end) : Map(SrcEnd);
        var rangeEnd = mapEnd - mapStart;
        return [mapStart, rangeEnd];
    }
}

file static class NumberExtensions
{
    public static long Map(this long value, long leftMin, long leftMax, long rightMin, long rightMax)
    {
        return rightMin + ((value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin));
    }
}