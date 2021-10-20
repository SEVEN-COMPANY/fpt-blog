using FPTBlog.Src.TagModule.Entity;
using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.CategoryModule.Entity;
using FPTBlog.Utils.Interface;
using FPTBlog.Src.CommentModule.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using FPTBlog.Src.PostModule.Entity;
using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Src.NotificationModule.Entity;

namespace FPTBlog.Utils {
    public class DB : DbContext {
        private IConfig Config;
        public DB(IConfig config) {
            this.Config = config;
        }
        public DbSet<User> User {
            set; get;
        }
        public DbSet<Tag> Tag {
            set; get;
        }
        public DbSet<Comment> Comment {
            set; get;
        }
        public DbSet<Category> Category {
            set; get;
        }
        public DbSet<Post> Post {
            set; get;
        }
        public DbSet<PostTag> PostTag {
            get; set;
        }
        public DbSet<LikePost> LikePost {
            get; set;
        }
        public DbSet<LikeComment> LikeComment {
            get; set;
        }

        public DbSet<FollowInfo> FollowInfo {
            get; set;
        }

        public DbSet<Reward> Reward {
            get; set;
        }

        public DbSet<UserReward> UserReward {
            get; set;
        }

        public DbSet<SavePost> SavePost {
            get; set;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(this.Config.GetEnvByKey("DB_URL"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<PostTag>()
                .HasOne(x => x.Post)
                .WithMany(x => x.PostTags)
                .HasForeignKey(x => x.PostId);

            modelBuilder.Entity<PostTag>()
                .HasOne(x => x.Tag)
                .WithMany(x => x.PostTags)
                .HasForeignKey(x => x.TagId);

            modelBuilder.Entity<FollowInfo>()
                .HasOne(x => x.Follower)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FollowInfo>()
                .HasOne(x => x.FollowingUser)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            // modelBuilder.Entity<LikePost>().HasKey(item => new { item.PostId, item.UserId });

            base.OnModelCreating(modelBuilder);
        }
        public static async Task<Boolean> InitDatabase(IConfig config) {
            var dbContext = new DB(config);
            bool result = await dbContext.Database.EnsureCreatedAsync();
            if (result) {
                Console.WriteLine("Database created");
            }

            return true;
        }
    }
}
