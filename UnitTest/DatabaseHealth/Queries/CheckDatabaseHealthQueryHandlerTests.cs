using Application.CQRS.DatabaseHealth.Queries.Check;
using Application.CQRS.Sample.Mapper;
using Application.Interfaces.Repositories;
using Application.UnitTest.DatabaseHealth.Fixtures;
using Application.UnitTest.Fixtures;
using AutoFixture;
using AutoMapper;
using Shared.Enums;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.DatabaseHealth.Queries;

public class CheckDatabaseHealthQueryHandlerTests
{
    private readonly IMapper _mapper;
    private readonly DatabaseHealthFixtures _fixture = new();

    public CheckDatabaseHealthQueryHandlerTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<SampleMappingProfile>();
            }
        );

        _mapper = configurationProvider.CreateMapper();
    }

    [Fact]
    public async Task CheckDatabaseHealthQueryHandlerTests_Repository_Mock_Valid()
    {
        // Arrange
        _fixture.SetUpHealthRepository();

        var handler = _fixture.Create<CheckDatabaseHealthQueryHandler>();

        // Act
        var result = await handler.Handle(new CheckDatabaseHealthQuery(), CancellationToken.None);

        //Assert
        result.ShouldBe(nameof(DatabaseHealthy.Healthy));
    }
}


