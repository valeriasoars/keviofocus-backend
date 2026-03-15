using keviofocus_backend.Dto.StudyMaterial;
using keviofocus_backend.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace keviofocus_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyMaterialController : ControllerBase
    {
        private readonly IStudyMaterialService _service;

        public StudyMaterialController(IStudyMaterialService service)
        {
            _service = service;
        }

        [HttpGet("session/{sessionId}")]
        public async Task<IActionResult> GetAllBySession(Guid sessionId)
        {
            var materials = await _service.GetAllBySession(sessionId);
            return Ok(materials);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var material = await _service.GetById(id);
            if (material is null) return NotFound();
            return Ok(material);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudyMaterialCreateDto dto)
        {
            var material = await _service.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = material.Id }, material);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] StudyMaterialUpdateDto dto)
        {
            var material = await _service.Update(id, dto);
            if (material is null) return NotFound();
            return Ok(material);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _service.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }


    }
}
