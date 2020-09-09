using Microsoft.AspNetCore.Mvc;
using Stock.Domain;
using Stock.Services.Repositories.Abstract;

namespace Stock.Api.Controllers
{
    public class RegulationsController : BaseController
    {
        private readonly IRegulationRepository _repo;

        public RegulationsController(IRegulationRepository repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IActionResult Regulations()
        {

            var regulation = _repo.Regulations();
            
            return Ok(regulation);
        }
        
        [HttpPost]
        public IActionResult Regulations([FromForm]Regulation regulation)
        {
            var result = _repo.Create(regulation);
            return Ok(result);
        }
    }
}