using E_CommerceProject.Core.Interfaces;
using E_CommerceProject.Core.Mapping.ProductMapping;
using E_CommerceProject.Environment;
using E_CommerceProject.Infrastructure.Context;
using E_CommerceProject.Infrastructure.files;
using E_CommerceProject.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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

//Dependency Injection
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
builder.Services.AddSingleton<IAppEnvironment, AppEnvironment>();
builder.Services.AddServiceRegistration(builder.Configuration);

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

// Register IFileService and its implementation
builder.Services.AddTransient<IFileService, FileService>();

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
