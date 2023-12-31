using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PropertySolutionHub.Domain.Entities.Shared;
using PropertySolutionHub.Infrastructure.DataAccess;
using PropertySolutionHub.Infrastructure.Service;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        builder.Services.AddIdentity<BaseApplicationUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();
        builder.Services.AddDbContext<IAuthDbContext, AuthDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("AuthConnection")));
        builder.Services.AddDbContext<ILocalDbContext, LocalDbContext>( options => options.UseLazyLoadingProxies().LogTo(Console.WriteLine).UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            

     //  builder.Services.AddDbContext<IAuthDbContext, AuthDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("HostAuthConnection")));
     //  builder.Services.AddDbContext<ILocalDbContext, LocalDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("ZRKE")));
      // builder.Services.AddDbContext<ILocalDbContext, LocalDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("HostDefaultConnection1")));

        var jwtSettings = configuration.GetSection("JWT");
        var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["ValidIssuer"],
                ValidAudience = jwtSettings["ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins("*").AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });
        });

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v2", new OpenApiInfo { Title = "My API", Version = "v2" });
        });
        builder.Services.AddAutoMapper(typeof(Program));

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
        });

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddMemoryCache();
        builder.Services.LoadServices();

         var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseDefaultFiles();
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                   Path.Combine(builder.Environment.ContentRootPath, "Files")),
            RequestPath = "/Files"
        });
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("CorsPolicy");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapSwagger();
        });

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v2/swagger.json", "My API V2");
            c.RoutePrefix = string.Empty;
            c.DocumentTitle = "Test Swagger";
        });

        app.Run();
    }
}