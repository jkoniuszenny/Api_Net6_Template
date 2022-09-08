namespace Application.CQRS.Sample.Queries.GetAll;

public class GetAllSampleQuery : IRequest<GlobalResponse<string>>
{
    public int Id { get; set; }

}