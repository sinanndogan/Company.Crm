using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _service;

    public DepartmentController(IDepartmentService service)
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
    public IActionResult Post([FromBody] Department department)
    {
        var data = _service.Insert(department);
        return Ok(data);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Department department)
    {
        var data = _service.Update(department);
        return Ok(data);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var data = _service.DeleteById(id);
        return Ok(data);
    }

    [HttpPost("deleteByEntity")]
    public IActionResult Delete([FromBody] Department department)
    {
        var data = _service.Delete(department);
        return Ok(data);
    }
}