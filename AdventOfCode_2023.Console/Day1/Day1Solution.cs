using AdventOfCode_2023.Console.Config;
using System.Text.RegularExpressions;

namespace AdventOfCode_2023.Console.Day1;
public partial class Day1Solution : ISolution
{
    public int Day => 1;


    public ValueTask<Answer> SolvePart1(string[] lines)
    {
        var answer = lines
            .Select(x => x.Where(s => char.IsNumber(s)))
            .Select(x => x.ToArray())
            .Select(x => new string([x.First(), x.Last()]))
            .Sum(x => int.Parse(x));
        return new(answer);
    }

    public ValueTask<Answer> SolvePart2(string[] lines)
    {
        var answer = lines
            .Select(line => WrittenNumbersOrNumbers().Matches(line))
            .Select(matches => matches.Select(match => match.Groups[1].Value).ToArray())
            .Select(numbers => new string[] { numbers[0], numbers[^1] }.Select(ConvertToNumber))
            .Select(numbers => string.Join("", numbers))
            .Sum(x => int.Parse(x));

        return new Answer(answer);
    }

    private string ConvertToNumber(string num)
    {
        return num switch
        {
            "one" => "1",
            "two" => "2",
            "three" => "3",
            "four" => "4",
            "five" => "5",
            "six" => "6",
            "seven" => "7",
            "eight" => "8",
            "nine" => "9",
            _ => num,
        };
    }

    [GeneratedRegex(@"(?=(one|two|three|four|five|six|seven|eight|nine|\d))", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex WrittenNumbersOrNumbers();
}