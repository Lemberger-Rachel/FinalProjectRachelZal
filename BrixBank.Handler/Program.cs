using BrixBank.Data;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BrixBank.Services.Interfaces;
using BrixBank.Services.Services;
using BrixBank.Data.Repositories;

namespace BrixBank.Handler
{
    public class Program
    {
        static async Task Main()
        {
            Console.Title = "Rules";
            var endpointConfiguration = new EndpointConfiguration("Rules");
            endpointConfiguration.EnableOutbox();
            var connection = "Server = ILRLEMBERGERLT; Database = BrixBankDB2‏; Trusted_Connection = True; MultipleActiveResultSets = true; ";
            var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
            var subscriptions = persistence.SubscriptionSettings();
            subscriptions.CacheFor(TimeSpan.FromMinutes(1));
            persistence.SqlDialect<SqlDialect.MsSqlServer>();
            persistence.ConnectionBuilder(
                connectionBuilder: () =>
                {
                    return new SqlConnection(connection);
                });

            var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
            transport.UseConventionalRoutingTopology();
            transport.ConnectionString("host= localhost:5672;username=guest;password=guest");
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.AuditProcessedMessagesTo("audit");

            var conventions = endpointConfiguration.Conventions();
            conventions.DefiningCommandsAs(type => type.Namespace == "BrixBank.Messages.Command");
            conventions.DefiningEventsAs(type => type.Namespace == "BrixBank.Messages.Event");

            var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
            containerSettings.ServiceCollection.AddScoped<IRuleRepository, RuleRepository>();
            containerSettings.ServiceCollection.AddScoped<IRuleService, RuleService>();
            containerSettings.ServiceCollection.AddDbContext<BrixBankContext>(options =>
                        options.UseSqlServer(connection));
            var endpointInstance = await Endpoint.Start(endpointConfiguration)
                .ConfigureAwait(false);

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();

            await endpointInstance.Stop()
                .ConfigureAwait(false);
        }
    }
}
