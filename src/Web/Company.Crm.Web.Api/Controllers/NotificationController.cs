using Company.Crm.Application.Dtos.Notification;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var data = _notificationService.GetAll();
        return Ok(data);
    }

    [HttpGet("GetPaged")]
    public IActionResult GetPaged([FromQuery] PaginationRequest req)
    {
        var data = _notificationService.GetPaged(req);

        return Ok(data);
    }
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var data = _notificationService.GetById(id);
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Post([FromBody] NotificationCreateOrUpdateDto entity)
    {
        var data = _notificationService.Insert(entity);
        return Ok(data);
    }
    [HttpPatch("{id}")]
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] NotificationCreateOrUpdateDto entity)
    {
        var data = _notificationService.Update(entity);
        return Ok(data);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var data = _notificationService.DeleteById(id);
        return Ok(data);
    }

    [HttpPost("deleteByEntity")]
    public IActionResult Delete([FromBody] Notification entity)
    {
        var data = _notificationService.Delete(entity);
        return Ok(data);
    }

    [HttpPatch("[action]/{id}")]
    [HttpPut("[action]/{id}")]

    public IActionResult ToggleReadSituation(int id)
    {
        return Ok(_notificationService.MarkAsReadOrUnread(id).IsSuccess);
    }
}
