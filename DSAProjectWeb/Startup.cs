using ElectronNET.API;
using ElectronNET.API.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Data.SqlClient;
using DSALib2.SQLDataBase;
using System;

namespace DSAProject2Web
{
    public class Startup
    {
        public const string TALENTJSONFILE = "TalentJsonFile";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string ElectronUserDataPath { get; set; }
        
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            DataBaseConfig();

            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";            
            });
            services.AddSingleton(Configuration);

            var dbPath = getDBPath();
            var dbName = "DSADatabaseTemplate.mdf";
            var newDB = Path.Combine(dbPath, dbName);

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "(localdb)\\MSSQLLocalDB";
            builder.InitialCatalog = dbName;
            builder.IntegratedSecurity = true;
            builder.AttachDBFilename = newDB;
            builder.MultipleActiveResultSets = true;

            //Richtig wäre nur den Connection String des Builders zu übergeben. Dies führt in Electron aber zu einem Assambly fehler den ich noch nicht verstanden habe.
            //Daher ist dieses Konstrukt an übergaben und Anpassungen aktuell nötig
            //Die Connection muss im Context dann angepasst werden, weil sie ansonsten nur einmal erstellt wird
            var connectionString = new SqlConnection(builder.ConnectionString);

            //services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(builder.ConnectionString));
            services.AddDbContext<ApplicationContext>(opts => opts.UseSqlServer(connectionString));
        
            //Configuration
            Configuration.GetSection(TALENTJSONFILE).Value = @"D:\04_Projekte\DSAProject\DSAProjectWeb\Resources\Talente.json";
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error");
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            
            // Open the Electron-Window here
            var x = new BrowserWindowOptions();
            x.TitleBarStyle = TitleBarStyle.hidden;
            
            Task.Run(async () => {
                var window = await Electron.WindowManager.CreateWindowAsync(x);              
                window.SetMenu(null);    
            });
        }
        
        private string getDBPath()
        {
            if (Electron.App.IsReady)
            {
                return Electron.App.GetPathAsync(PathName.UserData).GetAwaiter().GetResult();
            }
            else
            {
                return "C:\\Users\\chris\\AppData\\Roaming\\Electron";
            }
        }

        private void DataBaseConfig()
        {
            string electronUserDataPath = getDBPath();

            var dbName = "DSADatabaseTemplate.mdf";
            var ldfNmae = "DSADatabaseTemplate_log.ldf";

            var currentDirectory = Directory.GetCurrentDirectory();
            var resourceFolder = Path.Combine(currentDirectory, "Resources");
            var oldDB = Path.Combine(resourceFolder, dbName);
            var oldldfDB = Path.Combine(resourceFolder, ldfNmae);

            var newDB = Path.Combine(electronUserDataPath, dbName);
            var newldfDB = Path.Combine(electronUserDataPath, ldfNmae);

            if (System.IO.File.Exists(newDB))
            {
                System.IO.File.Delete(newDB);
            }

            if (System.IO.File.Exists(newldfDB))
            {
                System.IO.File.Delete(newldfDB);
            }

            System.IO.File.Copy(oldDB, newDB);
            System.IO.File.Copy(oldldfDB, newldfDB);
        }
    }
}
