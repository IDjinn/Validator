using System.Text.RegularExpressions;

namespace Validator.Expressions;

public partial record EmailExpression<TContract>(
    string? Value,
    Regex Regex,
    string Key,
    string Message,
    Contract<TContract> Contract
) : Expression
{
    private bool allowNullOrEmpty;

    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    public static partial Regex email_regex();

    public override IEnumerable<ValidationError> Validate()
    {
        if (allowNullOrEmpty && string.IsNullOrEmpty(Value))
            return Enumerable.Empty<ValidationError>();

        var errors = new List<ValidationError>();
        if (Value is null)
            errors.Add(new ValidationError("Email", "Email is null."));
        else if (!Regex.Match(Value).Success)
            errors.Add(new ValidationError("Email", "Email is not valid."));

        return errors;
    }

    public EmailExpression<TContract> OrNullOrEmpty()
    {
        allowNullOrEmpty = true;
        return this;
    }
}