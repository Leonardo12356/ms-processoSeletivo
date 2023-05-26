using Microsoft.EntityFrameworkCore;
using ms_processoSeletivo.Data;
using ms_processoSeletivo.Domain;
using ms_processoSeletivo.Exceptions;
using ms_processoSeletivo.Exceptions.Interfaces;
using ms_processoSeletivo.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IPessoa, PessoaDomain>();
builder.Services.AddScoped<IPessoaException, PessoaException>();



// AddDbContext -> postgres
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseLazyLoadingProxies().UseNpgsql(builder.Configuration.GetConnectionString("Default"),
    assembly => assembly.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
    );
});

// AddAutoMapper 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
