using System.Text.RegularExpressions;

namespace Validator.Expressions;

public partial record Email<TContract>(string? Value, Contract<TContract> Contract) where TContract:Contract<TContract>
{
    [GeneratedRegex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")]
    public static partial Regex email_regex();

    public Email<TContract> IsEmail(string? value, Regex regex, string key = "Email",
        string message = "Email field should be a valid email address.")
    {
        if (value is not string email || !regex.IsMatch(email))
            _errors.Add(new ValidationError(key,message));
        return this;
    }
    
    public Email<TContract> IsEmail(string? value)
    {
        return IsEmail(value, email_regex());
    }
}