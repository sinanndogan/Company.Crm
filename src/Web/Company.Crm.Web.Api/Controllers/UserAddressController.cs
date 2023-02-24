using Company.Crm.Application.Dtos.Address;
using Company.Crm.Application.Dtos.UserAddress;
using Company.Crm.Application.Dtos.UserAddress.AddressType;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Enums;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Api.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserAddressController : ControllerBase
{
    private readonly IUserAddressService _userAddressService;

    public UserAddressController(IUserAddressService addressService)
    {
        _userAddressService = addressService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        var address = _userAddressService.GetAll();
        return Ok(address);
    }
    [HttpGet("GetPaged")]
    public IActionResult GetPaged([FromQuery] PaginationRequest req)
    {
        var data = _userAddressService.GetPaged(req);

        return Ok(data);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var address = _userAddressService.GetById(id);
        return Ok(address);
    }

    [HttpPost]
    public IActionResult Post([FromBody] AddressCreateOrUpdateDto address)
    {
        var isAdded = _userAddressService.Insert(address);
        return Ok(isAdded);
    }

    [HttpPut("{id}")]
    [HttpPatch("{id}")]
    public IActionResult Put(int id, [FromBody] AddressCreateOrUpdateDto address)
    {
        var isUpdated = _userAddressService.Update(address);
        return Ok(isUpdated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var isDeleted = _userAddressService.DeleteById(id);
        return Ok(isDeleted);
    }

    [HttpPost("deleteByEntity")]
    public IActionResult Delete([FromBody] AddressDetailDto address)
    {
        var isDeleted = _userAddressService.Delete(address);
        return Ok(isDeleted);
    }
    [HttpGet("[action]")]
    public IActionResult GetAddressTypes()
    {
        var addressTypes = new List<AddressTypeDto>()
        {
            new()
            {
                AddressType = AddressTypeEnum.Home.ToString(),
                Value = (int)AddressTypeEnum.Home    //1
            },
            new()
            {
                AddressType = AddressTypeEnum.Job.ToString(),
                Value = (int)AddressTypeEnum.Job    //2
            }
        };
        return Ok(addressTypes);
    }
}