using AutoMapper;
using Base.Jwt;
using Business;
using Business.Generic;
using Business.Token;
using DataAccess;
using DataAccess.Uow;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Schema;
using System.Text;
namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public static JwtConfig JwtConfig { get; private set; }


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            //dbContext
            var dbType = Configuration.GetConnectionString("DbType");
            if (dbType == "Sql")
            {
                var dbConfig = Configuration.GetConnectionString("MsSqlConnection");
                services.AddDbContext<ManagementDbContext>(opts =>
                opts.UseSqlServer(dbConfig));
            }

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });
            services.AddSingleton(config.CreateMapper());

            services.AddScoped<IApartmentService, ApartmentService>();
            services.AddScoped<IBankCardInfoService, BankCardInfoService>();
            services.AddScoped<IDuesService, DuesService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserLogService, UserLogService>();
            services.AddScoped<ITokenService, TokenService>();




            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = JwtConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtConfig.Secret)),
                    ValidAudience = JwtConfig.Audience,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApi Management", Version = "v1.0" });


                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Sim Management for IT Company",
                    Description = "Enter JWT Bearer token **_only_**",
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
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {securityScheme, new string[] { }}
            });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApi v1"));

            }






            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
        }
    }
}

