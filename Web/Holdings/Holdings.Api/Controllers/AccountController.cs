using System.Threading.Tasks;
using AutoMapper;
using Holdings.Api.Resources;
using Holdings.Api.Resources.Validation;
using Holdings.Core.Models;
using Holdings.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Holdings.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public AccountController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
         
        [HttpPost("")]
        public async Task<ActionResult<UserRes>> Create([FromBody] SaveUserRes saveUserRes)
        {
            var validator = new SaveUserValidator();
            var validationResult = await validator.ValidateAsync(saveUserRes);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var create = await _service.Create(_mapper.Map<SaveUserRes, User>(saveUserRes));

            var portfolio = await _service.GetById(create.UserId);

            var resource = _mapper.Map<User, UserRes>(portfolio);

            return Ok(resource);
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