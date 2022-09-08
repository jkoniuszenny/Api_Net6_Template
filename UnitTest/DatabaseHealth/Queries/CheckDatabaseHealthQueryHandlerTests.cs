using Application.CQRS.DatabaseHealth.Queries.CheckDatabaseHealth;
using Application.UnitTest.DatabaseHealth.Fixtures;
using AutoFixture;
using Shared.Enums;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.DatabaseHealth.Queries;

public class CheckDatabaseHealthQueryHandlerTests
{
    private readonly DatabaseHealthFixtures _fixture = new();

    public CheckDatabaseHealthQueryHandlerTests()
    {
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


