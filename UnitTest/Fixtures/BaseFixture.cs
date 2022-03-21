using AutoFixture;
using AutoFixture.AutoMoq;
using System.Linq;

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

