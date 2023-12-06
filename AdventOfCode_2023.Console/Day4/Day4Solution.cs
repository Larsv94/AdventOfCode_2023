using AdventOfCode_2023.Console.Config;

namespace AdventOfCode_2023.Console.Day4;
public class Day4Solution : BaseSolution
{
    private readonly int[] _scores;
    public Day4Solution(Func<int, string[]> getInput) : base(getInput)
    {
        _scores = GetScores();
    }
    public override int Day => 4;

    public override ValueTask<Answer> SolvePart1()
    {
        var answer = _scores.Sum(x => (int) Math.Pow(2, x - 1));

        return new Answer(answer);
    }

    public override ValueTask<Answer> SolvePart2()
    {
        var cardMultiplier = Enumerable.Range(0, _input.Length).Select(x => 1).ToArray();
        var scores = _scores.ToArray();
        var totalCards = 0;
        for (var i = 0; i < _input.Length; i++)
        {
            var score = scores[i];
            var multiplier = cardMultiplier[i];
            for (var j = 1; j <= score; j++)
            {
                cardMultiplier[i + j] += multiplier;
            }
            totalCards += multiplier;
        }

        return new Answer(totalCards);
    }

    private int[] GetScores()
    {
        return _input
                .Select(line => line
                    .Split(":")[1]
                    .Split("|")
                    .Select(x => x.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .ToArray()
                ))
                .Select(game => new
                {
                    myNumbers = game.Last(),
                    notWinningNumers = game.Last().Except(game.First())
                })
                .Select(game => game.myNumbers.Length - game.notWinningNumers.Count())
                .ToArray();
    }
}
