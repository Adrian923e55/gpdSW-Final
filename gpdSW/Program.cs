using Microsoft.AspNetCore.Authentication.Cookies; // --

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvc();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(); // --

var app = builder.Build();

app.UseAuthentication(); // --
app.UseAuthorization(); // --

app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();
