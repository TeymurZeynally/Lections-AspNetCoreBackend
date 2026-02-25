using Lecture04.DI.Notifications;
using Lecture04.DI.Repositories;
using Lecture04.DI.Utility;

namespace Lecture04.DI.Services
{
    public class CatFeederService : ICatFeederService
    {
        private readonly IConsoleLogger _consoleLogger;
        private readonly INotifier _notifier;
        private readonly IFoodRepository _foodRepository;

        public CatFeederService(IConsoleLogger consoleLogger, INotifier notifier, IFoodRepository foodRepository)
        {
            Console.WriteLine($"Создался класс CatFeederService {new Random().Next(0, int.MaxValue)}");

            _consoleLogger = consoleLogger;
            _notifier = notifier;
            _foodRepository = foodRepository;
        }

        public void FeedCat(string catName)
        {
            var food = _foodRepository.GetFoodFor(catName);
            _consoleLogger.Log($"Feeding {catName} with {food}");

            _notifier.Notify($"Cat {catName} has been fed: {food}");
        }
    }
}
