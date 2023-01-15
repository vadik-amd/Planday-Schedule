using System.Net.Mail;
using System.Reflection;
using Planday.Schedule.Domain.Extensions;
using Planday.Schedule.Infrastructure.Extensions;
using FluentValidation.AspNetCore;
using Planday.Schedule.Api.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddSingleton<SmtpClient>();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddDomainServices();
builder.Services.AddAutoMapper(typeof(ShiftModelMappingProfile).Assembly);
builder.Services.AddFluentValidation(options =>
{
    // Validate child properties and root collection elements
    options.ImplicitlyValidateChildProperties = true;
    options.ImplicitlyValidateRootCollectionElements = true;

    // Automatic registration of validators in assembly
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
