using Company.Crm.Application.Dtos.Task;
using Company.Crm.Application.Services;
using Company.Crm.Application.Services.Abstracts;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var task = _taskService.GetAll();
        return Ok(task);
    }

    [HttpGet("GetPaged")]
    public IActionResult GetPaged([FromQuery] PaginationRequest req)
    {
        var tasks = _taskService.GetPaged(req);

        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var task = _taskService.GetById(id);
        return Ok(task);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateOrUpdateTaskDto task)
    {
        var isAdded = _taskService.Insert(task);
        return Ok(isAdded);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] CreateOrUpdateTaskDto task)
    {
        var isUpdated = _taskService.Update(task);
        return Ok(isUpdated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _taskService.DeleteById(id);
        return Ok(isDeleted);
    }

    [HttpPost("deleteByEntity")]
    public IActionResult Delete([FromBody] TaskDto task)
    {
        var isDeleted = _taskService.Delete(task);
        return Ok(isDeleted);
    }
}