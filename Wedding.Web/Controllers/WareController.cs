using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;
using Wedding.Web.Models.Ware;

namespace Wedding.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class WareController : ControllerBase
{
    private readonly IWareRepository _wareRepository;

    public WareController(IWareRepository wareRepository)
    {
        _wareRepository = wareRepository;
    }

    [HttpGet("{wareId:guid}/Image")]
    public async Task<IActionResult> GetCategoryImage(Guid wareId)
    {
        var items = await _wareRepository.GetByIdAsync(wareId);

        return File(items.FileBytes, "image/jpg");
    }

    [HttpPost("Items")]
    public async Task<IActionResult> GetItems([FromBody] GetWaresBody body)
    {
        var items = await _wareRepository.GetQuery()
            .Include(w => w.Category)
            .Where(w => body.WareIds.Contains(w.Id))
            .ToListAsync();

        var mappedItems = items
            .Select(w => new
            {
                w.Id,
                w.Name,
                w.Description,
                w.Discounted,
                Category = w.Category.Name,
                w.RetailPrice
            })
            .ToList();

        return Ok(mappedItems);
    }

    [HttpGet]
    public async Task<IActionResult> GetWares()
    {
        var items = await _wareRepository.GetAllAsync();

        return Ok(items);
    }

    [HttpGet]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _wareRepository.GetByIdAsync(id);

        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Add(WareDto body)
    {
        var item = new Ware
        {
            Name = body.Name,
            Price = body.Price,
            Description = body.Description
        };

        await _wareRepository.Create(item);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var entity = await _wareRepository.GetByIdAsync(id);

        await _wareRepository.Delete(entity);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Update(Ware item)
    {
        await _wareRepository.Update(item);

        return Ok();
    }
}