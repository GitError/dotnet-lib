using AutoMapper;
using Holdings.Api.Resources;
using Holdings.Api.Resources.Validation;
using Holdings.Core.Models;
using Holdings.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _service;
        private readonly IMapper _mapper;

        public PortfolioController(IPortfolioService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UserRes>>> GetAll()
        {
            var data = await _service.GetAll();
            var resource = _mapper.Map<IEnumerable<Portfolio>, IEnumerable<PortfolioRes>>(data);

            return Ok(resource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PortfolioRes>> GetById(int id)
        {
            var data = await _service.GetById(id);
            var resource = _mapper.Map<Portfolio, PortfolioRes>(data);

            return Ok(resource);
        }

        [HttpPost("")]
        public async Task<ActionResult<PortfolioRes>> Create([FromBody] SavePortfolioRes savePortfolioRes)
        {
            var validator = new SavePortfolioValidator();
            var validationResult = await validator.ValidateAsync(savePortfolioRes);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
             
            var create = await _service.Create(_mapper.Map<SavePortfolioRes, Portfolio>(savePortfolioRes));

            var portfolio = await _service.GetById(create.UserId);

            var resource = _mapper.Map<Portfolio, PortfolioRes>(portfolio);

            return Ok(resource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var portfolio = await _service.GetById(id);

            if (portfolio == null)
                return NotFound();

            await _service.Delete(portfolio);

            return NoContent();
        }
    }
}