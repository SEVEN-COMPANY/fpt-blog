using FPTBlog.TagModule.Entity;
using FPTBlog.UserModule.Entity;
using FPTBlog.Utils.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FPTBlog.Utils
{
    public class DB: DbContext
    {
        private IConfig Config;
        public DB(IConfig config)
        {
            this.Config = config;
        }
        public DbSet<User> user { set; get; }
        public DbSet<Tag> tag { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(this.Config.GetEnvByKey("DB_URL"));
        }
        public static async Task<Boolean> InitDatabase(IConfig config)
        {
            var dbContext = new DB(config);
            bool result = await dbContext.Database.EnsureCreatedAsync();
            if (result)
            {
                Console.WriteLine("Database created");
            }

            return true;
        }
    }
}
