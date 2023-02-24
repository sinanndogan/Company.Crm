using Company.Crm.Application.Constants;
using Company.Crm.Application.Dtos.UserEmail;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Domain.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class UserEmailController : Controller
{
    private readonly IUserEmailService _userEmailService;
    private readonly IValidator<CreateOrUpdateUserEmailDto> _userEmailValidator;

    public UserEmailController(IUserEmailService userEmailService, IValidator<CreateOrUpdateUserEmailDto> userEmailValidator)
    {
        _userEmailService = userEmailService;
        _userEmailValidator = userEmailValidator;
    }

    public IActionResult Index(int page = 1)
    {
        var userEmail = _userEmailService.GetPaged(page);
        return View(userEmail);
    }

    public PartialViewResult Detail(int id)
    {
        var userEmail = _userEmailService.GetById(id);
        return PartialView("_Detail", userEmail);
    }

    [HttpGet]
    public PartialViewResult Create()
    {
        CreateOrUpdateUserEmailDto dto = new();
        FillDropdownItems(dto);
        return PartialView("_Create", dto);
    }

    private void FillDropdownItems(CreateOrUpdateUserEmailDto dto)
    {
        dto.EmailTypes?.AddRange(new[]
        {
            new SelectListItem { Text = EmailTypeEnum.CustomEmail.ToString(), Value = ((int)EmailTypeEnum.CustomEmail).ToString() },
            new SelectListItem { Text = EmailTypeEnum.WorkEmail.ToString(), Value = ((int)EmailTypeEnum.WorkEmail).ToString() },
            new SelectListItem { Text = EmailTypeEnum.MobileEmail.ToString(), Value = ((int)EmailTypeEnum.MobileEmail).ToString() },
            new SelectListItem { Text = EmailTypeEnum.OtherEmail.ToString(), Value = ((int)EmailTypeEnum.OtherEmail).ToString() }
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateOrUpdateUserEmailDto item)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isInserted = _userEmailService.Insert(item);
                if (isInserted)
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

    public async Task<PartialViewResult> Edit(int? id)
    {
        var dto = new CreateOrUpdateUserEmailDto();
        if (id.HasValue) dto = _userEmailService.GetForEditById(id.Value);

        return PartialView("_Edit", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CreateOrUpdateUserEmailDto dto)

    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _userEmailService.Update(dto);

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
        var serviceItem = _userEmailService.GetById(id);
        return PartialView("_Delete", serviceItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        _userEmailService.DeleteById(id);
        return RedirectToAction(nameof(Index));
    }
}