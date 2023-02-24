using Company.Crm.Application;
using Company.Crm.Entityframework;
using Company.Crm.Web.Mvc;
using Company.Crm.Web.Mvc.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddEntityFrameworkRegistration(builder.Configuration);
builder.Services.AddApplicationRegistration(builder.Configuration);

builder.Services.AddMvcRegistration(builder.Configuration);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

/*
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Response.ContentType = "text/html";
        //context.Response.ContentType = "application/json";

        var errorMessage = "Error!";

        var error = context.Features.Get<IExceptionHandlerPathFeature>();

        var exception = error?.Error;

        if (exception is FileNotFoundException)
        {
            errorMessage = "File Not Found!";
        }
        else if (exception is UnauthorizedAccessException)
        {
            //context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            errorMessage = "Unauthorized Access to resource!";
        }
        else if (exception is ValidationException)
        {
            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            errorMessage = "Validation error!";
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            errorMessage = "Internal error!";
        }

        await context.Response.WriteAsync(errorMessage);
    });
});
*/

//app.UseIpLogging();
app.UseGlobalExceptionHandling();
//app.UseMiddleware<ExceptionHandlingMiddleware>();

//app.UseStatusCodePages();
app.UseStatusCodePagesWithRedirects("/Error/{0}"); // {0} hata kodunu ifade eder

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
