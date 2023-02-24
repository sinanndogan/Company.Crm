using Company.Crm.Application.Constants;
using Company.Crm.Application.Dtos.Request;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class RequestController : Controller
{
    private readonly IRequestService _requestService;

    private readonly IValidator<CreateOrUpdateRequestDto> _requestValidator;

    public RequestController(IRequestService requestService, IValidator<CreateOrUpdateRequestDto> requestValidator)
    {
        _requestService = requestService;
        _requestValidator = requestValidator;
    }

    public IActionResult Index(int page = 1)
    {
        var requests = _requestService.GetPaged(page);
        return View(requests);
    }

    public PartialViewResult Detail(int id)
    {
        var request = _requestService.GetById(id);
        return PartialView("_Detail", request);
    }

    public PartialViewResult Create()
    {
        var dto = new CreateOrUpdateRequestDto();

        return PartialView("_Create", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(CreateOrUpdateRequestDto item)
    {
        try
        {
            var validationResult = _requestValidator.Validate(item);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);

                return PartialView("_Create", item);
            }

            if (validationResult.IsValid)
            {
                var isInserted = _requestService.Insert(item);
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
        var dto = new CreateOrUpdateRequestDto();
        if (id.HasValue) dto = _requestService.GetForEditById(id.Value);

        return PartialView("_Edit", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(CreateOrUpdateRequestDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _requestService.Update(dto);
                if (isUpdated) return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
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
        var data = _requestService.GetById(id);
        return PartialView("_Delete", data);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        return Json(new { IsSuccess = _requestService.DeleteById(id), Redirect = Url.Action("Index") });
    }
}