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
    public class ModelController : ControllerBase
    {
        private readonly IModelService _service;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ModelController(IModelService service, IMapper mapper, ILogger logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<ModelRes>>> GetAll()
        {
            var data = await _service.GetAll();
            var resource = _mapper.Map<IEnumerable<Model>, IEnumerable<ModelRes>>(data);

            return Ok(resource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ModelRes>> GetById(int id)
        {
            var data = await _service.GetById(id);
            var resource = _mapper.Map<Model, ModelRes>(data);

            return Ok(resource);
        }

        [HttpPost("")]
        public async Task<ActionResult<ModelRes>> Create([FromBody] SaveModelRes saveModelRes)
        {
            var validator = new SaveModelValidator();
            var validationResult = await validator.ValidateAsync(saveModelRes);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var create = await _service.Create(_mapper.Map<SaveModelRes, Model>(saveModelRes));

            var model = await _service.GetById(create.ModelId);

            var resource = _mapper.Map<Model, ModelRes>(model);

            return Ok(resource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ModelRes>> Update(int id, [FromBody] SaveModelRes saveModelRes)
        {
            var validator = new SaveModelValidator();
            var validationResult = await validator.ValidateAsync(saveModelRes);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var modelToBeUpdate = await _service.GetById(id);

            if (modelToBeUpdate == null)
                return NotFound();

            var model = _mapper.Map<SaveModelRes, Model>(saveModelRes);

            await _service.Update(modelToBeUpdate, model);

            var updatedModel = await _service.GetById(id);
            var updatedModelResource = _mapper.Map<Model, ModelRes>(updatedModel);

            return Ok(updatedModelResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest();

            var model = await _service.GetById(id);

            if (model == null)
                return NotFound();

            await _service.Delete(model);

            return NoContent();
        }
    }
}