namespace Validator.Expressions;

public abstract class IExpression
{
    public IEnumerable<ValidationError> Validate();
}