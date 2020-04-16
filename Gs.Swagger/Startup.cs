using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Gs.Swagger
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
            //ע��Swagger
            services.AddSwaggerGen(c =>
            {
                //V1����˰汾
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Gs.Swagger", //��Ŀ���� 
                    Version = "v1"  //�汾 �ͺ�˰汾û�й�ϵ
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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

            //ע���֮�󣬽���������Swagger����
            app.UseSwagger();
            app.UseSwaggerUI(c => //UI����
            {
                //����1��д�϶�̬���ɵ�json��Ϣ�����еİ汾������SwaggerDoc�еİ汾һ��
                //����2���ĵ�����
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gs.Swagger");
            });
        }
    }
}
