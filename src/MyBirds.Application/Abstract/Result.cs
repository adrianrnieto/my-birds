namespace MyBirds.Application.Abstract;

public class Result<T> where T : class
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; }
    public string? Error { get; }

    protected Result(bool isSuccess, T? value, string? error)
    {
        if (isSuccess && error != null)
            throw new InvalidOperationException();

        if (!isSuccess && value != null)
            throw new InvalidOperationException();

        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(string error) => new(false, default, error);
}
