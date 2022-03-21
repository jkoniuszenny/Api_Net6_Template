using Application.Interfaces.Repositories;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.Fixtures;

public class BaseFixture : Fixture
{
    protected BaseFixture()
    {
        Customize(new AutoMoqCustomization());
        Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(behavior =>
            Behaviors.Remove(behavior));
        Behaviors.Add(new OmitOnRecursionBehavior());
    }

}

