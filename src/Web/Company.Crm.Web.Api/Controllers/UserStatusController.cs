using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserStatusController : Controller
{
    private readonly IUserStatusService _userStatusService;

    public UserStatusController(IUserStatusService userStatusService)
    {
        _userStatusService = userStatusService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var data = _userStatusService.GetAll();
        return Ok(data);
    }

    [HttpGet("GetPaged")]
    public IActionResult GetPaged([FromQuery] PaginationRequest request)
    {
        var data = _userStatusService.GetPaged(request);
        return Ok(data);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var data = _userStatusService.GetById(id);
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Insert([FromBody] UserStatus userStatus)
    {
        var isAdded = _userStatusService.Insert(userStatus);
        return Ok(isAdded);
    }
    [HttpPatch("{id}")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UserStatus userStatus)
    {
        var isUpdated = _userStatusService.Update(userStatus);
        return Ok(isUpdated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _userStatusService.DeleteById(id);
        return Ok(isDeleted);
    }

    [HttpPost("deleteByEntity")]
    public IActionResult DeleteById([FromBody] UserStatus userStatus)
    {
        var isDeleted = _userStatusService.Delete(userStatus);
        return Ok(isDeleted);
    }
}