using Microsoft.AspNetCore.Mvc;
using Wedding.DAL.Data.Entities;
using Wedding.DAL.Repository.Abstractions;
using Wedding.Web.Models.City;
using Wedding.Web.Models.Salon;

namespace Wedding.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class SalonController : ControllerBase
{
    private readonly ISalonRepository _salonRepository;

    public SalonController(ISalonRepository salonRepository)
    {
        _salonRepository = salonRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Add(SalonDto body)
    {
        var item = new Salon
        {
            Address = body.Address,
            Latitude = body.Latitude,
            Longitude = body.Longitude
        };

        await _salonRepository.Create(item);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteById(int id)
    {
        var entity = await _salonRepository.GetByIdAsync(id);

        await _salonRepository.Delete(entity);

        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetSalons()
    {
        var items = await _salonRepository.GetAllAsync();

        var mappedItems = items
            .Select(s => new SalonInfoDto
            {
                Id = s.Id,
                Address = s.Address,
                City = new CityDto
                {
                    Name = s.City.Name
                },
                Latitude = s.Latitude,
                Longitude = s.Longitude
            })
            .ToList();

        return Ok(mappedItems);
    }

    [HttpGet("{salonId:int}/Image")]
    public async Task<IActionResult> GetSalonImage(int salonId)
    {
        var item = await _salonRepository.GetByIdAsync(salonId);

        return File(item.FileBytes, "image/jpg");
    }
}