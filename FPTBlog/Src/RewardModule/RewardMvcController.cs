using System;

using FPTBlog.Src.AuthModule;
using FPTBlog.Src.RewardModule.Interface;
using FPTBlog.Src.UserModule.Interface;

using FPTBlog.Utils.Common;
using FPTBlog.Utils.Interface;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var now = DateTime.Now;
            if (endDate == null) {
                endDate = now.AddYears(5).ToString("yyyy-MM-dd");
            }
            if (startDate == null) {
                startDate = now.AddYears(-5).ToString("yyyy-MM-dd");
            }


            var (rewardReports, total) = this.RewardService.GetRewardReport(searchName, startDate, endDate, pageSize, pageIndex);
            var rewards = this.RewardService.GetRewardsDropList();

            this.ViewData["rewards"] = new SelectList(rewards, "");
            this.ViewData["total"] = total;
            this.ViewData["rewardReports"] = rewardReports;
            return View(RoutersAdmin.RewardGetHome.Page);
        }

        [Route("update")]
        public IActionResult GetUpdateForm(string rewardId) {
            var reward = this.RewardService.GetRewardByRewardId(rewardId);

            this.ViewData["rewardTypes"] = new SelectList(this.RewardService.GetRewardTypeDropList());
            this.ViewData["reward"] = reward;


            return View(RoutersAdmin.RewardPutUpdate.Page);
        }


        [Route("badge")]
        public IActionResult GetAllBadges(string searchName, int indexPage = 0, int pageSize = 12) {
            if (searchName == null) {
                searchName = "";
            }


            this.ViewData["rewardTypes"] = new SelectList(this.RewardService.GetRewardTypeDropList());
            var (rewards, total) = this.RewardService.GetRewardByName(indexPage, pageSize, searchName);
            this.ViewData["rewards"] = rewards;
            this.ViewData["total"] = total;


            return View(RoutersAdmin.RewardGetBadge.Page);
        }
    }
}
