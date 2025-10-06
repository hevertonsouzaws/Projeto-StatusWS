using Microsoft.EntityFrameworkCore;
using StatusWS.Data;
using StatusWS.Middleware;
using StatusWS.Models;
using StatusWS.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStatusTypeService, StatusTypeService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IJiraService, JiraService>();
builder.Services.AddSingleton<Microsoft.AspNetCore.Identity.IPasswordHasher<Employee>, Microsoft.AspNetCore.Identity.PasswordHasher<Employee>>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policyBuilder =>
        {
            policyBuilder.WithOrigins("https://localhost:7208",
                                     "http://localhost:5006",
                                     "http://localhost:5173",
                                     "https://projeto-status-ws.vercel.app",
                                     "https://unmildewed-wilburn-obsequent.ngrok-free.dev")
                                     .AllowAnyHeader()
                                     .AllowAnyMethod();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
