using Company.Crm.Application.Dtos;
using Company.Crm.Application.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeeController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var data = _service.GetAll();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var data = _service.GetById(id);
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateOrUpdateEmployeeDto dto)
    {
        var data = _service.Insert(dto);
        return Ok(data);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] CreateOrUpdateEmployeeDto dto)
    {
        var data = _service.Update(dto);
        return Ok(data);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var data = _service.DeleteById(id);
        return Ok(data);
    }
}