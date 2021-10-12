using System;
using System.Collections.Generic;
using FluentValidation.Results;
using FPTBlog.Src.AuthModule;
using FPTBlog.Src.RewardModule.DTO;
using FPTBlog.Src.RewardModule.Entity;
using FPTBlog.Src.RewardModule.Interface;
using FPTBlog.Src.UserModule.Interface;
using FPTBlog.Utils.Common;
using FPTBlog.Utils.Interface;
using FPTBlog.Utils.Locale;
using Microsoft.AspNetCore.Mvc;

namespace FPTBlog.Src.RewardModule {
    [Route("admin/reward")]
    [ServiceFilter(typeof(AuthGuard))]
    public class RewardMvcController : Controller {

        private readonly IRewardService RewardService;
        private readonly IUserService UserService;
        private readonly IUploadFileService UploadFileService;
        public RewardMvcController(IRewardService rewardService, IUploadFileService uploadFileService, IUserService userService) {
            this.RewardService = rewardService;
            this.UserService = userService;
            this.UploadFileService = uploadFileService;
        }

        [Route("")]
        public IActionResult GetRewards(string searchName, string startDate, string endDate, int pageSize = 12, int pageIndex = 0) {
            if (searchName == null) {
                searchName = "";
            }

            List<RewardReport> rewardReports = this.RewardService.GetRewardReport(searchName, startDate, endDate, pageSize, pageIndex);
            List<Reward> rewards = this.RewardService.GetRewards();

            this.ViewData["rewards"] = rewards;
            this.ViewData["rewardReports"] = rewardReports;
            return View(RoutersAdmin.RewardGetHome.Page);
        }

        [Route("update")]
        public IActionResult GetUpdateForm(string rewardId) {
            var reward = this.RewardService.GetRewardByRewardId(rewardId);
            this.ViewData["reward"] = reward;


            return View(RoutersAdmin.RewardPutUpdate.Page);
        }


        [Route("badge")]
        public IActionResult GetAllBadges(string searchName, int indexPage = 0, int pageSize = 12) {
            if (searchName == null) {
                searchName = "";
            }

            var (rewards, total) = this.RewardService.GetRewardByName(indexPage, pageSize, searchName);
            this.ViewData["rewards"] = rewards;
            this.ViewData["total"] = total;


            return View(RoutersAdmin.RewardGetBadge.Page);
        }
    }
}
