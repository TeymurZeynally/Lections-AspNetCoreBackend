using Lecture04.DI.Notifications;
using Lecture04.DI.Repositories;
using Lecture04.DI.Services;
using Lecture04.DI.Utility;
using Moq;

namespace Lecture04.DI.UnitTests.Services
{
    public class CatFeederServiceTests
    {
        private readonly Mock<IConsoleLogger> _logger;
        private readonly Mock<INotifier> _notifier;
        private readonly Mock<IFoodRepository> _foodRepository;

        private readonly CatFeederService _target;

        public CatFeederServiceTests()
        {
            _logger = new Mock<IConsoleLogger>();
            _notifier = new Mock<INotifier>();
            _foodRepository = new Mock<IFoodRepository>();

            _target = new CatFeederService(_logger.Object, _notifier.Object, _foodRepository.Object);
        }

        [Fact]
        public void FeedCat_LogsAndNotifies_WithFoodFromRepository()
        {
            // arrange
            var catName = "Barsik";
            var food = "Fish";

            _foodRepository.Setup(r => r.GetFoodFor(catName)).Returns(food);
            _logger.Setup(l => l.Log($"Feeding {catName} with {food}"));
            _notifier.Setup(n => n.Notify($"Cat {catName} has been fed: {food}"));

            // act
            _target.FeedCat(catName);

            // assert
            _foodRepository.Verify(r => r.GetFoodFor(catName), Times.Once);
            _logger.Verify(l => l.Log($"Feeding {catName} with {food}"), Times.Once);
            _notifier.Verify(n => n.Notify($"Cat {catName} has been fed: {food}"), Times.Once);

            _foodRepository.VerifyNoOtherCalls();
            _logger.VerifyNoOtherCalls();
            _notifier.VerifyNoOtherCalls();
        }
    }
}
