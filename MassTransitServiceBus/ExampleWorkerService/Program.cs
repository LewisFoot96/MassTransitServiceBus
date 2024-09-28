using Azure.Messaging.ServiceBus.Administration;
using ExampleWorkerService;
using ExampleWorkerService.Infrastructure;
using MassTransit;
using MessageContracts;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddMassTransit(config =>
{
    config.SetKebabCaseEndpointNameFormatter();

    config.AddConsumers(typeof(DeleteExamPaperConsumer).Assembly);

    config.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host("connection string to azure service bus");

        cfg.ConfigureEndpoints(context);


        cfg.Publish<CreateExamPaper>(x =>
        {
            x.EnablePartitioning = true;
        });

        cfg.SubscriptionEndpoint<DeleteExamPaper>("subscription-name", e =>
        {
            e.Filter = new SqlRuleFilter("1 = 1");

            e.ConfigureConsumer<DeleteExamPaperConsumer>(context);
        });
    });
});


var host = builder.Build();
host.Run();

public partial class Program { }
