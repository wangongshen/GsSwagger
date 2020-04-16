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
            //注册Swagger
            services.AddSwaggerGen(c =>
            {
                //V1：后端版本
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Gs.Swagger", //项目名称 
                    Version = "v1"  //版本 和后端版本没有关系
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

            //注册好之后，接下来引用Swagger服务
            app.UseSwagger();
            app.UseSwaggerUI(c => //UI界面
            {
                //参数1：写上动态生成的json信息；其中的版本和上面SwaggerDoc中的版本一致
                //参数2：文档名称
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gs.Swagger");
            });
        }
    }
}
