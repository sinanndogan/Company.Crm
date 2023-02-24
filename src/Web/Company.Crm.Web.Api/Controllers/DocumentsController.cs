using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentsController : ControllerBase
{
    private readonly IDocumentService _documentService;

    public DocumentsController(IDocumentService documentService)
    {
        _documentService = documentService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var document = _documentService.GetAll();

        return Ok(document);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var document = _documentService.GetById(id);
        return Ok(document);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Document document)
    {
        var isAdded = _documentService.Insert(document);
        return Ok(isAdded);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Document document)
    {
        var isUpdated = _documentService.Update(document);
        return Ok(isUpdated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _documentService.DeleteById(id);
        return Ok(isDeleted);
    }

    [HttpPost("deleteByEntity")]
    public IActionResult Delete([FromBody] Document entity)
    {
        var isDeleted = _documentService.Delete(entity);
        return Ok(isDeleted);
    }
}