namespace Validator.Expressions;

public record RangeExpression<TContract, TValue>(
    IEnumerable<TValue> Value, Contract<TContract> Contract
) : Expression<TContract>(Contract)
{
    private bool _allowNull;
    private int? _max;
    private int? _min;
    private bool inclusive;

    public RangeExpression(IEnumerable<TValue> value, Contract<TContract> contract, bool inclusive) : this(value,
        contract)
    {
        this.inclusive = inclusive;
    }

    public RangeExpression<TContract, TValue> Min(int? minLenght)
    {
        _min = minLenght;
        return this;
    }

    public RangeExpression<TContract, TValue> Max(int? maxLenght)
    {
        _max = maxLenght;
        return this;
    }


    public RangeExpression<TContract, TValue> OrNull()
    {
        _allowNull = true;
        return this;
    }


    public override IEnumerable<ValidationError> Validate()
    {
        if (_allowNull && !Value.Any())
            return ArraySegment<ValidationError>.Empty;

        var errors = new List<ValidationError>();
        if (_min != null && Value.Count() < _min)
        {
            errors.Add(new ValidationError("Min", ""));
        }

        if (_max != null && Value.Count() > _max)
        {
            errors.Add(new ValidationError("Min", ""));
        }

        return errors;
    }
}