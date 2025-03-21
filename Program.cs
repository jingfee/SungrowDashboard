using SungrowDashboard.Components;
using MudBlazor.Services;
using SungrowDashboard.Services;
using Azure.Messaging.ServiceBus;
using Azure.Data.Tables;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();

builder.Services.AddMudServices();

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSingleton(new ServiceBusClient(builder.Configuration["AppSettings:ServiceBusConnectionString"]));
var azureDataTableAccountName = builder.Configuration["AppSettings:AzureDataTableAccountName"];
var credential = new TableSharedKeyCredential(azureDataTableAccountName, builder.Configuration["AppSettings:AzureDataTableAccountKey"]);
builder.Services.AddSingleton(new TableClient(
    new Uri($"https://{azureDataTableAccountName}.table.core.windows.net"),
    builder.Configuration["AppSettings:AzureDataTableTableName"],
    credential));
builder.Services.AddScoped<IServiceBusService, ServiceBusService>();
builder.Services.AddScoped<IDataTableService, DataTableService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
