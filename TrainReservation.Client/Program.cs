using TrainReservation.Client.Configurations;
using TrainReservation.Client.Interfaces;
using TrainReservation.Client.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IBookingApiService, BookingApiService>();

builder.Services.AddScoped<IPredictionApiService, PredictionApiService>();

builder.Services.AddScoped<IReportApiService, ReportApiService>();

builder.Services.Configure<GatewaySettings>(
    builder.Configuration.GetSection("Gateway"));

builder.Services.AddHttpClient<IGatewayClient, GatewayClient>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

// REQUIRED to serve CSS, JS, Bootstrap, images, etc.
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

// Add this if you later introduce ASP.NET authentication.
// It is harmless if no authentication scheme is configured.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();