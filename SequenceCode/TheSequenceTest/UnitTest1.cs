using TheSequenceSystem;

namespace TheSequenceTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestStartGame()
        {
            Sequence sequence = new();
            sequence.StartGame();
            string msg = $"game status = {sequence.GameStatus.ToString()}";
            Assert.IsTrue(sequence.GameStatus == Sequence.GameStatusEnum.start, msg);
            TestContext.WriteLine(msg);
        }
    }
}