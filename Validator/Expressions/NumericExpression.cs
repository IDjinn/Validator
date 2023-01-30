using System.Numerics;
using Validator.Statements;
using Validator.Util;

namespace Validator.Expressions;

public partial record NumericExpression<TContract, TValue>(
    TValue? Value,
    Contract<TContract> Contract
) : Expression<TContract>(Contract) where TValue : INumber<TValue>
{
    private readonly IList<ValidationError> _errors = new List<ValidationError>();

    public NumericExpression<TContract, TValue> IsPositive(string? key = null)
    {
        if (Value is null)
            _errors.Add(Errors.Numbers.ExpectingPositive);
        else
            Contract.PushStatement(new TrueStatement(TValue.IsPositive(Value)));
        return this;
    }

    public NumericExpression<TContract, TValue> IsNegative(string? key = null)
    {
        if (Value is null)
            _errors.Add(Errors.Numbers.ExpectingNegative);
        else
            Contract.PushStatement(new TrueStatement(TValue.IsNegative(Value)));
        return this;
    }

    public new NumericExpression<TContract, TValue> Or()
    {
        var left = (NumericExpression<TContract, TValue>)Contract.PopExpression();
        var _ = Contract.PopStatement(); // useless true statement
        var right = new NumericExpression<TContract, TValue>(Value, Contract.ThenIf());
        var or = new OrStatement<TContract>(left)
        {
            Right = right
        };

        Contract.PushStatement(or);
        return right;
    }

    public override IEnumerable<ValidationError> Validate()
    {
        return _errors;
    }
}