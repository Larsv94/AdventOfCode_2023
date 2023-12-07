using AdventOfCode_2023.Console.Config;
using System.Diagnostics;


var input = (int day) => RemoveLastIfEmpty(File.ReadAllLines($"Day{day}/input.txt"));

//get all BaseSolution implementations
var solution = typeof(Program).Assembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && typeof(BaseSolution).IsAssignableFrom(t))
    .Select(t => Activator.CreateInstance(t, [input]) as BaseSolution)
    .Cast<BaseSolution>()
    .OrderBy(s => s.Day)
    .Last();

Console.WriteLine("############################################");
Console.WriteLine($"################## Day {solution.Day} ###################");
Console.WriteLine("############################################");
Console.WriteLine();

Stopwatch stopwatch = new();

stopwatch.Start();
string solutionPart1 = solution.SolvePart1();
stopwatch.Stop();
Console.WriteLine($"Part 1: {solutionPart1} ({stopwatch.ElapsedMilliseconds}ms)");

stopwatch.Restart();
stopwatch.Start();
string solutionPart2;
try
{
    solutionPart2 = solution.SolvePart2();
}
catch (NotImplementedException)
{
    solutionPart2 = "Unsolved";
}
stopwatch.Stop();
Console.WriteLine($"Part 2: {solutionPart2} ({stopwatch.ElapsedMilliseconds}ms)");

string[] RemoveLastIfEmpty(string[] array)
{
    if (array.Length == 0)
    {
        return array;
    }

    if (string.IsNullOrEmpty(array[^1]))
    {
        return array.Take(array.Length - 1).ToArray();
    }

    return array;
}