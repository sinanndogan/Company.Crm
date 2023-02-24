using Company.Crm.Application.Dtos;
using Company.Crm.Application.Dtos.Offer;
using Company.Crm.Application.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OfferController : ControllerBase
{
    private readonly IOfferService _offerService;

    public OfferController(IOfferService offerService)
    {
        _offerService = offerService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var data = _offerService.GetAll();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var offer = _offerService.GetById(id);
        return Ok(offer);
    }

    [HttpPost]
    public IActionResult Post([FromBody] OfferDto offers)
    {
        var isAdded = _offerService.Insert(offers);
        return Ok(isAdded);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] OfferDto offers)
    {
        var isUpdated = _offerService.Update(offers);
        return Ok(isUpdated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _offerService.DeleteById(id);
        return Ok(isDeleted);
    }

    [HttpPost("deleteByEntity")]
    public IActionResult Delete([FromBody] OfferDto entity)
    {
        var isDeleted = _offerService.Delete(entity);
        return Ok(isDeleted);
    }
}