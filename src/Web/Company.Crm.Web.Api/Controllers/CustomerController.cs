using Company.Crm.Application.Dtos;
using Company.Crm.Application.Services.Abstracts;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;

namespace Company.Crm.Web.Api.Controllers;

[Authorize]
//[PermissionAuthorize(PermissionEnum.P1)]
[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    // api/customer
    [HttpGet]
    public IActionResult Get()
    {
        var data = _customerService.GetAll();
        return Ok(data);
    }

    // api/customer/getpaged
    [HttpGet("GetPaged")]
    public IActionResult GetPaged([FromQuery] PaginationRequest req)
    {
#if DEBUG
        Thread.Sleep(TimeSpan.FromSeconds(2));
#endif
        var customers = _customerService.GetPaged(req);
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var data = _customerService.GetById(id);
        return Ok(data);
    }

    [HttpPost]
    public IActionResult Post([FromBody] CreateOrUpdateCustomerDto dto)
    {
        var data = _customerService.Insert(dto);
        return Ok(data);
    }

    [HttpPatch("{id}")]
    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] CreateOrUpdateCustomerDto dto)
    {
        var data = _customerService.Update(dto);
        return Ok(data);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var data = _customerService.DeleteById(id);
        return Ok(data);
    }

    [HttpPost("export-excel")]
    public IActionResult ExportExcel()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var stream = new MemoryStream();
        using var package = new ExcelPackage(stream);

        var sheet = package.Workbook.Worksheets.Add("Sheet1");

        var titleCell = sheet.Cells["A1:E1"];
        titleCell.Value = "Customers";
        titleCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        titleCell.Style.Font.Bold = true;
        titleCell.Merge = true;
        titleCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
        titleCell.Style.Fill.BackgroundColor.SetColor(Color.Red);
        titleCell.Style.Font.Color.SetColor(Color.White);

        sheet.Cells["A2"].Value = "Id";
        sheet.Cells["B2"].Value = "User";
        sheet.Cells["C2"].Value = "Company";
        sheet.Cells["D2"].Value = "Title";
        sheet.Cells["E2"].Value = "BirthDate";

        var customerResponse = _customerService.GetAll();
        if (customerResponse.IsSuccess && customerResponse.Data?.Count > 0)
        {
            int row = 3;
            foreach (var customer in customerResponse.Data)
            {
                sheet.Cells[row, 1].Value = customer.Id;
                sheet.Cells[row, 2].Value = customer.UserFullName;
                sheet.Cells[row, 3].Value = customer.CompanyName;
                sheet.Cells[row, 4].Value = customer.TitleName;
                sheet.Cells[row, 5].Value = customer.BirthDate.ToString("d");
                row++;
            }

            package.Save();

            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            var fileName = "CustomerReport.xlsx";

            stream.Position = 0;
            return File(stream, contentType, fileName);
        }

        return BadRequest();
    }
}