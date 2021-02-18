using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace OSIsoft.PISystemDeploymentTests
{
    /// <summary>
    /// This class tests various features of the Edge Data Store
    /// </summary>
    public class EdgeDataStoreTests : IClassFixture<EdgeDataStoreFixture>
    {
        internal const string KeySetting = "EdgeDataStoreTests";
        internal const string PortSetting = "EdgeDataStorePort";
        internal const TypeCode KeySettingTypeCode = TypeCode.Boolean;

        /// <summary>
        /// Constructor for EdgeDataStoreTests class.
        /// </summary>
        /// <param name="output">The output logger used for writing messages.</param>
        /// <param name="fixture">Fixture to manage Edge Data Store connection and specific helper functions.</param>
        public EdgeDataStoreTests(ITestOutputHelper output, EdgeDataStoreFixture fixture)
        {
            Output = output;
            Fixture = fixture;
        }

        private EdgeDataStoreFixture Fixture { get; }
        private ITestOutputHelper Output { get; }

        /// <summary>
        /// Verifies that the Edge Data Store configuration can be retrieved.
        /// </summary>
        [OptionalFact(KeySetting, KeySettingTypeCode)]
        public void ConfigurationTest()
        {
            string url = "configuration";
            Output.WriteLine($"Verifying the Edge Data Store configuration can be retrieved from {Fixture.Client.BaseAddress}{url}.");
            string content = Fixture.Client.DownloadString(url);
            Assert.True(!string.IsNullOrEmpty(content), "Failed to get local Edge Data Store configuration.");
        }

        /// <summary>
        /// Verifies Edge Data Store REST endpoints return a response.
        /// </summary>
        /// <param name="path">The REST endpoint to verify</param>
        [OptionalTheory(KeySetting, KeySettingTypeCode)]
        [InlineData("configuration")]
        [InlineData("tenants/default/namespaces/default/types")]
        [InlineData("tenants/default/namespaces/default/streams")]
        [InlineData("tenants/default/namespaces/diagnostics/types")]
        [InlineData("tenants/default/namespaces/diagnostics/streams")]
        public void EndpointsTest(string path)
        {
            Output.WriteLine($"Verifying the Edge Data Store response can be retrieved from {Fixture.Client.BaseAddress}{path}.");
            string content = Fixture.Client.DownloadString(path);
            Output.WriteLine("Response:");
            Output.WriteLine(content);
            Assert.True(!string.IsNullOrEmpty(content), "Failed to get response from Edge Data Store.");
        }
    }
}
