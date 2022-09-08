using Application.CQRS.Sample.Queries.GetAll;
using Domain.Entities;

namespace Application.CQRS.Sample.Mapper
{
    public class SampleMappingProfile : Profile
    {
        public SampleMappingProfile()
        {
            CreateMap<Audit, GetAllSampleDto>()
                .ReverseMap();
        }
    }
}
