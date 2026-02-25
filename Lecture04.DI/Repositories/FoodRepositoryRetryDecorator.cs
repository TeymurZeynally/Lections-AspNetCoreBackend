using System;
using System.Collections.Generic;
using System.Text;

namespace Lecture04.DI.Repositories
{
    internal class FoodRepositoryRetryDecorator : IFoodRepository
    {
        private readonly IFoodRepository _foodRepository;

        public FoodRepositoryRetryDecorator(IFoodRepository repository) => _foodRepository = repository;

        public string GetFoodFor(string catName)
        {
            //retry logic

            return _foodRepository.GetFoodFor(catName);
        }
    }
}
