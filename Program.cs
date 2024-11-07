using FleetManagement.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();  // Add support for controllers and views
builder.Services.AddRazorPages();            // Add Razor Pages if you're using them
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add services to the container.
if (builder.Environment.IsEnvironment("Testing")) 
{
    builder.Services.AddDbContext<FleetDbContext>(options =>
        options.UseInMemoryDatabase("TestDatabase"));  // Use in-memory database for testing
}
else
{
    builder.Services.AddDbContext<FleetDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));  // Use SQL Server for development/production
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Configure the HTTP request pipeline.
app.UseCors("AllowAll"); 
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Set default route to VehiclesController's GetLatestLocations endpoint
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Vehicles}/{action=LatestLocations}/{id?}");

app.Run();
