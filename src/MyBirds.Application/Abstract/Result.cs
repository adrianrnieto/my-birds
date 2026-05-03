namespace MyBirds.Application.Abstract;

/// <summary>
/// Represents the result of an operation without a return value.
/// Provides a consistent way to handle success and failure states.
/// </summary>
/// <remarks>
/// Use this class for operations that don't return a value. For operations with return values, use <see cref="Result{T}"/>.
/// </remarks>
public class Result
{
    /// <summary>
    /// Gets a value indicating whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Gets a value indicating whether the operation failed.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Gets the error message if the operation failed; otherwise null.
    /// </summary>
    public string? Error { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result"/> class.
    /// </summary>
    /// <param name="isSuccess">Whether the operation was successful.</param>
    /// <param name="error">The error message if failed.</param>
    /// <exception cref="InvalidOperationException">Thrown when success state and error message are inconsistent.</exception>
    protected Result(bool isSuccess, string? error)
    {
        if (isSuccess && error != null)
            throw new InvalidOperationException("Success result cannot have an error message.");

        if (!isSuccess && string.IsNullOrWhiteSpace(error))
            throw new InvalidOperationException("Failure result must have an error message.");

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A successful result.</returns>
    public static Result Success() => new(true, null);

    /// <summary>
    /// Creates a failed result with the specified error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>A failed result.</returns>
    public static Result Failure(string error) => new(false, error);
}

/// <summary>
/// Represents the result of an operation with a return value.
/// Provides a consistent way to handle success and failure states with associated data.
/// </summary>
/// <typeparam name="T">The type of the successful result value. Must be a reference type.</typeparam>
/// <remarks>
/// Use this class for operations that return a value. For void operations, use <see cref="Result"/>.
/// </remarks>
public class Result<T> : Result where T : class
{
    /// <summary>
    /// Gets the value if the operation was successful; otherwise null.
    /// </summary>
    /// <remarks>
    /// This value is guaranteed to be non-null when <see cref="Result.IsSuccess"/> is true.
    /// </remarks>
    public T? Value { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Result{T}"/> class.
    /// </summary>
    /// <param name="isSuccess">Whether the operation was successful.</param>
    /// <param name="value">The value if successful.</param>
    /// <param name="error">The error message if failed.</param>
    /// <exception cref="InvalidOperationException">Thrown when success state, value, and error message are inconsistent.</exception>
    protected Result(bool isSuccess, T? value, string? error) : base(isSuccess, error)
    {
        if (isSuccess && value == null)
            throw new InvalidOperationException("Success result must have a value.");

        Value = value;
    }

    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    /// <param name="value">The value to return.</param>
    /// <returns>A successful result containing the value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when value is null.</exception>
    public static Result<T> Success(T value)
    {
        if (value == null)
            throw new ArgumentNullException(nameof(value), "Success result value cannot be null.");

        return new(true, value, null);
    }

    /// <summary>
    /// Creates a failed result with the specified error message.
    /// </summary>
    /// <param name="error">The error message.</param>
    /// <returns>A failed result.</returns>
    public static new Result<T> Failure(string error) => new(false, null, error);
}
