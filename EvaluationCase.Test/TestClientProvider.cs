using EvaluationCase.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace EvaluationCase.Test
{
    public class TestClientProvider : IDisposable
    {
        private TestServer _testServer;
        public HttpClient TestClient { get; private set; }

        public TestClientProvider()
        {
            SetUpContext();
        }

        private void SetUpContext()
        {
            var configDirectory = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json");

            _testServer = new TestServer(new WebHostBuilder().ConfigureAppConfiguration((appContext, configuration) =>
            {
                configuration.AddJsonFile(configDirectory);
            }).UseStartup<Startup>());

            TestClient = _testServer.CreateClient();
        }

        public void Dispose()
        {
            _testServer?.Dispose();
            TestClient?.Dispose();
        }
    }
}