namespace AdventOfCode_2023.Tests;
public static class TestStringExtensions
{
    private static readonly string[] separator = ["\r\n", "\r", "\n"];

    public static string[] ToLines(this string str)
    {
        return str.Split(
        separator,
        StringSplitOptions.None
        );
    }

    public static Func<int, string[]> ToTestFunc(this string str)
    {
        var lines = str.ToLines();
        return (int _) => lines;
    }
}
