using Company.Crm.Application.Constants;
using Company.Crm.Application.Dtos.Notification;
using Company.Crm.Application.Services.Abstracts;
using Company.Framework.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Company.Crm.Web.Mvc.Areas.Admin.Controllers;

[Authorize(Roles = RoleNameConsts.Administrator)]
[Area("Admin")]
public class NotificationController : Controller
{
    private readonly INotificationService _notificationService;
    private readonly IUserService _userService;

    public NotificationController(INotificationService notificationService, IUserService userService)
    {
        _notificationService = notificationService;
        _userService = userService;
    }

    public IActionResult Index(int page = 1)
    {
        var notifications = _notificationService.GetPaged(new PaginationRequest { Page = page });
        return View(notifications.Data);
    }

    [HttpGet]
    public PartialViewResult Create()
    {
        NotificationCreateOrUpdateDto entity = new();
        return PartialView("_Create", entity);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(NotificationCreateOrUpdateDto notification)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isInserted = _notificationService.Insert(notification);
                if (isInserted.IsSuccess)
                    return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }


        return PartialView("_Create", notification);
    }

    [HttpGet]
    public PartialViewResult Detail(int id)
    {
        var notification = _notificationService.GetById(id);
        return PartialView("_Detail", notification.Data);
    }

    [HttpGet]
    public PartialViewResult Delete(int id)
    {
        var notification = _notificationService.GetById(id);
        return PartialView("_Delete", notification.Data);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        return Json(new { IsSuccess = _notificationService.DeleteById(id), Redirect = Url.Action("Index") });
    }

    [HttpGet]
    public PartialViewResult Edit(int? id)
    {
        var dto = new NotificationCreateOrUpdateDto();
        if (id.HasValue) dto = _notificationService.GetForEditById(id.Value).Data;
        return PartialView("_Edit", dto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(NotificationCreateOrUpdateDto notification)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var isUpdated = _notificationService.Update(notification);
                if (isUpdated.IsSuccess)
                    return Json(new { IsSuccess = true, Redirect = Url.Action("Index") });
            }
        }
        catch
        {
            ModelState.AddModelError("", "Unable to save changes.");
        }

        return PartialView("_Edit", notification);
    }

    [HttpGet]
    public ActionResult MarkAsRead(int id)
    {
        _notificationService.MarkAsReadOrUnread(id);
        return RedirectToAction(nameof(Index));
    }
}