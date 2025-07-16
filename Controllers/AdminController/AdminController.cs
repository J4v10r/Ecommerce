using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Saas.Services.AdminService;
using Saas.Services.DTOs.AdminDto;

namespace Saas.Controllers.AdminController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService adminService, IMapper mapper, ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Post([FromBody]AdminCreatDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var admin = await _adminService.CreateAdminAsync(dto);
            return StatusCode(StatusCodes.Status201Created, admin);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest("O id deve ser um valor maior que zero.");
            }
            await _adminService.DeleteAdminAsync(id);
            return NoContent();
        }
    }
}
