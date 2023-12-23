using AdventOfCode_2023.Console.Config;
using System.Text;

namespace AdventOfCode_2023.Console.Day14;
public class Day14Solution(Func<int, string[]> getInput) : BaseSolution(getInput)
{
    public override int Day => 14;

    public override Answer SolvePart1()
    {
        var grid = _input.Select(line => line.ToCharArray()).ToArray();
        var stoneYLevel = MoveStonesNorth(grid);
        return stoneYLevel.Sum(y => grid.Length - y);
    }

    private static List<int> MoveStonesNorth(char[][] grid)
    {
        var allowedStonePlacement = Enumerable.Range(0, grid[0].Length).Select(x => -1).ToArray();
        List<int> stoneYLevel = [];
        for (var y = 0; y < grid.Length; y++)
        {
            var row = grid[y];
            for (var x = 0; x < row.Length; x++)
            {
                var rock = row[x];
                var allowedInColumn = allowedStonePlacement[x];
                var rockHasMoved = false;

                if (rock == 'O')
                {
                    if (allowedInColumn is not -1)
                    {
                        stoneYLevel.Add(allowedInColumn);
                        allowedStonePlacement[x] = -1;
                        grid[allowedInColumn][x] = rock;
                        row[x] = '.';
                        rockHasMoved = true;
                    }
                    else
                    {
                        stoneYLevel.Add(y);
                    }
                }

                allowedStonePlacement[x] = rock switch
                {
                    '.' when allowedInColumn is -1 => y,
                    '.' when allowedInColumn is not -1 => allowedInColumn,
                    'O' when rockHasMoved => allowedInColumn + 1,
                    _ => -1
                };
            }
        }

        return stoneYLevel;
    }

    public override Answer SolvePart2()
    {
        var grid = _input.Select(line => line.ToCharArray()).ToArray();

        var totalCycles = 1_000_000_000;

        var cycle = 0;
        var sequence = new List<int>();
        var loopStart = -1;
        while (cycle < totalCycles && loopStart < 0)
        {
            sequence.Add(Cycle(grid));
            loopStart = FindLoop(sequence);
        }


        return ExtrapolateValue(sequence, loopStart, totalCycles);
    }

    private int FindLoop(List<int> sequence)
    {
        for (var i = 0; i < sequence.Count; i++)
        {
            for (var j = i + 1; j < sequence.Count; j++)
            {
                if (IsSubsequenceLooping(i, j, sequence))
                {
                    return i;
                }
            }
        }
        return -1;
    }

    private bool IsSubsequenceLooping(int start, int end, List<int> sequence)
    {
        var length = end - start;
        if (length < 2) return false; //Loop must be at least 2 long
        if (end + length > sequence.Count)
        {
            return false;
        }
        for (var k = 0; k < length; k++)
        {
            if (sequence[start + k] != sequence[end + k])
            {
                return false;
            }
        }
        return true;
    }

    private static int ExtrapolateValue(List<int> values, int loopStartIndex, int targetCycle)
    {
        var loopLength = values.Count - loopStartIndex;
        var indexInLoop = (targetCycle - loopStartIndex - 1) % loopLength;
        return values[loopStartIndex + indexInLoop];
    }

    private static int Cycle(char[][] grid)
    {

        List<int> yLevels = [];
        for (var turns = 0; turns < 4; turns++)
        {
            yLevels = MoveStonesNorth(grid);
            grid.Rotate();
        }
        var calculatedLoad = grid.Select((x, index) => (grid.Length - index) * x.Count(y => y == 'O')).Sum();

        return calculatedLoad;
    }
}

public record struct Position(int X, int Y);
public static class Day14Extensions
{
    public static void Rotate(this char[][] grid)
    {
        var n = grid.Length;
        for (var i = 0; i < n / 2; i++)
        {
            for (var j = i; j < n - i - 1; j++)
            {
                var temp = grid[i][j];
                grid[i][j] = grid[n - 1 - j][i];
                grid[n - 1 - j][i] = grid[n - 1 - i][n - 1 - j];
                grid[n - 1 - i][n - 1 - j] = grid[j][n - 1 - i];
                grid[j][n - 1 - i] = temp;
            }
        }
    }

    public static string DebugPrint(this char[][] matrix)
    {
        var sb = new StringBuilder();
        for (var i = 0; i < matrix.Length; i++)
        {
            for (var j = 0; j < matrix[i].Length; j++)
            {
                var val = matrix[i][j] switch
                {
                    'O' => $"{matrix.Length - i}",
                    '.' => ".",
                    _ => " "
                };
                sb.Append(val);
                System.Console.Write(val);
            }
            sb.AppendLine();
            System.Console.WriteLine();
        }
        System.Console.WriteLine();
        System.Console.WriteLine("##################################################################################");
        System.Console.WriteLine();
        return sb.ToString();
    }
}