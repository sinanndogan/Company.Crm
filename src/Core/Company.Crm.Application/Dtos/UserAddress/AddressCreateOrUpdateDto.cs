using System.ComponentModel.DataAnnotations.Schema;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.Crm.Application.Dtos.UserAddress;

public class AddressCreateOrUpdateDto : BaseDto
{
    public AddressCreateOrUpdateDto()
    {
        AddressTypes = new List<SelectListItem>();
    }

    public int UserId { get; set; }
    public string Description { get; set; }
    public short AddressType { get; set; } // addressTypeEnumNumber

    [NotMapped]
    [ValidateNever]
    public List<SelectListItem>? AddressTypes { get; set; }
}