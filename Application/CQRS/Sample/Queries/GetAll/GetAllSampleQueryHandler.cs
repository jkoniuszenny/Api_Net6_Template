using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Shared.GlobalResponse;
using System.Net;
using System.Text;

namespace Application.CQRS.Sample.Queries.GetAll;

public class GetAllSampleQueryHandler : IRequestHandler<GetAllSampleQuery, GlobalResponse<string>>
{
    private readonly IDistributedCache _cache;

    public GetAllSampleQueryHandler(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<GlobalResponse<string>> Handle(GetAllSampleQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetAllSampleValidator();
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
            return await GlobalResponse<string>.FailAsync(
                (int)HttpStatusCode.ExpectationFailed,
                "Błąd walidacji danych",
                validatorResult.Errors.Select(s=>s.ErrorMessage));

        var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddSeconds(30));

        var cacheExisted = await _cache.GetAsync("sample");

        if (cacheExisted is null)
        {
            var time = DateTime.Now.ToString();
            await _cache.SetAsync("sample", Encoding.UTF8.GetBytes(time), options, cancellationToken);

            return await GlobalResponse<string>.SuccessAsync(time);
        }

        return await GlobalResponse<string>.SuccessAsync(Encoding.UTF8.GetString(cacheExisted));
    }

}

