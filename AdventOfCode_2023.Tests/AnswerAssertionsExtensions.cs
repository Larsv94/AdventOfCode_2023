using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace AdventOfCode_2023.Tests;
public static class AnswerAssertionsExtensions
{
    public static AndConstraint<ObjectAssertions> BeString(this ObjectAssertions assertions, string expectedString, string because = "", params object[] becauseArgs)
    {
        if (assertions.Subject is Answer answer)
        {
            Execute.Assertion
                .ForCondition(answer.StringAnswer == expectedString)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected Answer to be a string '{0}'{reason}, but found '{1}'", expectedString, answer.StringAnswer);
        }
        else
        {
            Execute.Assertion
                .FailWith("Expected object to be of type Answer, but found '{0}'", assertions.Subject.GetType());
        }

        return new AndConstraint<ObjectAssertions>(assertions);
    }

    public static AndConstraint<ObjectAssertions> BeInt(this ObjectAssertions assertions, int expectedInt, string because = "", params object[] becauseArgs)
    {
        if (assertions.Subject is Answer answer)
        {
            Execute.Assertion
                .ForCondition(answer.IntAnswer == expectedInt)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected Answer to be an int '{0}'{reason}, but found '{1}'", expectedInt, answer.IntAnswer);
        }
        else
        {
            Execute.Assertion
                .FailWith("Expected object to be of type Answer, but found '{0}'", assertions.Subject.GetType());
        }

        return new AndConstraint<ObjectAssertions>(assertions);
    }

    public static AndConstraint<ObjectAssertions> BeLong(this ObjectAssertions assertions, long expectedLong, string because = "", params object[] becauseArgs)
    {
        if (assertions.Subject is Answer answer)
        {
            Execute.Assertion
                .ForCondition(answer.LongAnswer == expectedLong)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected Answer to be a long '{0}'{reason}, but found '{1}'", expectedLong, answer.LongAnswer);
        }
        else
        {
            Execute.Assertion
                .FailWith("Expected object to be of type Answer, but found '{0}'", assertions.Subject.GetType());
        }

        return new AndConstraint<ObjectAssertions>(assertions);
    }
}