using Application.Interfaces.Repositories;
using Application.UnitTest.Fixtures;
using AutoFixture;
using Moq;
using Shared.Enums;

namespace Application.UnitTest.DatabaseHealth.Fixtures;

public class DatabaseHealthFixtures : BaseFixture
{

    public void SetUpHealthRepository()
    {
        this.Freeze<Mock<IHealthyRepository>>()
            .Setup(s => s.GetHealthyAsync())
            .ReturnsAsync(nameof(DatabaseHealthy.Healthy));
    }
}

