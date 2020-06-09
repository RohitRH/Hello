using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FirstApi
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
            services.AddSingleton<UsersInterface, UsersRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
     
  .ConfigureApiBehaviorOptions(options => {
         
      options.InvalidModelStateResponseFactory = actionContext => {
          string message = "";
          int i = 1;
          foreach (string item in actionContext.ModelState.Keys)
          {
              //if(!item.Equals("id"))
                //message = message + actionContext.ModelState[item].Errors.FirstOrDefault().ErrorMessage;     
          }
            
          //return new BadRequestObjectResult(new { status = 400,values = actionContext.ModelState.Keys} );

          return CustomErrorResponse(actionContext);
      };
  });
        }

        private BadRequestObjectResult CustomErrorResponse(ActionContext actionContext)
        {
            //string ErrorDescription="";
            List<string> errors = (actionContext.ModelState
             .Where(modelError => modelError.Value.Errors.Count > 0)
             .Select(modelError => 
                  modelError.Value.Errors.FirstOrDefault().ErrorMessage
             ).ToList());
            string message = "";
            int i = 1;
            foreach (var item in errors)
            {
                message +=(i++)+") "+ item+"  ";
            }
            return new BadRequestObjectResult(new { status = 400 , message = message});
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
        }
    }
}
