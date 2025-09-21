using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebKut.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TypeContentController : ControllerBase
    {
        private readonly Services.Interfaces.ITypeContentService _typeContentService;
        public TypeContentController(Services.Interfaces.ITypeContentService typeContentService)
        {
            _typeContentService = typeContentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTypeContents()
        {
            var typeContents = await _typeContentService.GetAllTypeContentsAsync();
            return Ok(typeContents);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTypeContentById(int id)
        {
            var typeContent = await _typeContentService.GetTypeContentByIdAsync(id);
            if (typeContent == null)
            {
                return NotFound(new { message = "TypeContent não encontrado" });
            }
            return Ok(typeContent);
        }
        [HttpPost]
        public async Task<IActionResult> AddTypeContent([FromBody] DTOs.TypeContent.CreateTypeContentDto createTypeContentDto)
        {
            var newTypeContent = await _typeContentService.AddTypeContentAsync(createTypeContentDto);
            return CreatedAtAction(nameof(GetTypeContentById), new { id = newTypeContent.Id }, newTypeContent);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTypeContent(int id, [FromBody] DTOs.TypeContent.UpdateTypeContentDto updateTypeContentDto)
        {
            var updatedTypeContent = await _typeContentService.UpdateTypeContentAsync(id, updateTypeContentDto);
            if (updatedTypeContent == null)
            {
                return NotFound(new { message = "TypeContent não encontrado para atualização" });
            }
            return Ok(updatedTypeContent);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTypeContent(int id)
        {
            var isDeleted = await _typeContentService.DeleteTypeContentAsync(id);
            if (!isDeleted)
            {
                return NotFound(new { message = "TypeContent não encontrado para exclusão" });
            }
            return NoContent();
        }
    }
}
