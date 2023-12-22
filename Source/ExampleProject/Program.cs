using ExampleProject;
using ExampleProject.Models;
using ExampleProject.Permissions;
using MicroPermissions;
using Microsoft.AspNetCore.Authentication.Negotiate;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

// Register MicroPermissions
builder.Services.AddMicroPermissions<MyPermissionContext>();
builder.Services.AddHttpContextAccessor(); // we're adding IHttpAncestor for demo purposes, required for our custom MyPermissionContext.
builder.Services.AddScoped<MyPermissionContext>(); // add permission context to IoC

builder.Services.AddSingleton<TaskRepository>();

// register handlers & filters
builder.Services.AddTransient<IPermissionHandler<MyPermissionContext, IncrementRequest>, IncrementRequestPermissionHandler>();
builder.Services.AddTransient<IPermissionHandler<MyPermissionContext, CreateTaskRequest>, CreateTaskPermissionHandler>();
builder.Services.AddTransient<IPermissionFilter<MyPermissionContext, IQueryable<TaskEntity>>, TaskPermissionFilter>();

builder.Services.AddAuthorization(options => { options.FallbackPolicy = options.DefaultPolicy; });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
