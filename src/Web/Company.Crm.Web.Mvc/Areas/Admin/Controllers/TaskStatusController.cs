using Company.Crm.Application.Constants;
using Company.Crm.Application.Services.Abstracts;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskStatus = Company.Crm.Domain.Entities.Lst.TaskStatus;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class TaskStatusController : Controller
{
    private readonly ITaskStatusService _taskStatusService;

    public TaskStatusController(ITaskStatusService taskStatusService)
    {
        _taskStatusService = taskStatusService;
    }

    public IActionResult Index(PaginationRequest request)
    {
        var taskStatus = _taskStatusService.GetPaged(request);
        return View(taskStatus.Data);
    }

    public PartialViewResult Detail(int id)
    {
        var taskstatus = _taskStatusService.GetById(id);
        return PartialView("_Detail", taskstatus.Data);
    }

    [HttpGet]
    public PartialViewResult Create()
    {
        var taskstatus = new TaskStatus();

        return PartialView("_Create", taskstatus);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(TaskStatus taskStatus)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isInserted = _taskStatusService.Insert(taskStatus);
                if (isInserted.Data)
                {
                    return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
                }
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Create", taskStatus);
    }

    [HttpGet]
    public PartialViewResult Edit(int? id)
    {
        var taskStatus = new TaskStatus();
        if (id.HasValue)
        {
            taskStatus = _taskStatusService.GetForEditById(id.Value).Data;
        }

        return PartialView("_Edit", taskStatus);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(TaskStatus taskstatus)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _taskStatusService.Update(taskstatus);
                if (isUpdated.Data)
                    return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Edit", taskstatus);
    }

    [HttpGet]
    public PartialViewResult Delete(int id)
    {
        var taskstatus = _taskStatusService.GetById(id);

        return PartialView("_Delete", taskstatus.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        return Json(new { IsSuccess = _taskStatusService.DeleteById(id).Data, Redirect = Url.Action("Index") });
    }
}