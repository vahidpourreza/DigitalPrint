using DigitalPrint.Core.ApplicationServices.Product.CommandHandlers;
using DigitalPrint.Core.Domain.Products.Data;
using DigitalPrint.Infrastructures.Data.InMemory.Product;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>{c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product", Version = "v1" });});
builder.Services.AddScoped<IProductsRepository, InMemoryProdcutsRepository>();

builder.Services.AddScoped<CreateHandler>();
builder.Services.AddScoped<SetTitleHandler>();
builder.Services.AddScoped<UpdateDescriptionHandler>();
builder.Services.AddScoped<UpdatePriceHandler>();
builder.Services.AddScoped<SendToPublishHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1");
    });

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
