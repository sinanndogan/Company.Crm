using Company.Crm.Application.Services;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class RegionController : ControllerBase
{
    private readonly IRegionService _regionservice;
  

    public RegionController(IRegionService regionservice)
    {
        _regionservice = regionservice;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var region = _regionservice.GetAll();
        return Ok(region);
    }


    [HttpGet("GetPaged")]
    public IActionResult GetPaged([FromQuery] PaginationRequest req)
    {
        var regions = _regionservice.GetPaged(req);

        return Ok(regions);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var region = _regionservice.GetById(id);
        return Ok(region);
    }
   

    [HttpPost]
    public IActionResult Post([FromBody] Region region)
    {
        var isAdded = _regionservice.Insert(region);
        return Ok(isAdded);
    }


    [HttpPatch("{id}")]
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Region region)
    {
        var isUpdated = _regionservice.Update(region);
        return Ok(isUpdated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _regionservice.DeleteById(id);
        return Ok(isDeleted);
    }

    [HttpPost("deleteByEntity")]
    public IActionResult Delete([FromBody] Region entity)
    {
        var isDeleted = _regionservice.Delete(entity);
        return Ok(isDeleted);
    }
}