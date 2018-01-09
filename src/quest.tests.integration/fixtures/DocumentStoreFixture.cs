using Marten;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace quest.tests.integration
{
    public class DocumentStoreFixture
    {
        public DocumentStoreFixture()
        {
            var configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection();
            services.AddMarten(configuration);
            DocumentStore = services.BuildServiceProvider().GetService<IDocumentStore>();
        }

        public IDocumentStore DocumentStore { get; }
    }
}