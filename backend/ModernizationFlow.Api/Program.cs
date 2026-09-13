
using Microsoft.EntityFrameworkCore;
using ModernizationFlow.Application.Interfaces;
using ModernizationFlow.Application.Requests.Approve;
using ModernizationFlow.Application.Requests.Create;
using ModernizationFlow.Application.Requests.GetById;
using ModernizationFlow.Application.Requests.Reject;
using ModernizationFlow.Application.Requests.Search;
using ModernizationFlow.Application.Requests.Submit;
using ModernizationFlow.Application.Requests.Update;
using ModernizationFlow.Infrastructure.Persistence;
using ModernizationFlow.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories
builder.Services.AddScoped<IRequestRepository, RequestRepository>();

// Application Use Cases
builder.Services.AddScoped<CreateRequestHandler>();
builder.Services.AddScoped<GetRequestByIdHandler>();
builder.Services.AddScoped<SearchRequestsHandler>();
builder.Services.AddScoped<UpdateRequestHandler>();
builder.Services.AddScoped<SubmitRequestHandler>();
builder.Services.AddScoped<ApproveRequestHandler>();
builder.Services.AddScoped<RejectRequestHandler>();

var app = builder.Build();

// HTTP pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("ReactFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();

