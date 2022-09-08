using Domain.Entities;
using System.Text;

namespace Application.CQRS.Sample.Queries.GetAll;

public class GetAllSampleQueryHandler : IRequestHandler<GetAllSampleQuery, GlobalResponse<string>>
{
    private readonly IDistributedCache _cache;
    private readonly IMapper _mapper;

    public GetAllSampleQueryHandler(
        IDistributedCache cache,
        IMapper mapper)
    {
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<GlobalResponse<string>> Handle(GetAllSampleQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetAllSampleValidator();
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
            return await GlobalResponse<string>.FailAsync(
                (int)HttpStatusCode.ExpectationFailed,
                "Błąd walidacji danych",
                validatorResult.Errors.Select(s => s.ErrorMessage));

        var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(DateTime.Now.AddSeconds(30));

        var cacheExisted = await _cache.GetAsync("sample");

        if (cacheExisted is null)
        {
            var time = DateTime.Now.ToString();
            await _cache.SetAsync("sample", Encoding.UTF8.GetBytes(time), options, cancellationToken);

            return await GlobalResponse<string>.SuccessAsync(time);
        }

        var tmp = _mapper.Map<GetAllSampleDto>(new Audit() { TableName = "test" });

        return await GlobalResponse<string>.SuccessAsync(Encoding.UTF8.GetString(cacheExisted));
    }

}

