using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ThirdAPIv3;
using ThirdAPI.Models;
using ThirdAPI.Dtos;
using ThirdAPI.Datas;
using ThirdAPI.Interfaces;
using ThirdAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
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
            ["price"] = new OpenApiSchema { Type = "number", Format = "decimal", Example = new OpenApiDouble(0) },
        }
    });

     c.MapType<BookDto>(() => new OpenApiSchema
    {
        Type = "object",
        Properties =
        {
            ["id"] = new OpenApiSchema { Type = "integer", Format = "int32", Example = new OpenApiInteger(0) },
            ["name"] = new OpenApiSchema { Type = "string", Example = new OpenApiString("Updated Book Name") },
            ["price"] = new OpenApiSchema { Type = "number", Format = "decimal", Example = new OpenApiDouble(0.00) },
        }
    });
});

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory?.CreateScope())
    {
        var service = scope?.ServiceProvider.GetService<Seed>();
        service?.SeedDataContext();
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
     app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
