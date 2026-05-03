namespace MyBirds.Application.Abstract;

/// <summary>
/// Extension methods for the Result pattern to provide fluent API usage.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Executes an action if the result is successful.
    /// </summary>
    public static Result OnSuccess(this Result result, Action action)
    {
        if (result.IsSuccess)
        {
            action();
        }

        return result;
    }

    /// <summary>
    /// Executes an action if the result is a failure.
    /// </summary>
    public static Result OnFailure(this Result result, Action<string> action)
    {
        if (result.IsFailure)
        {
            action(result.Error!);
        }

        return result;
    }

    /// <summary>
    /// Executes an async action if the result is successful.
    /// </summary>
    public static async Task<Result> OnSuccessAsync(this Result result, Func<Task> action)
    {
        if (result.IsSuccess)
        {
            await action();
        }

        return result;
    }

    /// <summary>
    /// Executes an async action if the result is a failure.
    /// </summary>
    public static async Task<Result> OnFailureAsync(this Result result, Func<string, Task> action)
    {
        if (result.IsFailure)
        {
            await action(result.Error!);
        }

        return result;
    }

    /// <summary>
    /// Maps a successful result to a new value.
    /// </summary>
    public static Result<TNew> Map<TNew>(this Result result, Func<TNew> mapping) where TNew : class
    {
        if (result.IsSuccess)
        {
            return Result<TNew>.Success(mapping());
        }

        return Result<TNew>.Failure(result.Error!);
    }

    /// <summary>
    /// Executes an action if the result is successful.
    /// </summary>
    public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action) where T : class
    {
        if (result.IsSuccess)
        {
            action(result.Value!);
        }

        return result;
    }

    /// <summary>
    /// Executes an action if the result is a failure.
    /// </summary>
    public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action) where T : class
    {
        if (result.IsFailure)
        {
            action(result.Error!);
        }

        return result;
    }

    /// <summary>
    /// Executes an async action if the result is successful.
    /// </summary>
    public static async Task<Result<T>> OnSuccessAsync<T>(this Result<T> result, Func<T, Task> action) where T : class
    {
        if (result.IsSuccess)
        {
            await action(result.Value!);
        }

        return result;
    }

    /// <summary>
    /// Executes an async action if the result is a failure.
    /// </summary>
    public static async Task<Result<T>> OnFailureAsync<T>(this Result<T> result, Func<string, Task> action) where T : class
    {
        if (result.IsFailure)
        {
            await action(result.Error!);
        }

        return result;
    }

    /// <summary>
    /// Maps a successful result to a new value.
    /// </summary>
    public static Result<TNew> Map<T, TNew>(this Result<T> result, Func<T, TNew> mapping) where T : class where TNew : class
    {
        if (result.IsSuccess)
        {
            return Result<TNew>.Success(mapping(result.Value!));
        }

        return Result<TNew>.Failure(result.Error!);
    }

    /// <summary>
    /// Flattens a nested result into a single result.
    /// </summary>
    public static Result<T> Flatten<T>(this Result<Result<T>> result) where T : class
    {
        if (result.IsFailure)
        {
            return Result<T>.Failure(result.Error!);
        }

        return result.Value!;
    }
}
