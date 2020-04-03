using AutoMapper;
using Holdings.Api.Resources;
using Holdings.Api.Resources.Validation;
using Holdings.Core.Models;
using Holdings.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;

        public PortfolioController(IPortfolioService service, IMapper mapper, ILogger logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
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

        [HttpPut("{id}")]
        public async Task<ActionResult<ModelRes>> Update(int id, [FromBody] SavePortfolioRes savePortfolioRes)
        {
            var validator = new SavePortfolioValidator();
            var validationResult = await validator.ValidateAsync(savePortfolioRes);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var portfolioToBeUpdate = await _service.GetById(id);

            if (portfolioToBeUpdate == null)
                return NotFound();

            var portfolio = _mapper.Map<SavePortfolioRes, Portfolio>(savePortfolioRes);

            await _service.Update(portfolioToBeUpdate, portfolio);

            var updatedPortfolio = await _service.GetById(id);
            var updatedPortfolioResource = _mapper.Map<Portfolio, PortfolioRes>(updatedPortfolio);

            return Ok(updatedPortfolioResource);
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