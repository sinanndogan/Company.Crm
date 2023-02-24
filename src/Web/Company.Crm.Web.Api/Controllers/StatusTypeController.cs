using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StatusTypeController : ControllerBase
{
    private readonly IStatusTypeService _statusTypeService;

    public StatusTypeController(IStatusTypeService statusTypeService)
    {
        _statusTypeService = statusTypeService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var types = _statusTypeService.GetAll();
        return Ok(types);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var type = _statusTypeService.GetById(id);
        return Ok(type);
    }

    [HttpPost]
    public IActionResult Post([FromBody] StatusType statusType)
    {
        var data = _statusTypeService.Insert(statusType);
        return Ok(data);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] StatusType statusType)
    {
        var data = _statusTypeService.Update(statusType);
        return Ok(data);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var data = _statusTypeService.DeleteById(id);
        return Ok(data);
    }

    [HttpPost("deleteByEntity")]
    public IActionResult Delete([FromBody] StatusType statusType)
    {
        var data = _statusTypeService.Delete(statusType);
        return Ok(data);
    }
}