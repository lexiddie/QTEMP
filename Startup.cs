using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace QTEMP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            
            services.AddMemoryCache();
            
            //Add to run Electron.Net
            services.AddMvc ();
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Disable to run Electron.Net 
            // app.UseHttpsRedirection();
            // app.UseCookiePolicy();
            
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            
            //Method for running Electron.Net
            if (HybridSupport.IsElectronActive) {
                ElectronBootstrap ();
            }
        }
        
        private static async void ElectronBootstrap () {
            var browserWindow = await Electron.WindowManager.CreateWindowAsync (new BrowserWindowOptions {
                Width = 1920,
                Height = 1080,
                Show = false
            });

            browserWindow.OnReadyToShow += () => browserWindow.Show();
            browserWindow.SetTitle ("QTEMP");    
        }
    }
}