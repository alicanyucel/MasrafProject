using MediatR;
using Microsoft.Extensions.Caching.Memory;
namespace MasrafProject.Application.Behaviors;

public interface ICacheableQuery
{
    string CacheKey { get; }
    TimeSpan CacheDuration { get; }
}

public sealed class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheableQuery, IRequest<TResponse>
{
    private readonly IMemoryCache _cache;

    public CachingBehavior(IMemoryCache cache)
    {
        _cache = cache;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(request.CacheKey, out var cachedObj) && cachedObj is TResponse cachedResponse)
            return cachedResponse;
        
        var response = await next();
        _cache.Set(request.CacheKey, response, request.CacheDuration);
        return response;
    }
}


