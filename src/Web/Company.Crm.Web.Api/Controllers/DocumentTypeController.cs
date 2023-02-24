using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Lst;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DocumentTypeController : ControllerBase
{
    private readonly IDocumentTypeService _documentTypeService;

    public DocumentTypeController(IDocumentTypeService documentTypeService)
    {
        _documentTypeService = documentTypeService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var documentType = _documentTypeService.GetAll();

        return Ok(documentType);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var documentType = _documentTypeService.GetById(id);
        return Ok(documentType);
    }

    [HttpPost]
    public IActionResult Post([FromBody] DocumentType documentType)
    {
        var isAdded = _documentTypeService.Insert(documentType);
        return Ok(isAdded);
    }
    
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] DocumentType documentType)
    {
        var isUpdated = _documentTypeService.Update(documentType);
        return Ok(isUpdated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _documentTypeService.DeleteById(id);
        return Ok(isDeleted);
    }
    
    [HttpPost("deleteByEntity")]
    public IActionResult Delete([FromBody] DocumentType entity)
    {
        var isDeleted = _documentTypeService.Delete(entity);
        return Ok(isDeleted);
    }
}
