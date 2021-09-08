using System.Data;
using System.Data.SqlClient;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PS.template.accesodatos.Queries;
using PS.template.aplicacion.services;
using SqlKata.Compilers;
using Template.AccessData.Commands;
using Template.AcessData;
using Template.Aplication.Services;
using Template.Domain.Commands;
using Template.Domain.Queries;

namespace TP2_Logica_de_negocios
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //services.AddCors(c => {
            //    c.AddPolicy("AllowOrigin", options => options
            //                                                .AllowAnyOrigin()
            //                                                .AllowAnyMethod()
            //                                                .AllowAnyHeader());
            //});ArgumentException: 

            //services.AddSwaggerGen();
           var connectionString = Configuration.GetSection("ConnectionString").Value; 

            services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));


            //AutoMapper
            var mappingConfig = new MapperConfiguration(mc => 
            {
                mc.AddProfile(new AutoMapping());
            }
            );

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            // SQLKATA
            services.AddTransient<Compiler, SqlServerCompiler>();

            services.AddTransient<IDbConnection>(b =>
            {
                return new SqlConnection(connectionString);
            });
            //services.AddTransient<IRepository, GenericRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IClienteService, ClienteService>();
            services.AddTransient<IClienteQuery, ClienteQuery>();
            services.AddTransient<ILibroRepository, LibroRepository>();
            services.AddTransient<ILibroService, LibroService>();
            services.AddTransient<ILibroQuery, LibroQuery>();
            services.AddTransient<IAlquileresRepository, AlquileresRepository>();
            services.AddTransient<IAlquilerService, AlquilerService>();

            //Swagger
            services.AddSwaggerGen();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

           
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());


            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
