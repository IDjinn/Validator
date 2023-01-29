namespace Validator.Expressions;

public abstract partial record Expression
{
    public abstract IEnumerable<ValidationError> Validate();
}