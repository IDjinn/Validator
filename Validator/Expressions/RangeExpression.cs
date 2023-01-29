namespace Validator.Expressions;

public record LengthExpression<T>(T value) : IExpression
{
    private ulong min;
    private ulong max;
    
    public LengthExpression<T> Min(ulong minLenght)
    {
        min = minLenght;
        return this;
    }
    
    public LengthExpression<T> Max(ulong maxLenght)
    {
        max = maxLenght;
        return this;
    }
}