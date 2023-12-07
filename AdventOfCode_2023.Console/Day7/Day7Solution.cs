using AdventOfCode_2023.Console.Config;

namespace AdventOfCode_2023.Console.Day7;
public class Day7Solution(Func<int, string[]> GetInput) : BaseSolution(GetInput)
{
    public override int Day => 7;

    public override ValueTask<Answer> SolvePart1()
    {
        var answer = _input
            .Select(line => line.Split(' '))
            .Select(values => new Hand(values[0], int.Parse(values[1])))
            .OrderDescending()
            .Select((hand, index) => hand.Bid * (index + 1))
            .Sum();
        return new Answer(answer);
    }

    public override ValueTask<Answer> SolvePart2()
    {
        var answer = _input
            .Select(line => line.Split(' '))
            .Select(values => new Hand(values[0], int.Parse(values[1]), true))
            .OrderDescending()
            .Select((hand, index) => hand.Bid * (index + 1))
            .Sum();
        return new Answer(answer);
    }
}

public class Hand(string cards, int bid, bool additionalRule = false) : IComparable<Hand>
{
    public int Rank { get; private set; } = GetRank(cards, additionalRule);
    public string Cards { get; private set; } = cards;
    public int Bid { get; private set; } = bid;

    private readonly bool _additionalRuleApplies = additionalRule;

    private static int GetRank(string cards, bool additionalRule)
    {
        var cardCount = cards
            .Where(c => !additionalRule || c != 'J')
            .GroupBy(x => x)
            .Select(x => x.Count())
            .OrderDescending()
            .ToArray();

        var jokers = 5 - cardCount.Sum();
        cardCount = cardCount.Length != 0 ? [cardCount[0] + jokers, .. cardCount[1..]] : [5];

        return cardCount switch
        {
            [5] => 7,
            [4, 1] => 6,
            [3, 2] => 5,
            [3, 1, 1] => 4,
            [2, 2, 1] => 3,
            [2, 1, 1, 1] => 2,
            _ => 1
        };
    }

    public int CompareTo(Hand? other)
    {
        if (other == null) return 1;
        if (other.Rank - Rank != 0)
        {
            return other.Rank - Rank;
        }
        var (First, Second) = Cards
                .Select(GetCardValue)
                .Zip(other.Cards.Select(GetCardValue))
                .Where(z => z.First != z.Second)
                .First();
        return Second - First;
    }

    private int GetCardValue(char card)
    {
        return card switch
        {
            'A' => 14,
            'K' => 13,
            'Q' => 12,
            'J' => _additionalRuleApplies ? 1 : 11,
            'T' => 10,
            _ => (int) char.GetNumericValue(card)
        };
    }
}
