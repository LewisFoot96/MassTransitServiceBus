using MassTransit;
using MassTransit.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalTests
{
    public abstract class FunctionalTestBase
    {
        private IServiceProvider ServiceProvider { get; set; }
        public ITestHarness TestHarness { get; }

        private readonly WebApplicationFactory<Program> _webApplicationFactory;
        public readonly HttpClient TestClient;

        protected FunctionalTestBase()
        {
            if (_webApplicationFactory is null)
            {
                _webApplicationFactory = new WebApplicationFactory<Program>()
                    .WithWebHostBuilder(
                        builder =>
                        {
                            builder.UseEnvironment(Environments.Development);
                            builder.ConfigureServices(services =>
                            {
                                services.AddMassTransitTestHarness();
                            });
                            //Using a Worker Service that doesn't run a web host. However, the WebApplicationFactory still expects one, So creating an empty web application.
                            builder.Configure(_ => { });
                        });
                TestClient = _webApplicationFactory.CreateClient();
                ServiceProvider = _webApplicationFactory.Services;
                TestHarness = _webApplicationFactory.Services.GetTestHarness();
            }
        }
    }
}
