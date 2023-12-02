namespace AdventOfCode_2023.Console.Config;

//Discriminated union for string or int answers
public record struct Answer
{
    public string? StringAnswer { get; }
    public int? IntAnswer { get; }

    public Answer(string answer)
    {
        StringAnswer = answer;
        IntAnswer = null;
    }

    public Answer(int answer)
    {
        StringAnswer = null;
        IntAnswer = answer;
    }

    public override string ToString()
    {
        return StringAnswer ?? IntAnswer?.ToString() ?? "Unsolved";
    }

    public static implicit operator Answer(string answer)
    {
        return new(answer);
    }

    public static implicit operator Answer(int answer)
    {
        return new(answer);
    }

    public static implicit operator string(Answer answer)
    {
        return answer.ToString();
    }

    public static implicit operator int(Answer answer)
    {
        return answer.IntAnswer ?? throw new InvalidOperationException("Answer is not an int");
    }

    public static implicit operator ValueTask<Answer>(Answer answer)
    {
        return new(answer);
    }
}
