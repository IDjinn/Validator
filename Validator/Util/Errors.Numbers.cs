namespace Validator.Util;

public static partial class Errors
{
    public static partial class Numbers
    {
        public static readonly ValidationError ExpectingPositive =
            new ValidationError("Value", "Value field must be a positive number.");

        public static readonly ValidationError ExpectingNegative =
            new ValidationError("Value", "Value field must be a negative number.");
    }
}