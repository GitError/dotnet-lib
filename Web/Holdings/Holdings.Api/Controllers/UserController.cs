using AutoMapper;
using Holdings.Api.Resources;
using Holdings.Core.Models;
using Holdings.Core.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holdings.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<UserRes>>> GetAll()
        {
            var data = await _userService.GetAll();
            var resource = _mapper.Map<IEnumerable<User>, IEnumerable<UserRes>>(data);
            return Ok(resource);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRes>> GetById(int id)
        {
            var data = await _userService.GetById(id);
            var resource = _mapper.Map<User, UserRes>(data);

            return Ok(resource);
        }
    }
}