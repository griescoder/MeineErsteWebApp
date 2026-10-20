using MeineErsteWebApp.Repositories;
using MeineErsteWebApp.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddSingleton<TodoRepository>();
builder.Services.AddSingleton<ExpenseRepository>();

builder.Services.AddScoped<TodoService>();
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetRequiredService<TodoRepository>().Initialize();
    scope.ServiceProvider.GetRequiredService<ExpenseRepository>().Initialize();
}
if (!app.Environment.IsDevelopment()) 
{ 
    app.UseExceptionHandler("/Error"); 
    app.UseHsts(); 
}
app.UseHttpsRedirection(); app.UseRouting(); app.UseAuthorization(); app.MapStaticAssets(); app.MapRazorPages().WithStaticAssets(); app.Run();
