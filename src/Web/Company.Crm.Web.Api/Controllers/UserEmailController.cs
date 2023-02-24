using Company.Crm.Application.Dtos.UserEmail;
using Company.Crm.Application.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserEmailController : ControllerBase
{
    private readonly IUserEmailService _service;

    public UserEmailController(IUserEmailService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var email = _service.GetAll();
        return Ok(email);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var email = _service.GetById(id);
        return Ok(email);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateOrUpdateUserEmailDto userEmail)
    {
        var isAdded = _service.Insert(userEmail);
        return Ok(isAdded);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] CreateOrUpdateUserEmailDto userEmail)
    {
        var isUpdated = _service.Update(userEmail);
        return Ok(isUpdated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _service.DeleteById(id);
        return Ok(isDeleted);
    }

    [HttpPost("deleteByEntity")]
    public IActionResult Delete([FromBody] CreateOrUpdateUserEmailDto entity)
    {
        var isDeleted = _service.Delete(entity);
        return Ok(isDeleted);
    }
}