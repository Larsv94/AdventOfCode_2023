using AdventOfCode_2023.Console.Config;

namespace AdventOfCode_2023.Console.Day3;
public class Day3Solution : BaseSolution
{
    private readonly List<Symbol> _symbols;
    private readonly ISchematic[][] _grid;

    public Day3Solution(string[] input) : base(input)
    {
        _symbols = GetSymbols(input, out _grid);
    }
    public override int Day => 3;

    public override ValueTask<Answer> SolvePart1()
    {
        var validParts = _symbols.SelectMany(sym => sym.Mark(_grid).MarkedNumbers);
        return new Answer(validParts.Sum
            ());
    }

    public override ValueTask<Answer> SolvePart2()
    {
        var ratios = _symbols.Select(gear => gear.Mark(_grid).GetRatio());
        return new Answer(ratios.Sum());
    }

    private static List<Symbol> GetSymbols(string[] lines, out ISchematic[][] grid)
    {
        var symbols = new List<Symbol>();
        grid = new ISchematic[lines.Length][];
        for (var y = 0; y < lines.Length; y++)
        {
            var row = new ISchematic[lines[0].Length];
            for (var x = 0; x < lines[0].Length; x++)
            {
                var val = lines[y][x];
                if (char.IsNumber(val))
                {
                    var num = row[int.Max(0, x - 1)] as Number ?? new Number();
                    num.Value += val;
                    row[x] = num;
                }
                else if (val != '.')
                {
                    var symbol = new Symbol(x, y, val);
                    symbols.Add(symbol);
                    row[x] = symbol;
                }
            }
            grid[y] = row;
        }
        return symbols;
    }
}

public interface ISchematic { }
public class Number : ISchematic
{
    public string Value = string.Empty;
    public bool IsMarked { get; set; } = false;

    public bool IsPoint;
}

public record Symbol(int X, int Y, char Val) : ISchematic
{
    public HashSet<Number> Adjecent = [];
    public List<int> MarkedNumbers = [];
    public Symbol Mark(ISchematic[][] schematics)
    {
        var xLength = schematics[0].Length;
        var yLength = schematics.Length;

        var xStart = Math.Clamp(X - 1, 0, xLength);
        var yStart = Math.Clamp(Y - 1, 0, yLength);
        var xEnd = Math.Clamp(X + 2, 0, xLength);
        var yEnd = Math.Clamp(Y + 2, 0, yLength);

        for (var y = yStart; y < yEnd; y++)
            for (var x = xStart; x < xEnd; x++)
            {
                var point = schematics[y][x] as Number;
                if (point is { IsMarked: false })
                {
                    point.IsMarked = true;
                    MarkedNumbers.Add(int.Parse(point.Value));
                }
                if (point is { })
                {
                    Adjecent.Add(point);
                }
            }
        return this;
    }

    public int GetRatio()
    {
        if (Val != '*' || Adjecent.Count != 2) return 0;
        return Adjecent.Select(x => int.Parse(x.Value)).Aggregate((x, y) => x * y);
    }
}