namespace Application.CQRS.Sample.Queries.GetAll;

public class GetAllSampleValidator : AbstractValidator<GetAllSampleQuery>
{
    public GetAllSampleValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThanOrEqualTo(1);
    }
}
