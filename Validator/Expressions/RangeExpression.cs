using Validator.Statements;

namespace Validator.Expressions;

public record RangeExpression<TContract, TValue>(IEnumerable<TValue> Value, Contract<TContract> Contract) : Expression
{
    private bool allowNull;
    private bool inclusive;
    private int? max;
    private int? min;

    public RangeExpression<TContract, TValue> Min(int? minLenght)
    {
        min = minLenght;
        return this;
    }

    public RangeExpression<TContract, TValue> Max(int? maxLenght)
    {
        max = maxLenght;
        return this;
    }


    public RangeExpression<TContract, TValue> OrNull()
    {
        allowNull = true;
        return this;
    }

    public Contract<TContract> Or()
    {
        return Contract.NewStatement(new OrStatement(Contract.PopExpression()));
    }

    public Contract<TContract> ThenIf()
    {
        return Contract.ThenIf();
    }

    public override IEnumerable<ValidationError> Validate()
    {
        if (allowNull && !Value.Any())
            return ArraySegment<ValidationError>.Empty;

        var errors = new List<ValidationError>();
        if (min != null && Value.Count() < min)
        {
            errors.Add(new ValidationError("Min", ""));
        }

        if (max != null && Value.Count() > max)
        {
            errors.Add(new ValidationError("Min", ""));
        }

        return errors;
    }
}