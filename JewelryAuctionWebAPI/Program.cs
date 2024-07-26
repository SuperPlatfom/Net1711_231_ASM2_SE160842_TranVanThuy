using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using JewelryAuctionBusiness;
using JewelryAuctionBusiness.AutoMap;
using JewelryAuctionData;
using JewelryAuctionData.Entity;
using JewelryAuctionData.Repository;
using JewelryAuctionWebAPI.BackgroundService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "JewelryAuctionWebAPI", Version = "v1" });
    // Security Definition JWT
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
        // Customize the response for unauthorized requests
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                // Skip the default logic
                context.HandleResponse();

                // Set custom response
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(
                    new BusinessResult(404, "Authentication failed: Invalid JWT token."),
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                return context.Response.WriteAsync(result);
            },
            OnForbidden = context =>
            {
                // Set custom response
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(
                    new BusinessResult(403, "Authorization failed: You do not have the required role."),
                    new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                return context.Response.WriteAsync(result);
            }
        };
    });

// Add DbContext
builder.Services.AddDbContext<Net1711_231_7_JewelryAuctionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));

// Register UnitOfWork and business services
builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped<AccountBusiness>();
builder.Services.AddScoped<AuctionResultBusiness>();
builder.Services.AddScoped<AccountRepository>();

// Add background services
builder.Services.AddHostedService<AuctionStatusUpdater>();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Configure JSON options
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Add authorization services
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "JewelryAuctionWebAPI v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
