using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Qfx.Cinema.Web;
using Qfx.Cinema.Web.HttpHandler;
using Refit;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region Refit setup
builder.Services.AddTransient<HttpResponseHandler>();
builder.Services.AddRefitClient<IApiService>()
        .ConfigureHttpClient(client =>
        {
            client.BaseAddress = new Uri("https://web-api.qfxcinemas.com");
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ0b2tlbiI6IjBmZDc1OWM2LTczMTYtNDdlZi1iZmYyLTg3ZWYwNTYxYWUxMCIsImlhdCI6MTY3MjEzNzgwOX0.niE2hcIACf-Fu_glcbh8Vtm5f83WUjygGG9GbJTw4-o");
        })/*.AddHttpMessageHandler<HttpResponseHandler>()*/;
#endregion

await builder.Build().RunAsync();
