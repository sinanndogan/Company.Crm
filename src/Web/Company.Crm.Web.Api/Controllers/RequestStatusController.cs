using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequestStatusController : Controller
{
    private readonly IRequestStatusService _requestStatusService;

    public RequestStatusController(IRequestStatusService requestStatusService)
    {
        _requestStatusService = requestStatusService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var requestStatus = _requestStatusService.GetAll();
        return Ok(requestStatus);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var requestStatus = _requestStatusService.GetById(id);
        return Ok(requestStatus);
    }

    [HttpPost]
    public IActionResult Insert([FromBody] RequestStatus requestStatus)
    {
        var isAdded = _requestStatusService.Insert(requestStatus);
        return Ok(isAdded);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] RequestStatus requestStatus)
    {
        var isUpdated = _requestStatusService.Update(requestStatus);
        return Ok(isUpdated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _requestStatusService.DeleteById(id);

        return Ok(isDeleted);
    }

    [HttpPost("deleteByEntity")]
    public IActionResult DeleteById([FromBody] RequestStatus requestStatus)
    {
        var isDeleted = _requestStatusService.Delete(requestStatus);
        return Ok(isDeleted);
    }
}
