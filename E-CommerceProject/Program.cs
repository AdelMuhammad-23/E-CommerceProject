using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Core.Mapping.ProductMapping;
using E_CommerceProject.Environment;
using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.files;
using E_CommerceProject.Infrastructure.Repositories;
using E_CommerceProject.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Connect To SQL Server
var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(option =>
option.UseSqlServer(ConnectionString)
);
#endregion

builder.Services.AddHttpContextAccessor();

#region Dependency Injection
//For Business Model
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

//For Authentication
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddTransient<IUserRefreshTokenRepository, UserRefreshTokenRepository>();

//For Account
builder.Services.AddTransient<IAddressRepository, AddressRepository>();

//For GenericRepo
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

//for AutoMapper
builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

//
builder.Services.AddSingleton<IAppEnvironment, AppEnvironment>();

// for ServiceRegistration(Password Settings)
builder.Services.AddServiceRegistration(builder.Configuration);

//For Request info
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});

//EmailService
builder.Services.AddScoped<EmailService>();

// Register IFileService and its implementation
builder.Services.AddTransient<IFileService, FileService>();
#endregion


builder.Services.AddControllers();

#region Allow CORS
var CORS = "_cors";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CORS,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                      });
});
#endregion

var app = builder.Build();

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//    context.Database.Migrate();

//    await ApplicationDbContext.SeedData(context);
//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
