using Microsoft.AspNetCore.Mvc;
using PICA.UserMicroservice.WebAPI.Interfaces;
using PICA.UserMicroservice.WebAPI.Models;
using System.Text.Json;

namespace PICA.UserMicroservice.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;

        public UserController(
            IUserRepository userRepository,
            ILogger<UserController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        // GET: api/<UserController>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            _logger.LogInformation("UserController - GET");

            return await _userRepository.GetAllAsync();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("UserController - GET with param {0}", id);

            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task Post([FromBody] User user)
        {
            _logger.LogInformation("UserController - POST {0}", JsonSerializer.Serialize(user));

            await _userRepository.CreateAsync(user);
        }
    }
}
