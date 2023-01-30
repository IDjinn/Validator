namespace Validator.Util;

public static partial class Errors
{
    public static partial class Email
    {
        public static readonly ValidationError IsNull = new ValidationError("Email", "Email cannot be null,");
        public static readonly ValidationError IsInvalid = new ValidationError("Email", "Email field is not valid.");
    }
}