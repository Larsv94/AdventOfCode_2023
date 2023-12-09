using AdventOfCode_2023.Console.Config;
using AdventOfCode_2023.Console.Utils;

namespace AdventOfCode_2023.Console.Day9;
public class Day9Solution : BaseSolution
{
    private readonly IEnumerable<List<List<int>>> _sequences;
    public Day9Solution(Func<int, string[]> getInput) : base(getInput)
    {
        _sequences = _input
            .Select(line => line.Split(' ').Select(int.Parse))
            .Select(line => GetSequences(line));
    }
    public override int Day => 9;

    public override Answer SolvePart1()
    {
        return _sequences
            .Select(GetNextInSequence)
            .Sum();
    }
    public override Answer SolvePart2()
    {
        return _sequences
        .Select(GetFirstInSequence)
        .Sum();
    }
    private int GetNextInSequence(List<List<int>> list)
    {
        list.Reverse();
        List<int> prevSeq = [.. list[0], 0];
        foreach (var seq in list.Skip(1))
        {
            var prevLastItem = prevSeq[^1];
            var thisLastItem = seq[^1];
            prevSeq = [.. seq, thisLastItem + prevLastItem];
        }
        return prevSeq[^1];
    }

    private int GetFirstInSequence(List<List<int>> list)
    {
        list.Reverse();
        List<int> prevSeq = [0, .. list[0]];
        foreach (var seq in list.Skip(1))
        {
            var prevFirstItem = prevSeq[0];
            var thisFirstItem = seq[0];
            prevSeq = [thisFirstItem - prevFirstItem, .. seq,];
        }
        return prevSeq[0];
    }

    public static List<List<int>> GetSequences(IEnumerable<int> source)
    {
        var sourceList = source.ToList();
        var differences = sourceList.Window(2).Select(x => x[1] - x[0]).ToList();
        List<List<int>> list = [sourceList, differences];
        while (!differences.All(x => x == 0))
        {
            differences = differences.Window(2).Select(x => x[1] - x[0]).ToList();
            list.Add(differences);
        }
        return list;
    }
}