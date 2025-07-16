using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Saas.Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Design;
using Saas.Repository.TenantRep;
using Saas.Repository.UserRep;
using Saas.Repository.ProductRep;
using Saas.Repository.PlanRep;
using Saas.Repository.PaymentRep;
using Saas.Repository.OrderRep;
using Saas.Repository.OrderItemRep;
using Saas.Repository.AdminRep;
using Saas.Repository.CatalogRep;
using Saas.Services.TenantServices;
using Saas.Mappings;
using Saas.Models;
using Microsoft.AspNetCore.Identity;
using Saas.Services.PlanServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Saas.Services.AuthService;
using System.Configuration;
using Microsoft.OpenApi.Models;
using Saas.Services.AdminService;
using Saas.Services.CatalogService;
using Saas.Services.ProductServices;
using Saas.Services.UserServices;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        builder => builder
            .WithOrigins("https://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); 
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
 c.SwaggerDoc("v1",new OpenApiInfo { Title = "Sistema de tarefas Api", Version = "v1" });

var securitySchema = new OpenApiSecurityScheme
{
    Name = "JWT Autenticação",
    Description = "Entre com o Jwt bearer token",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.Http,
    Scheme = "bearer",
    BearerFormat = "JWT",
    Reference = new OpenApiReference
    {
        Id = JwtBearerDefaults.AuthenticationScheme,
        Type = ReferenceType.SecurityScheme
    }
};
    c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securitySchema);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            securitySchema, new[] { "Bearer" }
        }
    });
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


builder.Services.AddScoped<ITenantRepository, TenantRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IPlanRepository, PlanRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped< IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();

builder.Services.AddScoped<ITenantService, TenantService>();
builder.Services.AddScoped<IPasswordHasher<Tenant>, PasswordHasher<Tenant>>();
builder.Services.AddScoped<IPlanService, PlanService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IPasswordHasher<Admin>, PasswordHasher<Admin>>();
builder.Services.AddScoped<ICatalogService, CatalogService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ITenantProvider, TenantProvider>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IUserAuthService, UserAuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAdminAuthService, AdminAuthService>();

builder.Services.AddHttpContextAccessor();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"]
        };
    });

builder.Services.AddAutoMapper(typeof(TenantMap));
builder.Services.AddAutoMapper(typeof(PlanMap));
builder.Services.AddAutoMapper(typeof(ProductMap));
builder.Services.AddAutoMapper(typeof(UserMap));
builder.Services.AddAutoMapper(typeof(PaymentMap));
builder.Services.AddAutoMapper(typeof(OrderMap));
builder.Services.AddAutoMapper(typeof(OrderItemMap));
builder.Services.AddAutoMapper(typeof(AdminMap));
builder.Services.AddAutoMapper(typeof(CatalogMap));


builder.Logging.AddConsole();
builder.Logging.AddDebug();


var app = builder.Build();

app.UseCors("AllowAngularDev");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngularDev");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
