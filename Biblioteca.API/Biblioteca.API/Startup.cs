using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using Biblioteca.AccessData.BibliotecaDBContext;
using Biblioteca.AccessData.Commands;
using Biblioteca.AccessData.Queries;
using Biblioteca.Application.Services;
using Biblioteca.Domain.Commands;
using Biblioteca.Domain.Queries;
using Biblioteca.Domain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SqlKata.Compilers;
using Biblioteca.Application.Mappers;

namespace Biblioteca.API
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
            //CORS
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });


            services.AddControllers();

            var connectionString = Configuration.GetSection("ConnectionString").Value;
            
            services.AddDbContext<BibliotecaContext>(options => options.UseSqlServer(connectionString));


            //SqlKata
            services.AddTransient<Compiler, SqlServerCompiler>();

            services.AddTransient<IDbConnection>(b =>
            {
                return new SqlConnection(connectionString);
            });

            //AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMappingProfile());
            }
            );

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //Swagger
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Biblioteca APIs v1.0",
                    Description = "Test services"
                });
            });
            services.AddControllers();
            //



            services.AddTransient<IGenericsRepository, GenericsRepository>();

            services.AddTransient<IClienteService, ClienteService>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            services.AddScoped<IAlquilerService, AlquilerService>();
            services.AddScoped<IAlquilerRepository, AlquilerRepository>();

            services.AddScoped<ILibroService, LibroService>();
            services.AddScoped<ILibroRepository, LibroRepository>();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            //Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Biblioteca API V1");
                c.RoutePrefix = string.Empty;
            });



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
