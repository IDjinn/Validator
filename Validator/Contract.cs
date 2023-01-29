using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;
using Validator.Expressions;
using Validator.Statements;

namespace Validator;

public partial record Contract<T>()
{
    private readonly List<Contract<T>> _contracts = new();
    private readonly List<ValidationError> _errors = new();
    private readonly List<Expression> _expressions = new();
    private readonly List<Statement> _statements = new();

    public IReadOnlyList<ValidationError> ValidationErrors
    {
        get => _errors.AsReadOnly();
    }

    public bool IsValid => !Validate().Any();

    public RangeExpression<T, char> LengthOf(string? value)
    {
        var enumerable = value?.AsEnumerable() ?? Enumerable.Empty<char>();
        var expression = new RangeExpression<T, char>(enumerable, this);
        _expressions.Add(expression);
        return expression;
    }

    public IReadOnlyList<ValidationError> Validate()
    {
        foreach (var statement in _statements)
        {
            _errors.AddRange(statement.Validate());
        }

        foreach (var expression in _expressions)
        {
            _errors.AddRange(expression.Validate());
        }

        foreach (var contract in _contracts)
        {
            _errors.AddRange(contract.Validate());
        }

        return _errors.AsReadOnly();
    }

    public Expression PopExpression()
    {
        Debug.Assert(_expressions.Count >= 1);

        var last = _expressions.Last();
        _expressions.RemoveAt(_expressions.Count - 1);
        return last;
    }

    public Contract<T> NewStatement(Statement statement)
    {
        _statements.Add(statement);
        return this;
    }

    public Contract<T> ThenIf()
    {
        var contract = new Contract<T>();
        _contracts.Add(contract);
        return contract;
    }

    public Contract<T> IsPositive<V>(V value) where V : INumber<V>
    {
        NewStatement(new TrueStatement(V.IsPositive(value)));
        return this;
    }

    public EmailExpression<T> IsEmail(string? value, Regex? regex = null, string key = "Email",
        string message = "Email field should be a valid email address.")
    {
        var email = new EmailExpression<T>(value, regex ?? EmailExpression<T>.email_regex(), key, message, this);
        _expressions.Add(email);
        return email;
    }

    public Contract<T> Or()
    {
        return this;
    }
}