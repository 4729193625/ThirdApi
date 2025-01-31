using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ThirdAPI.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.MapType<Book>(() => new OpenApiSchema
    {
        Type = "object",
        Properties =
        {
            ["name"] = new OpenApiSchema { Type = "string", Example = new OpenApiString("Book Name") },
            ["price"] = new OpenApiSchema { Type = "number", Format = "decimal", Example = new OpenApiDouble(0.00) },
        }
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
