namespace Agri.Shared.Result;

public static class ResultExtensions
{
    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFailure) =>
        result.IsSuccess
        ? onSuccess()
        : onFailure(result);

    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFailure) =>
        result.IsSuccess
        ? onSuccess(result.Value)
        : onFailure(result);

    public static Result<T> Wrap<T>(T value) where T : class => Result.Success(value);
    public static Result<T> AsResult<T>(this T value)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(value);

        return Result.Success(value);
    }
    public static Result<T> OnSuccess<T>(this Result<T> result, Func<T, T> func)
    {
        if (result.IsFailure)
            return result;

        return Result.Success(func(result.Value));
    }
    public static Result<T> Ensure<T>(this Result<T> result, Predicate<T> predicate, Error error)
    {
        if (result.IsFailure)
            return result;

        if (!predicate(result.Value))
            return Result.Failure<T>(error);

        return result;
    }
    public static Result Tap(this Result result, Action action)
    {
        if (result.IsSuccess)
            action();

        return result;
    }
    public static Result Tap<T>(this Result<T> result, Action<T> action)
    {
        if (result.IsSuccess)
            action(result.Value);

        return result;
    }
    public static Result<K> Map<T, K>(this Result<T> result, Func<T, K> func)
    {
        if (result.IsFailure)
            return Result.Failure<K>(result.Error);

        return Result.Success(func(result.Value));
    }
    public static Result<K> Bind<T, K>(this Result<T> result, Func<T, Result<K>> func)
    {
        if (result.IsFailure)
            return Result.Failure<K>(result.Error);

        var operation = func(result.Value);
        return operation;
    }
    public static Result<T> OnCondition<T>(this Result<T> result, bool condition, Func<T, Result> func)
    {
        if (result.IsFailure)
            return result;

        if (condition)
        {
            var operation = func(result.Value);
            return operation.IsSuccess
                ? Result.Success(result.Value)
                : Result.Failure<T>(operation.Error);
        }

        return result;
    }
    public static Result<T> OnCondition<T>(this Result<T> result, Predicate<T> predicate, Action<T> action)
    {
        if (result.IsFailure)
            return result;

        if (predicate(result.Value))
            action(result.Value);

        return result;
    }

    public static Result<T> OnCondition<T>(this Result<T> result, Predicate<T> predicate, Func<T, Result> func)
    {
        if (result.IsFailure)
            return result;

        if (predicate(result.Value))
        {
            var operation = func(result.Value);
            return operation.IsSuccess
                ? Result.Success(result.Value)
                : Result.Failure<T>(operation.Error);
        }
        return result;
    }
    #region Async
    public static async Task<Result<K>> OnSuccessAsync<T, K>(this Task<Result<T>> task, Func<Result<K>> func)
    {
        var result = await task;
        if (result.IsFailure)
            return Result.Failure<K>(result.Error);

        var operation = func();
        return result.IsSuccess
            ? Result.Success(operation.Value)
            : Result.Failure<K>(operation.Error);
    }
    public static async Task<Result<T>> OnSuccessAsync<T>(this Task<Result<T>> task, Func<T, Task> func)
    {
        var result = await task;
        if (result.IsFailure)
            return result;

        await func(result.Value);

        return result;
    }
    public static async Task<Result<T>> OnSuccessAsync<T>(this Task<Result> task, Func<Task<Result<T>>> func)
    {
        var result = await task;
        if (result.IsFailure)
            return Result.Failure<T>(result.Error);

        return await func();
    }
    public static async Task<Result<K>> MapAsync<T, K>(this Result<T> result, Func<T, Task<K>> func)
    {
        if (result.IsFailure)
            return Result.Failure<K>(result.Error);

        return Result.Success(await func(result.Value));
    }
    public static async Task<Result<K>> MapAsync<T, K>(this Task<Result<T>> task, Func<T, K> func)
    {
        var result = await task;
        if (result.IsFailure)
            return Result.Failure<K>(result.Error);

        return Result.Success(func(result.Value));
    }
    public static async Task<Result<K>> MapAsync<K>(this Task<Result> task, Func<K> func)
    {
        var result = await task;
        if (result.IsFailure)
            return Result.Failure<K>(result.Error);

        return Result.Success(func());
    }
    public static async Task<Result<K>> BindAsync<T, K>(this Result<T> result, Func<T, Task<Result<K>>> func)
    {
        if (result.IsFailure)
            return Result.Failure<K>(result.Error);

        var operation = await func(result.Value);

        return operation.IsSuccess
            ? Result.Success(operation.Value)
            : Result.Failure<K>(operation.Error);
    }
    public static async Task<Result<T>> TapAsync<T>(this Task<Result<T>> task, Action<T> action)
    {
        var result = await task;
        if (result.IsSuccess)
            action(result.Value);

        return result;
    }
    public static async Task<Result<T>> TapAsync<T>(this Task<Result<T>> task, Func<Task> action)
    {
        var result = await task;
        if (result.IsSuccess)
            await action();

        return result;
    }
    public static async Task<Result<T>> TapAsync<T>(this Result<T> result, Task<Action> action)
    {
        if (result.IsSuccess)
            await action;

        return result;
    }
    public static async Task<Result<T>> TapAsync<T>(this Result<T> result, Func<T, Task> action)
    {
        if (result.IsSuccess)
            await action(result.Value);

        return result;
    }
    public static async Task<Result> TapAsync(this Task<Result> task, Action action)
    {
        var result = await task;
        if (result.IsSuccess)
            action();

        return result;
    }
    public static async Task<Result<T>> EvenAsync<T>(this Result<T> result, Func<Task<Result<T>>> func)
    {
        if (result.IsSuccess)
            return result;

        var operation = await func();

        return operation.IsSuccess
           ? Result.Success(operation.Value)
           : Result.Failure<T>(operation.Error);
    }
    public static async Task<Result<T>> EnsureAsync<T>(this Task<Result<T>> task, Predicate<T> predicate, Error error)
    {
        var result = await task;
        if (result.IsFailure)
            return result;

        if (!predicate(result.Value))
            return Result.Failure<T>(error);

        return result;
    }
    public static async Task<Result> ToResultAsync<T>(this Task<Result<T>> task)
    {
        var result = await task;
        if (result.IsFailure)
            return Result.Failure(result.Error);

        return Result.Success();
    }
    #endregion
}