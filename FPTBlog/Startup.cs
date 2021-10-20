using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

using FluentValidation;
using System.Globalization;

using Microsoft.AspNetCore.Http;

using System.Collections.Generic;
using FPTBlog.Utils.Locale;


using FPTBlog.Utils;
using FPTBlog.Utils.CronJob;
using FPTBlog.Utils.Interface;

using Microsoft.AspNetCore.HttpsPolicy;

using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading.Tasks;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Src.UserModule;
using FPTBlog.Src.AuthModule.Interface;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.CommentModule.Interface;
using FPTBlog.Src.CommentModule;
using FPTBlog.Src.TagModule.Interface;
using FPTBlog.Src.TagModule;
using FPTBlog.Src.CategoryModule.Interface;
using FPTBlog.Src.CategoryModule;
using FPTBlog.Utils.Repository;
using FPTBlog.Utils.Repository.Interface;
using FPTBlog.Src.PostModule.Interface;
using FPTBlog.Src.PostModule;
using FPTBlog.Src.RewardModule.Interface;
using FPTBlog.Src.RewardModule;
using FPTBlog.Src.NotificationModule.Interface;
using FPTBlog.Src.NotificationModule;
using FPTBlog.Src.ChatModule.Interface;
using FPTBlog.Src.ChatModule;

namespace FPTBlog {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration {
            get;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();
            services.AddScoped<IConfig, Config>();
            services.AddScoped<DB>();
            services.AddScoped<IRedisService, RedisService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUploadFileService, UploadFileService>();

            // Auth Module
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<AuthGuard>();
            services.AddScoped<UserFilter>();

            // User Module
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            // Tag Module
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<ITagService, TagService>();

            //Comment Module
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ICommentService, CommentService>();

            // Category Module
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();

            // Blog Module
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostService, PostService>();

            // Reward Module
            services.AddScoped<IRewardRepository, RewardRepository>();
            services.AddScoped<IUserRewardRepository, UserRewardRepository>();
            services.AddScoped<IRewardService, RewardService>();

            // Cron Job
            services.AddCronJob<GiveRewardJob>(c => {
                c.TimeZoneInfo = TimeZoneInfo.Local;
                c.CronExpression = "59 23 * * *"; // 23h59
                // c.CronExpression = "* * * * *"; // every 1 minute
            });

            services.AddSession();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            // Notification Module
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();

            // Chat Module
            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IChatService, ChatService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        async public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfig config) {
            ValidatorOptions.Global.LanguageManager = new CustomLanguageValidator();
            app.UseStatusCodePagesWithRedirects("/error/{0}");

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/error/500");
                app.UseHsts();
            }

            app.Use(next => context => {
                var lang = "en";
                var cookies = new Dictionary<string, string>();
                var values = ((string) context.Request.Headers["Cookie"])?.Split(',', ';');

                if (values != null) {
                    foreach (var parts in values) {
                        var cookieArray = parts.Trim().Split('=');
                        if (cookieArray.Length >= 2) {
                            cookies.Add(cookieArray[0], cookieArray[1]);
                        }
                    }

                    var outValue = "";
                    if (cookies.TryGetValue("lang", out outValue)) {
                        lang = outValue;
                    }
                }
                ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo(lang);
                return next(context);
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthorization();


            app.UseEndpoints(endpoints => {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            await DB.InitDatabase(config);
        }
    }
}
