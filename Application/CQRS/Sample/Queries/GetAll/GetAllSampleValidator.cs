using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Sample.Queries.GetAll;

public class GetAllSampleValidator : AbstractValidator<GetAllSampleQuery>
{
    public GetAllSampleValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThanOrEqualTo(1);
    }
}
