using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wedding.AdminPanel.Constants;
using Wedding.AdminPanel.Controllers.Abstractions;
using Wedding.AdminPanel.Models.Pagination;
using Wedding.AdminPanel.Models.WareCategory;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;

namespace Wedding.AdminPanel.Controllers;

public class WareCategoryController : ReadWriteControllerBase<WareCategory, Guid, IWareCategoryRepository>
{
    public WareCategoryController(IWareCategoryRepository repository) : base(repository)
    {
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("[controller]/Table")]
    public async Task<IActionResult> Table(int? page)
    {
        page ??= 1;
        var query = Repository.GetQuery();

        var total = await query
            .CountAsync();

        var items = await query
            .Skip((page.Value - 1) * PaginationConstants.ElementsOnPage)
            .Take(PaginationConstants.ElementsOnPage)
            .ToListAsync();

        var result = new BasePagedModel<WareCategory>
        {
            Total = total,
            Page = page.Value,
            PageItems = items
        };

        return PartialView(result);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return PartialView();
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateWareCategoryDto body)
    {
        var wareCategory = new WareCategory
        {
            Name = body.Name
        };

        if (body.File != null)
        {
            using var dataStream = new MemoryStream();
            await body.File.CopyToAsync(dataStream);
            var imageBytes = dataStream.ToArray();

            wareCategory.FileBytes = imageBytes;
        }

        await Repository.Create(wareCategory);

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        var wareCategory = await Repository.GetByIdAsync(id);
        await Repository.Delete(wareCategory);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        var item = await Repository.GetByIdAsync(id);

        return PartialView(item);
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Update([FromForm] UpdateWareCategoryDto body)
    {
        var item = await Repository.GetByIdAsync(body.Id);

        item.Name = body.Name;
            

        if (body.File != null)
        {
            using var dataStream = new MemoryStream();
            await body.File.CopyToAsync(dataStream);
            var imageBytes = dataStream.ToArray();

            item.FileBytes = imageBytes;
        }

        await Repository.Update(item);

        return NoContent();
    }
}