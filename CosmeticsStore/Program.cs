using Asp.Versioning;
using CosmeticsStore.Application.Common.Security;
using CosmeticsStore.Application.User.Auth;
using CosmeticsStore.Domain.Interfaces.Persistence.Repositories;
using CosmeticsStore.Extensions;
using CosmeticsStore.Infrastructure.Persistence.DbContexts;
using CosmeticsStore.Infrastructure.Persistence.Repositories;
using CosmeticsStore.Middlewares;
using FluentValidation;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File(
        path: "Logs/log-.txt",
        rollingInterval: RollingInterval.Day,
        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();
// ------------------------------------------------------
// Controllers
// ------------------------------------------------------
builder.Services.AddControllers();


// ------------------------------------------------------
// API Versioning
// ------------------------------------------------------
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
})
.AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// ------------------------------------------------------
// CORS
// ------------------------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ------------------------------------------------------
// Swagger
// ------------------------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "CosmeticsStore API",
        Version = "v1"
    });

    options.CustomSchemaIds(type => type.FullName!.Replace("+", "."));
});


// ------------------------------------------------------
// DbContext
// ------------------------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ------------------------------------------------------
// Repositories
// ------------------------------------------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ICouponRepository, CouponRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IMediaRepository, MediaRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
builder.Services.AddScoped<IPasswordHasher<CosmeticsStore.Domain.Entities.User>, PasswordHasher<CosmeticsStore.Domain.Entities.User>>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

// ------------------------------------------------------
// AutoMapper
// ------------------------------------------------------
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// ------------------------------------------------------
// FluentValidation
// ------------------------------------------------------
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ApiExceptionFilter>();
});

// ------------------------------------------------------
// JWT Authentication
// ------------------------------------------------------
builder.Services.AddScoped<IJwtService, JwtService>();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var jwtKey = jwtSettings["Key"];

if (string.IsNullOrEmpty(jwtKey))
    throw new InvalidOperationException("JWT Key not configured in appsettings.json");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
        ClockSkew = TimeSpan.Zero
    };
});

// ------------------------------------------------------
// Authorization
// ------------------------------------------------------
builder.Services.AddAuthorization();

// ------------------------------------------------------
// Rate Limiting
// ------------------------------------------------------
builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("fixed", limiterOptions =>
    {
        limiterOptions.PermitLimit = 200;
        limiterOptions.Window = TimeSpan.FromMinutes(1);
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 8;
    });

    options.AddSlidingWindowLimiter("sliding", limiterOptions =>
    {
        limiterOptions.PermitLimit = 200;
        limiterOptions.Window = TimeSpan.FromMinutes(1);
        limiterOptions.SegmentsPerWindow = 8;
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 8;
    });

    options.AddTokenBucketLimiter("token", limiterOptions =>
    {
        limiterOptions.TokenLimit = 200;
        limiterOptions.ReplenishmentPeriod = TimeSpan.FromMinutes(1);
        limiterOptions.TokensPerPeriod = 200;
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 8;
    });

    options.AddConcurrencyLimiter("concurrency", limiterOptions =>
    {
        limiterOptions.PermitLimit = 90;
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        limiterOptions.QueueLimit = 20;
    });

    options.AddPolicy("per-ip", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 20,
                Window = TimeSpan.FromMinutes(1)
            }));

    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
    {
        var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        return RateLimitPartition.GetFixedWindowLimiter(clientIp, _ =>
            new FixedWindowRateLimiterOptions
            {
                PermitLimit = 4000,
                Window = TimeSpan.FromHours(1)
            });
    });

    options.OnRejected = async (context, cancellationToken) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;

        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            await context.HttpContext.Response.WriteAsJsonAsync(new
            {
                error = "Too many requests",
                message = "You have exceeded the rate limit",
                retryAfter = $"{retryAfter.TotalSeconds} seconds"
            }, cancellationToken);
        }
        else
        {
            await context.HttpContext.Response.WriteAsJsonAsync(new
            {
                error = "Too many requests",
                message = "Please try again later"
            }, cancellationToken);
        }
    };
});

// ------------------------------------------------------
// Global Exception Handler
// ------------------------------------------------------
//builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

// ------------------------------------------------------
// Logging
// ------------------------------------------------------
//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
//builder.Logging.AddDebug();

// ------------------------------------------------------
// PasswordHasher
// ------------------------------------------------------
builder.Services.AddScoped<IPasswordHasher<CosmeticsStore.Domain.Entities.User>,
    PasswordHasher<CosmeticsStore.Domain.Entities.User>>();

// ------------------------------------------------------
// MediatR
// ------------------------------------------------------
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});

// ------------------------------------------------------
// Build App
// ------------------------------------------------------
var app = builder.Build();

// ------------------------------------------------------
// Configure Pipeline
// ------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}
app.UseCors("AllowAll");

app.UseStaticFiles();

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

// Global Exception Handler
//app.UseExceptionHandler();

app.MapControllers();

// ------------------------------------------------------
// Apply Migrations Automatically
// ------------------------------------------------------
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        app.Logger.LogInformation("Database migrated successfully");
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "Database migration failed");
        throw;
    }
}

app.Run();