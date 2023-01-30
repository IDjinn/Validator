namespace Validator.Tests;

public class ContractTests
{
    [Theory]
    [InlineData(-100)]
    [InlineData(-2324)]
    [InlineData(-43425000)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-4000)]
    public void check_positive_or_negative_values(int value)
    {
        var validContract = new Contract<bool>();
        validContract
            .Number(value)
            .IsPositive()
            .Or()
            .IsNegative();
        Assert.True(validContract.IsValid);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-4000)]
    public void check_invalid_positive_values(int value)
    {
        var validContract = new Contract<bool>();
        validContract
            .Number(value)
            .IsPositive();
        Assert.False(validContract.IsValid);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(4000)]
    public void check_valid_positive_values(int value)
    {
        var validContract = new Contract<bool>();
        validContract
            .Number(value)
            .IsPositive();
        Assert.True(validContract.IsValid);
    }

    [Theory]
    [InlineData("djinn", 3, 15)]
    [InlineData("djinn", 4, 35)]
    [InlineData(null, 4, 35)]
    public void check_valid_range_expression_or_null_for_strings(string? value, int? min, int? max)
    {
        var validContract = new Contract<bool>();
        validContract
            .LengthOf(value)
            .Max(max)
            .Min(min)
            .OrNull();
        Assert.True(validContract.IsValid);
    }

    [Theory]
    [InlineData("djinn", 3, 15)]
    [InlineData("djinn", 4, 35)]
    public void check_valid_range_expressions_for_strings(string? value, int? min, int? max)
    {
        var validContract = new Contract<bool>();
        validContract
            .LengthOf(value)
            .Max(max)
            .Min(min);
        Assert.True(validContract.IsValid);
    }

    [Theory]
    [InlineData(null, 3, 15)]
    [InlineData("djinn", 10, 35)]
    [InlineData("djinn", 1, 3)]
    public void check_invalid_range_expressions_for_strings(string? value, int? min, int? max)
    {
        var invalidContract = new Contract<bool>();
        invalidContract
            .LengthOf(value)
            .Max(max)
            .Min(min);
        Assert.False(invalidContract.IsValid);
    }

    [Theory]
    [InlineData("djinn@gmail.com")]
    public void check_valid_email(string value)
    {
        var validContract = new Contract<bool>();
        validContract
            .IsEmail(value);

        Assert.True(validContract.IsValid);
    }

    [Theory]
    [InlineData("djinn@gmail.com")]
    [InlineData(null)]
    public void check_valid_email_or_null(string value)
    {
        var validContract = new Contract<bool>();
        validContract
            .IsEmail(value)
            .OrNullOrEmpty();

        Assert.True(validContract.IsValid);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("null")]
    [InlineData(".com")]
    public void check_invalid_email(string? value)
    {
        var validContract = new Contract<bool>();
        validContract
            .IsEmail(value);

        Assert.False(validContract.IsValid);
    }
}