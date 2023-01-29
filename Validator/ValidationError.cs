namespace Validator;

public readonly record struct ValidationError(
    string Key,
    string? Message
);