using Aplication.Productos;
using Connection.Connection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureCors ( );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<YeminusContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddMediatR(typeof(GetProductos.Managment).Assembly);
builder.Services.AddAutoMapper(typeof(GetProductos.Managment));
builder.Services.AddSwaggerGen ( c => {
    c.SwaggerDoc ( "v1", new OpenApiInfo {
        Title = "Services",
        Version = "v1"
    } );
    c.CustomSchemaIds ( c => c.FullName );
} );

var app = builder.Build();

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment ( ) ) {
    app.UseSwagger ( );
    app.UseSwaggerUI ( c => {
        c.SwaggerEndpoint ( "/swagger/v1/swagger.json", "Yeminus v1" );
    } );
}

app.UseCors ( "CorsPolicy" );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
