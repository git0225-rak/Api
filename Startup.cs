using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DAO.Services;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using Simpolo_Endpoint;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Options;
using System.Configuration;
using Microsoft.AspNetCore.Http;
using Simpolo_Endpoint.DBUtil;

namespace Simpolo_Endpoint
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IAccount, AccountService>();
            services.AddSingleton<ICommon, CommonService>();
            services.AddSingleton<IMasterData, MasterDataservice>();
            services.AddSingleton<ILogin, LoginService>();
            services.AddSingleton<IItemMasterData, ItemMasterService>();
            services.AddSingleton<IPOSO, POSOService>();
            services.AddSingleton<IAdministration, AdministrationService>();
            services.AddSingleton<IHouseKeeping, HouseKeepingService>();
            services.AddSingleton<IOrders, OrdersService>();
            services.AddSingleton<IInbound, InboundService>();
            services.AddSingleton<IOutbound, OutboundService>();
            services.AddSingleton<IInventory, InventoryServices>();
            services.AddSingleton<IReports, ReportsService>();
            services.AddSingleton<ISAPJsonPostService, SAPJsonPostService>();
            services.AddSingleton<EmailService>();
            services.AddSingleton<Simpolo_Endpoint.DAO.HHTInterface.IInbound, Simpolo_Endpoint.DAO.HHTServices.InboundService>();
            services.AddSingleton<Simpolo_Endpoint.DAO.HHTInterface.IOutbound, Simpolo_Endpoint.DAO.HHTServices.OutboundService>();
            services.AddSingleton<Simpolo_Endpoint.DAO.HHTInterface.ICycleCount, Simpolo_Endpoint.DAO.HHTServices.CycleCountService>();
            services.AddSingleton<Simpolo_Endpoint.DAO.HHTInterface.IScan, Simpolo_Endpoint.DAO.HHTServices.ScanService>();
            services.AddSingleton<Simpolo_Endpoint.DAO.HHTInterface.IHouseKeeping, Simpolo_Endpoint.DAO.HHTServices.HouseKeepingService>();
            services.AddSingleton<Simpolo_Endpoint.DAO.HHTInterface.IInternalTransfer, Simpolo_Endpoint.DAO.HHTServices.InternalTransferService>();
            services.AddSingleton<Simpolo_Endpoint.DAO.HHTInterface.iException, Simpolo_Endpoint.DAO.HHTServices.ExceptionService>();
            services.AddSingleton<Simpolo_Endpoint.DAO.HHTInterface.IGroupOBD, Simpolo_Endpoint.DAO.HHTServices.GroupOBDService>();
            services.AddSingleton<IShipperIDIntegration, ShipperIDIntegrationService>();
            services.AddSingleton<IGroupOBD, GroupOBDService>();
            services.AddSingleton<ISAPJsonPostService, SAPJsonPostService>();
            services.AddHttpClient<ISAPJsonPostService, SAPJsonPostService>();
            services.AddHttpClient<WhatsAppService>();
            services.AddScoped<IWhatappInterface, WhatsAppService>();

            services.AddControllers()
                    .AddXmlSerializerFormatters();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.AddSingleton<IAccount, AccountService>();
            services.AddSingleton<ICommon, CommonService>();


            services.Configure<FileStorage>(Configuration.GetSection("FileStorage"));
            services.AddScoped(sp => sp.GetRequiredService<IOptions<FileStorage>>().Value);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["AppSettings:ValidIssuer"],
                            ValidAudience = Configuration["AppSettings:ValidAudience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AppSettings:IssuerSigningKey"]))
                        };
                    });

            services.AddSwaggerGen(c =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "Authorization header using bearer schema. Please enter your token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                };
                c.AddSecurityDefinition("Bearer", securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, Array.Empty<string>() }
            });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Simpolo_Endpoint", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simpolo_Endpoint v1"));
            }


            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'; script-src 'self' http://192.168.1.20:8089/");
                context.Response.Headers.Add("Strict-Transport-Security", "max-age=31536000; includeSubDomains; preload");
                context.Response.Headers.Add("Referrer-Policy", "no-referrer-when-downgrade");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                context.Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
                context.Response.Headers.Add("Pragma", "no-cache");
                context.Response.Headers.Add("Expires", "0");
                context.Response.Headers.Remove("Server");
                context.Response.Headers.Remove("X-Powered-By");
                await next();
            });

            app.UseCors("AllowAllOrigins");

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<SessionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


}