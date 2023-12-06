using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddContelWorks(builder);
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseContelWorks(app.Environment);
await app.RunAsync();