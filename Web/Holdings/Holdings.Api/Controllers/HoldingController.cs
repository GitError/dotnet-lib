﻿using AutoMapper;
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
    public class HoldingController : ControllerBase
    {
        private readonly IHoldingService _service;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public HoldingController(IHoldingService service, IMapper mapper, ILogger logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<HoldingRes>>> GetAll()
        {
            var data = await _service.GetAll();
            var resource = _mapper.Map<IEnumerable<Holding>, IEnumerable<HoldingRes>>(data);

            return Ok(resource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<HoldingRes>> GetById(int id)
        {
            var data = await _service.GetById(id);
            var resource = _mapper.Map<Holding, HoldingRes>(data);

            return Ok(resource);
        }

        [HttpPost("")]
        public async Task<ActionResult<HoldingRes>> Create([FromBody] SaveHoldingRes saveHoldingRes)
        {
            var validator = new SaveHoldingValidator();
            var validationResult = await validator.ValidateAsync(saveHoldingRes);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var create = await _service.Create(_mapper.Map<SaveHoldingRes, Holding>(saveHoldingRes));

            var holding = await _service.GetById(create.ModelId);

            var resource = _mapper.Map<Holding, HoldingRes>(holding);

            return Ok(resource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<HoldingRes>> Update(int id, [FromBody] SaveHoldingRes saveHoldingRes)
        {
            var validator = new SaveHoldingValidator();
            var validationResult = await validator.ValidateAsync(saveHoldingRes);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var holdingToBeUpdate = await _service.GetById(id);

            if (holdingToBeUpdate == null)
                return NotFound();

            var holding = _mapper.Map<SaveHoldingRes, Holding>(saveHoldingRes);

            await _service.Update(holdingToBeUpdate, holding);

            var updatedHolding = await _service.GetById(id);
            var updatedHoldingResource = _mapper.Map<Holding, HoldingRes>(updatedHolding);

            return Ok(updatedHoldingResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var holding = await _service.GetById(id);

            if (holding == null)
                return NotFound();

            await _service.Delete(holding);

            return NoContent();
        }
    }
}