using AdventOfCode_2023.Console.Config;
using System.Diagnostics;

var solving = 3;

var input = await File.ReadAllLinesAsync($"Day{solving}/input.txt");
//get all BaseSolution implementations
var solutions = typeof(Program).Assembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && typeof(BaseSolution).IsAssignableFrom(t))
    .Select(t => Activator.CreateInstance(t, [input]) as BaseSolution)
    .Cast<BaseSolution>()
    .OrderBy(s => s.Day)
    .ToList();

Stopwatch stopwatch = new();

stopwatch.Start();
string solutionPart1 = await solutions[solving - 1].SolvePart1();
stopwatch.Stop();
Console.WriteLine($"Part 1: {solutionPart1} ({stopwatch.ElapsedMilliseconds}ms)");

stopwatch.Restart();
stopwatch.Start();
string solutionPart2;
try
{
    solutionPart2 = await solutions[solving - 1].SolvePart2();
}
catch (NotImplementedException)
{
    solutionPart2 = "Unsolved";
}
stopwatch.Stop();
Console.WriteLine($"Part 2: {solutionPart2} ({stopwatch.ElapsedMilliseconds}ms)");
