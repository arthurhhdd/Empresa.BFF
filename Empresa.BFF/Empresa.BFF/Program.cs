using Empresa.Application.UseCases.Interfaces;
using Empresa.Application.UseCases;
using Empresa.Infra;
using Empresa.Domain.Adapters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<EmpresaDbContext>();
builder.Services.AddScoped<IGetLeadsByStatus, GetLeadsByStatus>();
builder.Services.AddScoped<IUpdateLeadStatus, UpdateLeadStatus>();
builder.Services.AddScoped<ILeadsRepository, LeadsRepository>();
builder.Services.AddScoped<IEmailService, FakeEmail>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

