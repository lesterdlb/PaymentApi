using System.Text;
using Data.Context;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Api.Authentication;
using Data.Services.CreditCards;
using Data.Services.Transactions;
using Data.Services.FeeExchange;
using PaymentFeesWorker;

var builder = WebApplication.CreateBuilder(args);

// Contexts
builder.Services.AddDbContext<DatabaseContext>();

// Services
builder.Services.AddScoped<IFeeExchangeService, FeeExchangeService>();
builder.Services.AddScoped<ICreditCardService, CreditCardService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// Worker
builder.Services.AddHostedService<UniversalFeesExchangeWorker>();

// Jwt
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection(JwtSettings.SectionName));
builder.Services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
                (Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
        };
    });

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();