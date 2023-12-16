using AdventOfCode_2023.Console.Config;
using System.Data;
using System.Text;

namespace AdventOfCode_2023.Console.Day10;
public class Day10Solution : BaseSolution
{
    private static readonly char[] _canConnectEast = ['-', 'L', 'F'];
    private static readonly char[] _canConnectWest = ['-', 'J', '7'];
    private static readonly char[] _canConnectSouth = ['|', '7', 'F'];
    private static readonly char[] _canConnectNorth = ['|', 'L', 'J'];
    private readonly Pipe _start = null!;
    private readonly Pipe[][] _pipes;

    public Day10Solution(Func<int, string[]> getInput) : base(getInput)
    {
        var grid = _input.Select(l => l.ToCharArray()).ToArray();
        var height = grid.Length;
        var width = grid[0].Length;
        _pipes = new Pipe[height][];
        for (var y = 0; y < height; y++)
        {
            var pipeRow = new Pipe[width];
            for (var x = 0; x < width; x++)
            {
                var val = grid[y][x];
                var pipe = new Pipe(val, x, y);
                if (y - 1 >= 0)
                {
                    var north = _pipes[y - 1][x];
                    pipe.ConnectThisWithNorth(north);
                }
                if (x - 1 >= 0)
                {
                    var west = pipeRow[x - 1];
                    pipe.ConnectThisWithWest(west);
                }
                pipeRow[x] = pipe;
                if (val == 'S') _start = pipe;

            }
            _pipes[y] = pipeRow;
        }
    }

    public override int Day => 10;

    public override Answer SolvePart1()
    {

        var startingPipes = _start.GetStartingConnections().ToArray();
        var steps = 0;
        while (startingPipes.All(p => !p.Value.IsTraversed))
        {
            startingPipes = startingPipes.Select(GetNextConnection).ToArray();
            steps++;
        }
        return steps;
    }

    private KeyValuePair<Direction, Pipe> GetNextConnection(KeyValuePair<Direction, Pipe> pair)
    {
        var comingFrom = pair.Key.Invert();
        var otherConnection = pair.Value.Next(comingFrom);
        pair.Value.MarkTraversed();
        return otherConnection;
    }

    public override Answer SolvePart2()
    {

        var current = _start.ValidConnections.First();
        var debug1 = Debug(null, true);
        var totalDirection = 0;
        var debug2 = current.Value.PipeName;
        while (current.Value != _start)
        {
            var comingFrom = current.Key.Invert();
            var next = current.Value.Next(comingFrom);
            totalDirection += current.Value.MarkTraversedAndSides(comingFrom);
            current = next;
        }

        var inside = totalDirection % 360 > 0 ? Position.Right : Position.Left;
        var insideMarkedPipes = _pipes.SelectMany(x => x).Where(x => x.PipePosition == inside && !x.IsTraversed);

        System.Console.WriteLine($"Inside is on the {inside}");

        var debug3 = Debug(inside, true);

        foreach (var pipe in insideMarkedPipes)
        {
            foreach (var connection in pipe.ValidConnections)
            {
                connection.Value.Propegate(inside);
            }
            //System.Console.WriteLine("=======================");
            //var debug4 = Debug(inside, true);
        }
        Debug(inside, true);


        var totalInside = _pipes.SelectMany(x => x).Where(x => x.PipePosition == inside && !x.IsTraversed).Count();

        return totalInside;
    }

    public string Debug(Position? inside, bool showOutside)
    {
        var stringBuilder = new StringBuilder();
        foreach (var row in _pipes)
        {
            foreach (var pipe in row)
            {
                var name = showOutside ? '.' : pipe.PipeName;
                if (pipe.IsTraversed)
                {
                    name = showOutside ? pipe.PipeName : '#';
                }
                else if (inside is not null && pipe.PipePosition == inside)
                {
                    name = '%';
                }
                else if (showOutside && pipe.PipePosition != Position.None)
                {
                    name = '@';
                }
                if (pipe.PipeName == 'S')
                {
                    name = 'S';
                }

                stringBuilder.Append(name);
            }
            stringBuilder.AppendLine();

        }

        var result = stringBuilder.ToString();
        System.Console.WriteLine(result);
        return result;
    }

}

public class Pipe(char pipeName, int X, int Y)
{
    private static readonly char[] _canConnectEast = ['-', 'L', 'F'];
    private static readonly char[] _canConnectWest = ['-', 'J', '7'];
    private static readonly char[] _canConnectSouth = ['|', '7', 'F'];
    private static readonly char[] _canConnectNorth = ['|', 'L', 'J'];

    public bool IsTraversed { get; private set; } = false;
    public char PipeName { get; } = pipeName;
    public int X { get; } = X;
    public int Y { get; } = Y;
    public Dictionary<Direction, Pipe> Connections { get; } = [];

