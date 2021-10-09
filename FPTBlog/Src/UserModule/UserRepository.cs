using FPTBlog.Src.UserModule.Entity;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Utils;
using System.Linq;
using System.Collections.Generic;
using System;
using FPTBlog.Utils.Repository;
using FPTBlog.Src.PostModule.Entity;

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

        public (List<User>, int) GetUsersStatusAndRoleWithCount(int pageIndex, int pageSize, string searchName, UserStatus searchStatus, UserRole searchRole) {
            List<User> list = new List<User>();
            if (searchStatus == 0 && searchRole == 0) {
                list = (List<User>) this.GetAll(item => item.Name.Contains(searchName));
            }
            else if (searchRole == 0) {
                list = (List<User>) this.GetAll(item => item.Name.Contains(searchName) && item.Status == searchStatus);
            }
            else if (searchStatus == 0) {
                list = (List<User>) this.GetAll(item => item.Name.Contains(searchName) && item.Role == searchRole);
            }
            else {
                list = (List<User>) this.GetAll(item => item.Name.Contains(searchName) && item.Status == searchStatus && item.Role == searchRole);
            }

            var count = list.Count();
            var pagelist = list.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();
            return (pagelist, count);
        }

        public (List<User>, int) GetUsersNameWithCount(int pageSize, int pageIndex, string searchName) {
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

        public ReportUser GetMonthlyReport() {
            ReportUser report = new ReportUser();

            string thisMonth = DateTime.Now.AddMonths(-1).ToShortDateString();
            DateTime thisMonthDate = Convert.ToDateTime(thisMonth);
            string lastMonth = DateTime.Now.AddMonths(-2).ToShortDateString();
            DateTime lastMonthDate = Convert.ToDateTime(lastMonth);

            List<User> users = this.Db.User.ToList();

            foreach (var user in users) {
                DateTime date = Convert.ToDateTime(user.CreateDate);
                if (DateTime.Compare(date, thisMonthDate) > 0) {
                    if (user.Role == UserRole.STUDENT) {
                        report.StudentThisMonth++;
                    }
                    if (user.Role == UserRole.LECTURER) {
                        report.LecturerThisMonth++;
                    }
                }
                if (DateTime.Compare(date, lastMonthDate) > 0 && (DateTime.Compare(date, thisMonthDate) < 0)) {
                    if (user.Role == UserRole.STUDENT) {
                        report.StudentLastMonth++;
                    }
                    if (user.Role == UserRole.LECTURER) {
                        report.LecturerLastMonth++;
                    }
                }
            }
            return report;
        }

        public void SavePost(User user, Post post) {
            SavePost savePost = new SavePost(){
                User = user,
                UserId = user.UserId,
                Post = post,
                PostId = post.PostId
            };
            this.Db.SavePost.Add(savePost);
            this.Db.SaveChanges();
        }

        public (List<Post>, int) GetSavePost(string userId, int pageIndex, int pageSize){
            var query = (from Post in this.Db.Post
                                join SavePost in this.Db.SavePost
                                on Post.PostId equals SavePost.PostId
                                where SavePost.UserId.Equals(userId)
                                select Post).ToList();
            int count = query.Count;
            List<Post> list = query.Take((pageIndex + 1) * pageSize).Skip(pageIndex * pageSize).ToList();

            return (list, count);
        }

    }
}
