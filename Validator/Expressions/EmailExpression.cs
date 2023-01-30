using System.Text.RegularExpressions;
using Validator.Util;

namespace Validator.Expressions;

public partial record EmailExpression<TContract>(
    string? Value,
    Regex Regex,
    string Key,
    string Message,
    Contract<TContract> Contract
) : Expression<TContract>(Contract)
{
    private bool _allowNullOrEmpty;

    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    public static partial Regex email_regex();

    public override IEnumerable<ValidationError> Validate()
    {
        if (_allowNullOrEmpty && string.IsNullOrEmpty(Value))
            return Enumerable.Empty<ValidationError>();

        var errors = new List<ValidationError>();
        if (Value is null)
            errors.Add(Errors.Email.IsNull);
        else if (!Regex.Match(Value).Success)
            errors.Add(Errors.Email.IsInvalid);

        return errors;
    }

    public EmailExpression<TContract> OrNullOrEmpty()
    {
        _allowNullOrEmpty = true;
        return this;
    }
}