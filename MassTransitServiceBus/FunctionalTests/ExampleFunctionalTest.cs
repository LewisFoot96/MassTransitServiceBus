using ExampleWorkerService.Infrastructure;
using MessageContracts;

namespace FunctionalTests
{
    [TestClass]
    public class ExampleFunctionalTest : FunctionalTestBase
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            //Can also test database interactions
            await Task.Delay(10000);
            var consumerTestHarness = TestHarness.GetConsumerHarness<DeleteExamPaperConsumer>();
            Assert.IsTrue(await consumerTestHarness.Consumed.Any<DeleteExamPaper>(x => x.Context.Message.ExamName == "Test"));

            //Test publish message. 
            await TestHarness.Bus.Publish(new DeleteExamPaper
            { ExamName = "NewTest"});

            Assert.IsTrue(await consumerTestHarness.Consumed.Any<DeleteExamPaper>(x => x.Context.Message.ExamName == "NewTest"));

        }
    }
}