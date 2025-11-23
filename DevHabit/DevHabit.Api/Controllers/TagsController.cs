using DevHabit.Api.Database;
using DevHabit.Api.DTOs.Habits;
using DevHabit.Api.DTOs.Tags;
using DevHabit.Api.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace DevHabit.Api.Controllers;

[ApiController]
[Route("tags")]
public class TagsController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    public TagsController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<ActionResult<TagCollectionDto>> GetTags()
    {
        List<TagDto> tags = await _dbContext.Tags.Select(TagQueries.ProjectToDto()).ToListAsync();
        var tagCollectionDto = new TagCollectionDto()
        {
            Data = tags
        };
        return Ok(tagCollectionDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TagDto>> GetTag(string id)
    {
        TagDto? tag = await _dbContext.Tags.Where(t => t.Id == id).Select(TagQueries.ProjectToDto()).FirstOrDefaultAsync();
        if (tag is null)
        {
            return NotFound();
        }
        return Ok(tag);
    }

    [HttpPost]
    public async Task<ActionResult<TagDto>> CreateTag(CreateTagDto createTagDto)
    {
        Tag tag = createTagDto.ToEntity();

        if(await _dbContext.Tags.AnyAsync(t => t.Name == tag.Name))
        {
            return Conflict($"This tag '{tag.Name}' already exists");
        }

        _dbContext.Tags.Add(tag);
        await _dbContext.SaveChangesAsync();

        TagDto tagDto = tag.ToDto();

        return CreatedAtAction(nameof(GetTag), new { id = tagDto.Id }, tagDto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTag(string id, UpdateTagDto updateTagDto)
    {
        Tag? tag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        if (tag is null)
        {
            return NotFound();
        }

        tag.UpdateFromDto(updateTagDto);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTag(string id)
    {
        Tag? tag = await _dbContext.Tags.FirstOrDefaultAsync(t => t.Id == id);
        if (tag is null)
        {
            return NotFound();
        }
        _dbContext.Tags.Remove(tag);
        await _dbContext.SaveChangesAsync();

        return NoContent();
    }
}
