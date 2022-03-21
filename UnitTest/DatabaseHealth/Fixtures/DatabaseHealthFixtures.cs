using Application.Interfaces.Repositories;
using Application.UnitTest.Fixtures;
using AutoFixture;
using Moq;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

