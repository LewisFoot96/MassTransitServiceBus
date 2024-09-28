using ExampleWorkerService.Infrastructure;
using MassTransit.TestFramework.ForkJoint.Contracts;
using MassTransit.Testing;
using MessageContracts;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using static System.Net.Mime.MediaTypeNames;

namespace FunctionalTests
{
    [TestClass]
    public class ExampleFunctionalTest : FunctionalTestBase
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            await Task.Delay(10000);
            var consumerTestHarness = TestHarness.GetConsumerHarness<DeleteExamPaperConsumer>();
            Assert.IsTrue(await consumerTestHarness.Consumed.Any<DeleteExamPaper>(x => x.Context.Message.ExamName == "Test"));
        }
    }
}