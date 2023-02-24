using Company.Crm.Application;
using Company.Crm.Entityframework;
using Company.Crm.Web.Api;
using Company.Crm.Web.Api.Filters;
using Company.Crm.Web.Api.Middlewares;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    if (Environment.GetEnvironmentVariable("IS_MONGO_LOG_ACTIVE") == "1")
        options.Filters.Add<LogActionFilter>();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Company CRM API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddEntityFrameworkRegistration(builder.Configuration);
builder.Services.AddApplicationRegistration(builder.Configuration);
builder.Services.AddApiRegistration(builder.Configuration);


var app = builder.Build();

//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp => errorApp.UseGlobalExceptionHandling(app.Environment.IsProduction()));
app.UseHttpsRedirection();
app.UseCors("CrmCors");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();