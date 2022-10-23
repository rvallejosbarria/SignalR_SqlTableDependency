using SignalR_SqlTableDependency.Hubs;
using SignalR_SqlTableDependency.MiddlewareExtensions;
using SignalR_SqlTableDependency.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

// Dependency Injection
builder.Services.AddSingleton<DashboardHub>();
builder.Services.AddSingleton<SubscribeProductTableDependency>();

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
app.MapHub<DashboardHub>("/dashboardHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

/*
 * we must call SubscribeTableDependency() here
 * we create one middleware and call SubscribeTableDependency() method in the middleware
 */
app.UseProductTableDependency();

app.Run();

/*
 * must enable service broker in the database to retrieve data
 * it can be done using the following commands:
 * ALTER DATABASE SignalRDemo SET enable_broker WITH ROLLBACK IMMEDIATE
 * SELECT name, is_broker_enabled FROM sys.databases
 */