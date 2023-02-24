using Company.Crm.Application.Services.Abstracts;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Mvc;
using TaskStatus = Company.Crm.Domain.Entities.Lst.TaskStatus;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskStatusController : ControllerBase
{
	private readonly ITaskStatusService _taskStatusService;

	public TaskStatusController(ITaskStatusService taskStatusService)
	{
		_taskStatusService = taskStatusService;
	}

	[HttpGet]
	public IActionResult Get()
	{
		var taskStatus = _taskStatusService.GetAll();
		return Ok(taskStatus);
	}
	[HttpGet("GetPaged")]
	public IActionResult GetPaged([FromQuery] PaginationRequest req)
	{
		var taskStatus = _taskStatusService.GetPaged(req);
		return Ok(taskStatus);
	}

	[HttpGet("{id}")]
	public IActionResult Get(int id)
	{
		var taskStatus = _taskStatusService.GetById(id);
		return Ok(taskStatus);
	}

	[HttpPost]
	public IActionResult Post([FromBody] TaskStatus taskStatus)
	{
		var isAdded = _taskStatusService.Insert(taskStatus);
		return Ok(isAdded);
	}

	[HttpPut("{id}")]
	[HttpPatch("{id}")]
	public IActionResult Put(int id, [FromBody] TaskStatus taskStatus)
	{
		var isUpdated = _taskStatusService.Update(taskStatus);
		return Ok(isUpdated);
	}

	[HttpDelete("{id}")]
	public IActionResult Delete(int id)
	{
		var isDeleted = _taskStatusService.DeleteById(id);
		return Ok(isDeleted);
	}

	[HttpPost("deleteByEntity")]
	public IActionResult Delete([FromBody] TaskStatus entity)
	{
		var isDeleted = _taskStatusService.Delete(entity);
		return Ok(isDeleted);
	}
}
