using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Saas_Sexshop.Dto.TemplateDto;
using Saas_Sexshop.Models;
using Saas_Sexshop.Repository.TemplateRep;

namespace Saas_Sexshop.Controllers.TemplateController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly IMapper _mapper;

        public TemplateController(ITemplateRepository templateRepository, IMapper mapper)
        {
            _templateRepository = templateRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TemplateResponseDto>>> GetAllTemplates()
        {
            var templates = await _templateRepository.GetAllTemplatesAsync();
            var templateDtos = _mapper.Map<IEnumerable<TemplateResponseDto>>(templates);
            return Ok(templateDtos);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<TemplateResponseDto>>> GetActiveTemplates()
        {
            var templates = await _templateRepository.GetActiveTemplatesAsync();
            var templateDtos = _mapper.Map<IEnumerable<TemplateResponseDto>>(templates);
            return Ok(templateDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TemplateResponseDto>> GetTemplateById(int id)
        {
            var template = await _templateRepository.GetTemplateByIdAsync(id);
            if (template == null)
                return NotFound();

            var templateDto = _mapper.Map<TemplateResponseDto>(template);
            return Ok(templateDto);
        }

        [HttpPost]
        public async Task<ActionResult<TemplateResponseDto>> CreateTemplate([FromBody] TemplateCreateDto templateCreateDto)
        {
            var template = _mapper.Map<Template>(templateCreateDto);
            await _templateRepository.AddTemplateAsync(template);

            var templateResponseDto = _mapper.Map<TemplateResponseDto>(template);
            return CreatedAtAction(nameof(GetTemplateById), new { id = template.TemplateId }, templateResponseDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTemplate([FromRoute] int id, [FromBody] TemplateCreateDto templateUpdateDto)
        {
            var existingTemplate = await _templateRepository.GetTemplateByIdAsync(id);
            if (existingTemplate == null)
                return NotFound();

            _mapper.Map(templateUpdateDto, existingTemplate);
            await _templateRepository.UpdateTemplateAsync(existingTemplate);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplate(int id)
        {
            var deleted = await _templateRepository.DeleteTemplateByIdAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
