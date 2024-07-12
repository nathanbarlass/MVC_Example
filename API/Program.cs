using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Implementations;
using Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

app.MapGet("/users/{id}", (string id, IUserService userService) => {
    var user = userService.GetUserById(id);
    return user is not null ? Results.Ok(user) : Results.NotFound();
});

app.MapPost("/users", (ApplicationUser user, IUserService userService) =>
{
    userService.CreateUser(user);
    return Results.Created($"/users/{user.Id}", user);
});

app.MapPut("/users/{id}", (string id, ApplicationUser updatedUser, IUserService userService) =>
{
    var user = userService.GetUserById(id);
    if (user is null) return Results.NotFound();

    userService.UpdateUser(user);

    return Results.NoContent();
});

app.MapDelete("/users/{id}", (string id, IUserService userService) =>
{
    userService.DeleteUser(id);
    return Results.NoContent();
});

app.Run();
