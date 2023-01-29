namespace Validator.Statements;

public record TrueStatement(bool Value) : Statement
{
    public override IEnumerable<ValidationError> Validate()
    {
        return Value
            ? Enumerable.Empty<ValidationError>()
            : new List<ValidationError>() { new ValidationError("true", null) };
    }
}