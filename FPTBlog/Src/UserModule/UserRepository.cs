using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Utils;
using System.Linq;
using System.Collections.Generic;
using System;
using FPTBlog.Utils.Repository;
namespace FPTBlog.Src.UserModule {
    public class UserRepository : Repository<User>, IUserRepository {
        private readonly DB Db;
        public UserRepository(DB db) : base(db) {
            this.Db = db;
        }

        // Đếm những đứa follow mình
        public (List<User>, int) CalculateFollower(string userId) {
            List<FollowInfo> followInfos = this.Db.FollowInfo.Where(item => item.FollowingUserId == userId).ToList();

            List<User> users = new List<User>();
            foreach (var followInfo in followInfos) {
                User foundUser = this.GetFirstOrDefault(item => item.UserId == followInfo.FollowerId);
                if (foundUser != null) {
                    users.Add(foundUser);
                }
            }
            return (users, users.Count);
        }

        // Đếm những thằng mà mình follow nó
        public (List<User>, int) CalculateFollowing(string userId) {
            List<FollowInfo> followInfos = this.Db.FollowInfo.Where(item => item.FollowerId == userId).ToList();

            List<User> users = new List<User>();
            foreach (var followInfo in followInfos) {
                User foundUser = this.GetFirstOrDefault(item => item.UserId == followInfo.FollowingUserId);
                if (foundUser != null) {
                    users.Add(foundUser);
                }
            }
            return (users, users.Count);
        }

        public void FollowUser(User followingUser, User follower) {
            FollowInfo followInfo = new FollowInfo() {
                Follower = follower,
                FollowInfoId = follower.UserId,
                FollowingUser = followingUser,
                FollowingUserId = followingUser.UserId
            };
            this.Db.FollowInfo.Add(followInfo);
            this.Db.SaveChanges();
        }

        public (List<User>, int) GetUsersStatusWithCount(int pageIndex, int pageSize, string searchName, UserStatus searchStatus, UserRole searchRole) {
            List<User> list = (List<User>) this.GetAll(item => item.Name.Contains(searchName) && item.Status == searchStatus && item.Role == searchRole);
            var count = list.Count();
            var pagelist = list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();


            return (pagelist, count);
        }
        public (List<User>, int) GetUsersWithCount(int pageSize, int pageIndex, string searchName) {
            List<User> list = (List<User>) this.GetAll(item => item.Name.Contains(searchName));
            var count = list.Count();
            var pagelist = list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();


            return (pagelist, count);
        }

        public bool IsFollow(string userId, string followerId) {
            List<FollowInfo> followInfos = this.Db.FollowInfo.Where(item => item.FollowerId == followerId && item.FollowingUserId == userId).ToList();
            if (followInfos != null) {
                return true;
            }
            return false;
        }
    }
}
