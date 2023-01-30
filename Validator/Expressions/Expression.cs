using Validator.Statements;

namespace Validator.Expressions;

public abstract partial record Expression<TContract>(Contract<TContract> Contract)
{
    public abstract IEnumerable<ValidationError> Validate();

    public Contract<TContract> Or()
    {
        return Contract.PushStatement(new OrStatement<TContract>(Contract.PopExpression()));
    }

    public Contract<TContract> ThenIf()
    {
        return Contract.ThenIf();
    }
}