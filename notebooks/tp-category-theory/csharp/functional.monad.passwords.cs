using System;
using System.Linq;

namespace Passwrods
{
    internal static class Program
    {
        public static void Main(string[] args)
        {
            Func<char, bool> isSpecial = x => (new[] { '$', '#', '@' }).Contains(x);

            ResultFunc<string, string> hasUpper = text => text.Check(x => x.Any(char.IsUpper));
            ResultFunc<string, string> hasLower = text => text.Check(x => x.Any(char.IsLower));
            ResultFunc<string, string> hasDigit = text => text.Check(x => x.Any(char.IsDigit));
            ResultFunc<string, string> hasSpecial = text => text.Check(x => x.Any(isSpecial));
            ResultFunc<string, string> greaterThan = text => text.Check(x => x.Length >= 6);
            ResultFunc<string, string> lessThan = text => text.Check(x => x.Length <= 12);

            ResultFunc<string, string> checkPassword = hasUpper
                .Compose(hasLower)
                .Compose(hasDigit)
                .Compose(hasSpecial)
                .Compose(greaterThan)
                .Compose(lessThan);

            string result = Console.ReadLine()
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(Result.Ok)
                .Select(checkPassword.Invoke)
                .Where(Result.IsOk)
                .Select(Result.Match)
                .Aggregate((left, right) => $"{left},{right}");

            Console.WriteLine(result);
        }
    }

    public class Result<T>
    {
        public Result(T value) { Value = value; }

        public T Value { get; }
    }

    public sealed class OkResult<T> : Result<T>
    {
        public OkResult(T value) : base(value) { }
    }

    public sealed class ErrorResult : Result<string>
    {
        public ErrorResult(string message) : base(message) { }
    }

    public static class Result
    {
        public static Result<T> Ok<T>(T value) => new OkResult<T>(value);

        public static Result<string> Error(string message) => new ErrorResult(message);

        public static bool IsOk<T>(this Result<T> result) => result is OkResult<T>;

        public static bool IsError<T>(this Result<T> result) => result is ErrorResult;

        public static T Match<T>(this Result<T> value)
        {
            switch (value)
            {
                case OkResult<T> okay:
                    return okay.Value;
                case ErrorResult error:
                    return default;
                default:
                    return default;
            }
        }

        public static Result<string> Check(this Result<string> text, Func<string, bool> func)
        {
            switch (text)
            {
                case OkResult<string> okay:
                    return Check(okay.Value, func);
                case ErrorResult error:
                    return error;
                default:
                    return text;
            }
        }

        private static Result<string> Check(string value, Func<string, bool> condition)
        {
            return condition(value) ? Ok(value) : Error("Value does not match a given condition.");
        }
    }

    public delegate Result<T2> ResultFunc<T1, T2>(Result<T1> value);

    public static class ResultFuncExtensions
    {
        public static ResultFunc<T1, T3> Compose<T1, T2, T3>(this ResultFunc<T1, T2> left, ResultFunc<T2, T3> right)
        {
            return new ResultFunc<T1, T3>(value => right(left(value)));
        }
    }
}
