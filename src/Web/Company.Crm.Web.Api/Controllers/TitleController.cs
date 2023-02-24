using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Company.Crm.Redis;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TitleController : ControllerBase
{
    private readonly ITitleService _service;
    private readonly RedisService _redisService;

    public TitleController(ITitleService service, RedisService redisService)
    {
        _service = service;
        _redisService = redisService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var titles = _redisService.GetOrSetCache("Titles", () => _service.GetAll());

        return Ok(titles);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var title = _service.GetById(id);
        return Ok(title);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Title titles)
    {
        var isAdded = _service.Insert(titles);

        _redisService.RemoveCache("Titles");

        return Ok(isAdded);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Title titles)
    {
        var isUpdated = _service.Update(titles);

        _redisService.RemoveCache("Titles");

        return Ok(isUpdated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _service.DeleteById(id);

        _redisService.RemoveCache("Titles");

        return Ok(isDeleted);
    }
}