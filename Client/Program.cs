using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CMVMD.Client;
using Radzen;
using CMVMD.Client.Services.Article;
using CMVMD.Client.Services.Member;
using CMVMD.Client.Services.Document;
using CMVMD.Client.Services;
using CMVMD.Client.Services.Event;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddRadzenComponents();
builder.Services.AddHttpClient();

await builder.Build().RunAsync();
