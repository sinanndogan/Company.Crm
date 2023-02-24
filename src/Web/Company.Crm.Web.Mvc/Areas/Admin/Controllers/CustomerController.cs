using Company.Crm.Application.Constants;
using Company.Crm.Application.Dtos;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Application.Validators;
using Company.Framework.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;
    private readonly IValidator<CreateOrUpdateCustomerDto> _customerValidator;
    private readonly IGenderService _genderService;
    private readonly IStatusTypeService _statusTypeService;
    private readonly ITitleService _titleService;

    public CustomerController(ICustomerService customerService, IStatusTypeService statusTypeService, IValidator<CreateOrUpdateCustomerDto> customerValidator, IGenderService genderService, ITitleService titleService)
    {
        _customerService = customerService;
        _statusTypeService = statusTypeService;
        _customerValidator = customerValidator;
        _genderService = genderService;
        _titleService = titleService;
    }

    public IActionResult Index(int page = 1)
    {
        var customers = _customerService.GetPaged(new PaginationRequest { Page = page });
        return View(customers.Data);
    }

    public PartialViewResult Detail(int id)
    {
        var customer = _customerService.GetById(id);
        return PartialView("_Detail", customer.Data);
    }

    [HttpGet]
    public PartialViewResult Create()
    {
        var dto = new CreateOrUpdateCustomerDto();

        FillDropdownItems(dto);

        return PartialView("_Create", dto);
    }

    private void FillDropdownItems(CreateOrUpdateCustomerDto dto)
    {
        dto.Genders = _genderService.GetAll()
            .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
            .ToList();

        dto.Titles = _titleService.GetAll()
            .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
            .ToList();

        dto.StatusTypes = _statusTypeService.GetAll()
            .Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name })
            .ToList();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(CreateOrUpdateCustomerDto item)
    {
        try
        {
            //var validationResult = new CustomerValidator().Validate(item);
            var validationResult = _customerValidator.Validate(item);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);

                return PartialView("_Create", item);
            }

            //if (ModelState.IsValid)
            if (validationResult.IsValid)
            {
                var isInserted = _customerService.Insert(item);
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
        var dto = new CreateOrUpdateCustomerDto();
        if (id.HasValue)
        {
            var customer = _customerService.GetForEditById(id.Value);
            dto = customer.Data;
        }

        FillDropdownItems(dto);

        return PartialView("_Edit", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CreateOrUpdateCustomerDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _customerService.Update(dto);
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
        var customer = _customerService.GetById(id);

        return PartialView("_Delete", customer.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        _customerService.DeleteById(id);

        return RedirectToAction(nameof(Index));
    }
}