using Xunit;

namespace MinimalApiAndRespawn.Tests.Integration
{
    [CollectionDefinition("TestCollection")]
    public class TestCollection : ICollectionFixture<ApiFactory>
    {
    }
}
