using MassTransit;
using MessageContracts;

namespace ExampleWorkerService.Infrastructure
{
    public class DeleteExamPaperConsumer : IConsumer<DeleteExamPaper>
    {
        public Task Consume(ConsumeContext<DeleteExamPaper> context)
        {
            return Task.CompletedTask;
        }
    }
}
