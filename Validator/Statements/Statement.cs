namespace Validator.Statements;

public abstract record Statement
{
    public abstract IEnumerable<ValidationError> Validate();
}