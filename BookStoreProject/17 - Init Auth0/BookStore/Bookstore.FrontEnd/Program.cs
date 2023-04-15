using Blazored.Modal;
using Blazored.Toast;
using Bookstore.FrontEnd;
using Bookstore.FrontEnd.Clients;
using Bookstore.FrontEnd.Configuration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//configuration
var http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
builder.Services.AddScoped(sp => http);
using var response = await http.GetAsync("appsettings.json");
using var stream = await response.Content.ReadAsStreamAsync();
builder.Configuration.AddJsonStream(stream);
var BookstoreApiSettings = new BookstoreApiSettings();
builder.Configuration.GetSection("BookstoreApiSettings").Bind(BookstoreApiSettings);


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddBlazoredModal();
builder.Services.AddBlazoredToast();

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
});



builder.Services.AddHttpClient<BookStoreClient>(client =>
{
    client.BaseAddress = new Uri(BookstoreApiSettings.BaseUrl);
    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
});


await builder.Build().RunAsync();
