using Microsoft.EntityFrameworkCore;
using NutriPlan.Data;
using NutriPlan.Services;
using NutriPlan.Services.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register the database context with the dependency injection container
builder.Services.AddDbContext<NutriPlanContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NutriPlanContext")));

// Register services
builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<IIntakeService, IntakeService>();
builder.Services.AddScoped<IMealPlanService, MealPlanService>();
builder.Services.AddScoped<IMealService, MealService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern:  "{controller=Home}/{action=Index}/{id?}");

app.Run();
