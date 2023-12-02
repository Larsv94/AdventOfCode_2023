using AdventOfCode_2023.Console.Config;
using System.Diagnostics;

var solving = 2;

var solutions = typeof(Program).Assembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && typeof(ISolution).IsAssignableFrom(t))
    .Select(t => Activator.CreateInstance(t) as ISolution)
    .Cast<ISolution>()
    .OrderBy(s => s.Day)
    .ToList();


var input = await File.ReadAllLinesAsync($"Day{solving}/input.txt");



Stopwatch stopwatch = new();

stopwatch.Start();
string solutionPart1 = await solutions[solving - 1].SolvePart1(input);
stopwatch.Stop();
Console.WriteLine($"Part 1: {solutionPart1} ({stopwatch.ElapsedMilliseconds}ms)");

stopwatch.Restart();
stopwatch.Start();
string solutionPart2;
try
{
    solutionPart2 = await solutions[solving - 1].SolvePart2(input);
}
catch (NotImplementedException)
{
    solutionPart2 = "Unsolved";
}
stopwatch.Stop();
Console.WriteLine($"Part 2: {solutionPart2} ({stopwatch.ElapsedMilliseconds}ms)");
