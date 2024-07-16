using DigitalPrint.Core.ApplicationServices.Product.CommandHandlers;
using DigitalPrint.Core.ApplicationServices.UserProfiles.CommandHandlers;
using DigitalPrint.Core.Domain.Products.Data;
using DigitalPrint.Core.Domain.UserProfiles.Data;
using DigitalPrint.Infrastructures.Data.EventStore;
using DigitalPrint.Infrastructures.Data.SqlServer;
using DigitalPrint.Infrastructures.Data.SqlServer.Product;
using DigitalPrint.Infrastructures.Data.SqlServer.UserProfiles;
using EventStore.ClientAPI;
using Framework.Domain.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product", Version = "v1" }); });

#region EventStore
var esConnection = EventStoreConnection.Create(builder.Configuration["EventStore:ConnectionString"], ConnectionSettings.Create().KeepReconnecting(), builder.Environment.ApplicationName);
esConnection.ConnectAsync().Wait();
var store = new DigitalPrintEventSource(esConnection);
builder.Services.AddSingleton(esConnection);
builder.Services.AddSingleton<IEventSource>(store);
#endregion
#region UOW
builder.Services.AddScoped<IUnitOfWork, ProductUnitOfWork>();
#endregion
#region ORM
builder.Services.AddScoped(c => new SqlConnection(builder.Configuration.GetConnectionString("ProductCnn")));
builder.Services.AddDbContext<ProductDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ProductCnn")));
#endregion
#region QueryService
builder.Services.AddScoped<IProductQueryService, SqlProductQueryService>();
#endregion
#region Product Agg
builder.Services.AddScoped<IProductsRepository, EfProductsRepository>();
builder.Services.AddScoped<CreateHandler>();
builder.Services.AddScoped<SendToPublishHandler>();
#endregion
#region UserProfile Agg
builder.Services.AddScoped<IUserProfileRepository, EfUserProfileRepository>();
builder.Services.AddScoped<RegisterUserHandler>();
builder.Services.AddScoped<UpdateUserNameHandler>();
builder.Services.AddScoped<UpdateUserEmailHandler>();
builder.Services.AddScoped<UpdateUserDisplayNameHandler>();
#endregion

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product API V1"); });
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
