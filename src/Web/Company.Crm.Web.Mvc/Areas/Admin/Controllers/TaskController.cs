using Company.Crm.Application.Constants;
using Company.Crm.Application.Dtos.Task;
using Company.Crm.Application.Services.Abstracts;
using Company.Crm.Application.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class TaskController : Controller
{
    private readonly IStatusTypeService _statusTypeService;
    private readonly ITaskService _taskService;
    private readonly IValidator<CreateOrUpdateTaskDto> _taskValidator;

    public TaskController(ITaskService taskService, IStatusTypeService statusTypeService, IValidator<CreateOrUpdateTaskDto> taskValidator)
    {
        _taskService = taskService;
        _statusTypeService = statusTypeService;
        _taskValidator = taskValidator;
    }

    public IActionResult Index(int page = 1)
    {
        var tasks = _taskService.GetPaged(new() { Page = page });
        return View(tasks);
    }

    public async Task<PartialViewResult> Detail(int id)
    {
        var task = _taskService.GetById(id);
        return PartialView("_Detail", task);
    }

    [HttpGet]
    public PartialViewResult Create()
    {
        var dto = new CreateOrUpdateTaskDto();


        return PartialView("_Create", dto);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(CreateOrUpdateTaskDto item)
    {
        try
        {
            var validationResult = _taskValidator.Validate(item);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(ModelState);

                return PartialView("_Create", item);
            }

            if (validationResult.IsValid)
            {
                var isInserted = _taskService.Insert(item);
                if (isInserted.Data)
                    return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }


        return PartialView("_Create", item);
    }

    public async Task<PartialViewResult> Edit(int? id)
    {
        var dto = new CreateOrUpdateTaskDto();
        if (id.HasValue) dto = _taskService.GetForEditById(id.Value).Data;


        return PartialView("_Edit", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(CreateOrUpdateTaskDto dto)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _taskService.Update(dto);
                if (isUpdated.Data)
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
    public async Task<PartialViewResult> Delete(int id)
    {
        var serviceItem = _taskService.GetById(id);

        return PartialView("_Delete", serviceItem);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteConfirmed(int id)
    {
        _taskService.DeleteById(id);
        return RedirectToAction(nameof(Index));
    }
}