using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saas.Services.DTOs.UserDto;
using Saas.Services.UserServices;

namespace Saas.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase{

        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Post([FromBody] UserCreateDto userCreateDto){
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try{
                var sucess = await _userService.AddUserAsync(userCreateDto);
                if (!sucess) { return BadRequest(new { success = false, message = "Não foi possível adicionar o usuario" });}

                _logger.LogInformation("usuário criado com sucesso.");
                return Ok(new { success = true, message = "usuário criado com sucesso" });
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Erro ao criar usuário");
                return StatusCode(500, new { success = false, error = "Erro interno ao criar o usuário." });
            }
        } 
    }
}
