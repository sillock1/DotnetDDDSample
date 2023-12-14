using Ordering.API.Apis;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.AddApplicationServices();

builder.Services.AddHttpContextAccessor();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering.API", Version = "v1" });
});
var app = builder.Build();
app.UseExceptionHandler();
app.UseStatusCodePages();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}


app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.API");
});

app.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();


app.MapGroup("/api/v1/order")
    .MapOrderApi();
app.Run();