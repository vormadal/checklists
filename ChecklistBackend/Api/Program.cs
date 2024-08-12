using Application;
using Persistence;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRepositories(builder.Configuration.GetConnectionString("ChecklistDatabase") ?? throw new Exception("Missing DB connection string"))
    .AddApplication()
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    config.CustomOperationIds(e => e.ActionDescriptor.RouteValues["action"]);
    config.UseOneOfForPolymorphism();

    config.SelectDiscriminatorNameUsing(baseType => "TypeName");
    config.SelectDiscriminatorValueUsing(subType => subType.Name);

    var controllersXmlFilename = $"{typeof(Contracts.AssemblyReference).Assembly.GetName().Name}.xml";

    config.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, controllersXmlFilename));
    config.SupportNonNullableReferenceTypes();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.AllowAnyMethod();
    x.WithOrigins("https://localhost:3000");
});

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.AddDatabaseMigrations();
    if (app.Environment.IsDevelopment())
    {
        await scope.ServiceProvider.AddDatabaseTestData();
    }
}

app.Run();
