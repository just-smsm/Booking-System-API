using BookingSystem.Application.CQRS.Handlers.Commands;
using BookingSystem.Application.CQRS.Handlers.Queries;
using BookingSystem.Application.CQRS.Mapping;
using BookingSystem.Core.Entities;
using BookingSystem.Core.Interfaces;
using BookingSystem.Infrastructure.Data;
using BookingSystem.Infrastructure.Repositories;
using BookingSystem.Infrastructure.UnitOfWork;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Add repositories and Unit of Work
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add AutoMapper for DTO mappings
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add MediatR for CQRS
builder.Services.AddMediatR(typeof(GetAllReservationsHandler).Assembly);

// Add Controllers
builder.Services.AddControllers();

// Register Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Booking System API",
        Version = "v1",
        Description = "API for managing reservations and trips.",
        Contact = new OpenApiContact
        {
            Name = "Support Team",
            Email = "support@bookingsystem.com",
            Url = new Uri("https://bookingsystem.com")
        }
    });

    //  Enable JWT Authentication support in Swagger 
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer {token}' to authenticate"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Configure the middleware pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Booking System API v1");
        c.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

// Seed Database on startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    await DbInitializer.SeedAsync(context, userManager);
}

app.Run();
