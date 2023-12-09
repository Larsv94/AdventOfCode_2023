using AdventOfCode_2023.Console.Config;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode_2023.Console.Day8;
public class Day8Solution : BaseSolution
{
    private readonly Dictionary<string, Node> _nodes;

    public Day8Solution(Func<int, string[]> getInput) : base(getInput)
    {
        _nodes = [];
        foreach (var node in _input.Skip(2))
        {
            var split = node.Split(" = ");
            var name = split[0];

            if (_nodes.TryGetValue(name, out var existingNode))
            {
                existingNode.ApplyChildren(split[1], _nodes);
                continue;
            }

            _nodes[name] = new Node(name).ApplyChildren(split[1], _nodes);
        }
    }

    public override int Day => 8;

    public override Answer SolvePart1()
    {
        var instructions = _input[0];
        var current = _nodes.First(n => n.Value.Name == "AAA").Value;
        var index = 0;
        var steps = 0;
        while (current.Name != "ZZZ")
        {
            var step = instructions[index];

            current = current[step];

            steps++;
            index = (index + 1) % instructions.Length;
        }

        return steps;
    }

    public override Answer SolvePart2()
    {
        var instructions = _input[0];
        var answer = _nodes
            .Values
            .Where(n => n.Name.EndsWith('A'))
            .Select(n => GetStepsToEnd(n, instructions))
            .ToArray()
            .LCM();

        return (Answer) answer;
    }

    public long GetStepsToEnd(Node node, string instructions)
    {
        var current = node;
        var index = 0;
        var steps = 0l;
        while (!current.Name.EndsWith('Z'))
        {
            var step = instructions[index];

            current = current[step];

            steps++;
            index = (index + 1) % instructions.Length;
        }
        return steps;
    }
}

[DebuggerDisplay("Name = {Name}")]
public partial class Node(string name)
{
    public string Name { get; private set; } = name;
    public Node Left { get; private set; } = null!;
    public Node Right { get; private set; } = null!;

    public Node ApplyChildren(string childString, Dictionary<string, Node> nodes)
    {
        var children = GetChildrenRegex().Match(childString).Groups;
        Left = GetNode(nodes, children[1].Value);
        Right = GetNode(nodes, children[2].Value);

        return this;
    }

    private Node GetNode(Dictionary<string, Node> nodes, string childName)
    {
        if (childName == this.Name) return this;
        return nodes.TryGetValue(childName, out var leftNode) ?
            leftNode :
            (nodes[childName] = new Node(childName));
    }

    public Node this[char pos]
    {
        get
        {
            if (pos == 'L') return Left;
            if (pos == 'R') return Right;
            throw new NotSupportedException();
        }
    }

    [GeneratedRegex(@"\(([0-9A-Z]{3}), ([0-9A-Z]{3})\)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex GetChildrenRegex();
}

public static class ArrayExtensions
{
    public static long LCM(this IEnumerable<long> numbers)
    {
        return numbers.Aggregate(LCMInternal);
    }
    private static long LCMInternal(long a, long b)
    {
        return Math.Abs(a * b) / GCDInternal(a, b);
    }
    private static long GCDInternal(long a, long b)
    {
        return b == 0 ? a : GCDInternal(b, a % b);
    }
}