using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Entities.Usr;
using Company.Framework.Authentication;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Company.Crm.Web.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var data = _userService.GetAll();
        return Ok(data);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var data = _userService.GetById(id);
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Insert([FromBody] User user)
    {
        var isAdded = _userService.Insert(user);
        return Ok(isAdded);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] User userStatus)
    {
        var isUpdated = _userService.Update(userStatus);
        return Ok(isUpdated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _userService.DeleteById(id);
        return Ok(isDeleted);
    }

    [HttpPost("deleteByEntity")]
    public IActionResult DeleteById([FromBody] User user)
    {
        var isDeleted = _userService.Delete(user);
        return Ok(isDeleted);
    }

    [HttpPost("import-excel")]
    public async Task<IActionResult> ImportExcel(IFormFile excelFile)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        var stream = excelFile.OpenReadStream();
        using var package = new ExcelPackage(stream);

        var sheet = package.Workbook.Worksheets["Sheet1"];

        var users = new List<User>();

        int startRow = 2;
        int endRow = sheet.Dimension.Rows;
        for (int row = startRow; row <= endRow; row++)
        {
            users.Add(new()
            {
                Username = sheet.Cells[row, 2].Value.ToString(),
                Email = sheet.Cells[row, 3].Value.ToString(),
                Password = SecurityHelper.HashCreate("123"),
                Name = sheet.Cells[row, 5].Value.ToString(),
                Surname = sheet.Cells[row, 5].Value.ToString(),
                UserStatusId = 1
            });
        }

        var isInserted = await _userService.InsertAll(users);

        return Ok(isInserted);
    }
}