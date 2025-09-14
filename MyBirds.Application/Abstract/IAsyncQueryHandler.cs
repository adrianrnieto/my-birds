namespace MyBirds.Application.Abstract;

public interface IAsyncQueryHandler<TResult>
    where TResult : class, IQueryResult
{
    Task<Result<TResult>> HandleAsync(CancellationToken cancellationToken);
}

public interface IAsyncQueryHandler<in TQuery, TResult>
    where TQuery : IQuery
    where TResult : class, IQueryResult
{
    Task<Result<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken);
}