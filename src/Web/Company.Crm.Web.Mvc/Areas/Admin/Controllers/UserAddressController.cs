using Company.Crm.Application.Constants;
using Company.Crm.Application.Dtos.UserAddress;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Enums;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class UserAddressController : Controller
{
    private readonly IUserAddressService _addressService;

    public UserAddressController(IUserAddressService addressService, IStatusTypeService statusTypeService)
    {
        _addressService = addressService;
    }

    public IActionResult Index(int page = 1)
    {
        var addresses = _addressService.GetPaged(new PaginationRequest { Page = page });
        return View(addresses.Data);
    }

    public PartialViewResult Detail(int id)
    {
        var address = _addressService.GetById(id);
        return PartialView("_Detail", address.Data);
    }

    [HttpGet]
    public PartialViewResult Create()
    {
        AddressCreateOrUpdateDto dto = new();
        FillDropdownItems(dto);
        return PartialView("_Create", dto);
    }

    private void FillDropdownItems(AddressCreateOrUpdateDto dto)
    {
        dto.AddressTypes?.AddRange(new[]
        {
            new SelectListItem { Text = AddressTypeEnum.Home.ToString(), Value = ((int)AddressTypeEnum.Home).ToString() },
            new SelectListItem { Text = AddressTypeEnum.Job.ToString(), Value = ((int)AddressTypeEnum.Job).ToString() }
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(AddressCreateOrUpdateDto item)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isInserted = _addressService.Insert(item);
                if (isInserted.IsSuccess)
                    return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        FillDropdownItems(item);

        return PartialView("_Create", item);
    }

    public PartialViewResult Edit(int? id)
    {
        var dto = new AddressCreateOrUpdateDto();
        if (id.HasValue) dto = _addressService.GetForEditById(id.Value).Data;

        FillDropdownItems(dto);

        return PartialView("_Edit", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(AddressCreateOrUpdateDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _addressService.Update(dto);
                if (isUpdated.IsSuccess)
                    return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        FillDropdownItems(dto);

        return PartialView("_Edit", dto);
    }

    [HttpGet]
    public PartialViewResult Delete(int id)
    {
        var serviceItem = _addressService.GetById(id);
        return PartialView("_Delete", serviceItem.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        return Json(new { IsSuccess = _addressService.DeleteById(id), Redirect = Url.Action("Index") });
    }
}