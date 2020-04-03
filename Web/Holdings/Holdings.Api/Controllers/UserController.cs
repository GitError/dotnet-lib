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
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UserController(IUserService userService, IMapper mapper, ILogger logger)
        {
            _service = userService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UserRes>>> GetAll()
        {
            var data = await _service.GetAll();
            var resource = _mapper.Map<IEnumerable<User>, IEnumerable<UserRes>>(data);
            return Ok(resource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRes>> GetById(int id)
        {
            var data = await _service.GetById(id);
            var resource = _mapper.Map<User, UserRes>(data);

            return Ok(resource);
        }

        [HttpPost("")]
        public async Task<ActionResult<UserRes>> Create([FromBody] SaveUserRes saveUserRes)
        {
            var validator = new SaveUserValidator();
            var validationResult = await validator.ValidateAsync(saveUserRes);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var create = await _service.Create(_mapper.Map<SaveUserRes, User>(saveUserRes));

            var user = await _service.GetById(create.UserId);

            var resource = _mapper.Map<User, UserRes>(user);

            return Ok(resource);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ModelRes>> Update(int id, [FromBody] SaveUserRes saveUserRes)
        {
            var validator = new SaveUserValidator();
            var validationResult = await validator.ValidateAsync(saveUserRes);

            var requestIsInvalid = id == 0 || !validationResult.IsValid;

            if (requestIsInvalid)
                return BadRequest(validationResult.Errors);

            var userToBeUpdate = await _service.GetById(id);

            if (userToBeUpdate == null)
                return NotFound();

            var user = _mapper.Map<SaveUserRes, User>(saveUserRes);

            await _service.Update(userToBeUpdate, user);

            var updatedUser = await _service.GetById(id);
            var updatedUserResource = _mapper.Map<User, UserRes>(updatedUser);

            return Ok(updatedUserResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id == 0)
                return BadRequest();

            var user = await _service.GetById(id);

            if (user == null)
                return NotFound();

            await _service.Delete(user);

            return NoContent();
        }
    }
}