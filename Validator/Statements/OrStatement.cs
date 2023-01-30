using Validator.Expressions;

namespace Validator.Statements;

public record OrStatement<TContract>(Expression<TContract>? Left) : Statement
{
    public Expression<TContract>? Right { get; set; }

    public override IEnumerable<ValidationError> Validate()
    {
        var leftIsError = Left?.Validate().Any() ?? false;
        var rightIsError = Right?.Validate().Any() ?? false;
        if (!leftIsError && !rightIsError)
            return Enumerable.Empty<ValidationError>();

        if (leftIsError && rightIsError)
        {
            return new ValidationError[] { new ValidationError("or", null) };
        }

        return Enumerable.Empty<ValidationError>();
    }
}