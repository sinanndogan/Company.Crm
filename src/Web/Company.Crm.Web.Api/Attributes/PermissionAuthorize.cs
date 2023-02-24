using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Company.Crm.Web.Api.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public class PermissionAuthorize : AuthorizeAttribute, IAuthorizationFilter
{
    private string[] Permissions { get; set; }

    public PermissionAuthorize(params object[] permissions)
    {
        if (permissions.Any(p => p.GetType().BaseType != typeof(Enum)))
            throw new ArgumentException("permissionsRequired");

        Permissions = permissions.Select(p => Enum.GetName(p.GetType(), p)).ToArray();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        bool authorized = false;

        //var user = service.FindByUsername(HttpContext.Current.User.Identity.Name);
        //string userRole = Enum.GetName(typeof(Role), user.Role);

        var user = context.HttpContext.User;

        if (user.IsInRole(RoleEnum.Administrator.ToString()))
            authorized = true;

        var userPermissions = user.FindFirst("Permissions").Value.Split(",");
        foreach (var permission in Permissions)
        {
            if (userPermissions.Contains(permission))
            {
                authorized = true;
                break;
            }
        }

        //Eğer uymuyorsa Home/index sayfasına yönlendirilir.
        if (!authorized)
        {
            //var url = new UrlHelper(context.RequestContext);
            //var logonUrl = url.Action("Index", "Home", new { Id = 302, Area = "" });
            //context.Result = new RedirectResult(logonUrl);

            context.Result = new UnauthorizedResult();
        }
    }
}

public enum PermissionEnum
{
    P1 = 1,
    P2 = 2,
    P3 = 3
}

public enum RoleEnum
{
    Administrator,
    User
}