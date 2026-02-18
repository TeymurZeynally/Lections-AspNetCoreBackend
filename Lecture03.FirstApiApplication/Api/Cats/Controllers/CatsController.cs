using Lecture03.FirstApiApplication.Api.Cats.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Lecture03.FirstApiApplication.Api.Cats.Controllers
{
    [Route("api/cats")]
    [ApiController]
    public class CatsController : ControllerBase
    {
        [HttpPost]
        public ActionResult Create([FromBody] CatUpsertRequestContract contract)
        {
            var nextId = _cats.Count == 0 ? 1 : _cats.Max(x => x.Id) + 1;

            var cat = (Id: nextId, Name: contract.Name, Age: contract.AgeYears, WeightKg: contract.WeightKg);

            _cats.Add(cat);

            return Ok();
        }

        [HttpGet]
        [Route("all")]
        public ActionResult<CatResponseContract[]> GetAllCats()
        {
            var cats = _cats
                .Select(x => new CatResponseContract { Id = x.Id, AgeYears = x.Age, Name = x.Name, WeightKg = x.WeightKg })
                .ToArray();

            return Ok(cats);
        }


        private static List<(int Id, string Name, int Age, double WeightKg)> _cats = new();
    }
}