    public IEnumerable<KeyValuePair<Direction, Pipe>> ValidConnections
        => Connections.Where(x => x.Value.PipeName != '.');

    private readonly List<Direction> _validDirections = GetValidDirections(pipeName).ToList();

    public Position PipePosition { get; set; }

    public int MarkTraversedAndSides(Direction comingFrom)
    {
        IsTraversed = true;
        var left = GetLeft(comingFrom, PipeName);
        var right = GetLeft(_validDirections.First(x => x != comingFrom), PipeName);
        PipePosition = Position.Invalid;
        if (left.Concat(right).Count() > 2)
        {
            throw new DataException("More than 2 directions found");
        }

        foreach (var leftPipe in left)
        {
            if (Connections.TryGetValue(leftPipe, out var pipe) && !pipe.IsTraversed)
            {
                pipe.PipePosition = Position.Left;
            }
        }
        foreach (var rightPipe in right)
        {
            if (Connections.TryGetValue(rightPipe, out var pipe) && !pipe.IsTraversed)
            {
                pipe.PipePosition = Position.Right;
            }
        }
        return GetTurnDegree();

    }
    public void MarkTraversed()
    {
        IsTraversed = true;
    }

    public Position Propegate(Position inside)
    {
        PipePosition = Position.Invalid;
        var outside = inside.Invert();
        if (Connections.Count > 4)
        {
            PipePosition = outside;
            return outside;
        }
        var isValid = Connections
            .Where(x => x.Value.PipePosition == Position.None)
            .All(x => x.Value.Propegate(inside) == inside);
        if (isValid)
        {
            PipePosition = inside;
            return inside;
        }
        return Position.Invalid;
    }

    public void ConnectThisWithWest(Pipe westPipe)
    {
        Connections.Add(Direction.West, westPipe);
        westPipe.Connections.Add(Direction.East, this);
    }

    public void ConnectThisWithNorth(Pipe northPipe)
    {
        Connections.Add(Direction.North, northPipe);
        northPipe.Connections.Add(Direction.South, this);
    }

    public KeyValuePair<Direction, Pipe> Next(Direction comingFrom)
    {
        var nextDirection = _validDirections.First(x => x != comingFrom);
        return Connections.First(x => x.Key == nextDirection);
    }

    public bool CanConnect(Direction direction)
    {
        return _validDirections.Contains(direction);
    }

    public IEnumerable<KeyValuePair<Direction, Pipe>> GetStartingConnections()
    {
        foreach (var connection in Connections)
        {
            if (connection.Value.CanConnect(connection.Key.Invert()))
            {
                yield return connection;
            }
        }
    }

    private static IEnumerable<Direction> GetValidDirections(char pipeName)
    {
        if (_canConnectEast.Contains(pipeName)) yield return Direction.East;
        if (_canConnectWest.Contains(pipeName)) yield return Direction.West;
        if (_canConnectNorth.Contains(pipeName)) yield return Direction.North;
        if (_canConnectSouth.Contains(pipeName)) yield return Direction.South;
    }

    private static List<Direction> GetLeft(Direction comingFrom, char pipeName)
    {
        return (comingFrom, pipeName) switch
        {
            (Direction.East, 'L') => [Direction.West, Direction.South],
            (Direction.North, 'L') => [],
            (Direction.South, '|') => [Direction.West],
            (Direction.North, '|') => [Direction.East],
            (Direction.East, '-') => [Direction.South],
            (Direction.West, '-') => [Direction.North],
            (Direction.North, 'J') => [Direction.East, Direction.South],
            (Direction.West, 'J') => [],
            (Direction.South, '7') => [],
            (Direction.West, '7') => [Direction.North, Direction.East],
            (Direction.South, 'F') => [Direction.West, Direction.North],
            (Direction.East, 'F') => [],
            _ => [],
        };
    }

    private int GetTurnDegree()
    {
        return PipeName switch
        {
            'L' => -90,
            'J' => 90,
            '7' => -90,
            'F' => 90,
            _ => 0,
        };
    }
}

public enum Direction
{
    North,
    South,
    East,
    West
}

public enum Position
{
    None,
    Left,
    Right,
    Invalid
}

public static class Extionsions
{
    public static Direction Invert(this Direction direction)
    {
        return direction switch
        {
            Direction.North => Direction.South,
            Direction.South => Direction.North,
            Direction.East => Direction.West,
            Direction.West => Direction.East,
            _ => throw new NotImplementedException(),
        };
    }

    public static Position Invert(this Position position)
    {
        return position switch
        {
            Position.Left => Position.Right,
            Position.Right => Position.Left,
            _ => throw new NotImplementedException(),
        };
    }
}