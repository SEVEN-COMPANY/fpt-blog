using FPTBlog.Utils.Interface;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace FPTBlog.Utils {
    public class Config : IConfig {
        private readonly IWebHostEnvironment Env;
        public Config(IWebHostEnvironment env) {
            this.Env = env;
        }

        public string GetEnvByKey(string name) {
            string currentEnv = this.Env.EnvironmentName.ToLower();
            string envFileName = "env." + currentEnv + ".json";
            string envPath = Path.Combine(Directory.GetCurrentDirectory(), "config") + "/" + envFileName;

            IConfiguration configs = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(envPath, true, true).Build();
            return configs[name];
        }
    }
}
