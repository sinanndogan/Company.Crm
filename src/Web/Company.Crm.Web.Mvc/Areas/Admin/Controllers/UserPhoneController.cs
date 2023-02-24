using Company.Crm.Application.Constants;
using Company.Crm.Application.Dtos.UserPhone;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class UserPhoneController : Controller
{
    private readonly IUserPhoneService _userPhoneService;
    private readonly IValidator<CreateOrUpdateUserPhoneDto> _userPhoneValidator;

    public UserPhoneController(IUserPhoneService userPhoneService, IValidator<CreateOrUpdateUserPhoneDto> userPhoneValidator)
    {
        _userPhoneService = userPhoneService;
        _userPhoneValidator = userPhoneValidator;
    }

    public IActionResult Index(int page = 1)
    {
        var userPhones = _userPhoneService.GetPaged(page);
        return View(userPhones);
    }

    public PartialViewResult Detail(int id)
    {
        var userPhone = _userPhoneService.GetById(id);
        return PartialView("_Detail", userPhone);
    }

    [HttpGet]
    public PartialViewResult Create()
    {
        var dto = new CreateOrUpdateUserPhoneDto();
        return PartialView("_Create", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateOrUpdateUserPhoneDto item)
    {
        try
        {
            var validationResult = _userPhoneValidator.Validate(item);
            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);

                return PartialView("_Create", item);
            }


            if (validationResult.IsValid)
            {
                var isInserted = _userPhoneService.Insert(item);
                if (isInserted)
                    return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Create", item);
    }

    public PartialViewResult Edit(int? id)
    {
        var dto = new CreateOrUpdateUserPhoneDto();
        if (id.HasValue) dto = _userPhoneService.GetForEditById(id.Value);

        return PartialView("_Edit", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(CreateOrUpdateUserPhoneDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _userPhoneService.Update(dto);
                if (isUpdated)
                    return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Edit", dto);
    }

    [HttpGet]
    public PartialViewResult Delete(int id)
    {
        var serviceItem = _userPhoneService.GetById(id);

        return PartialView("_Delete", serviceItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        _userPhoneService.DeleteById(id);
        return RedirectToAction(nameof(Index));
    }
}