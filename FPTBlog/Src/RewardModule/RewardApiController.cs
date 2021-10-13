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
    [Route("api/reward")]
    [ServiceFilter(typeof(AuthGuard))]
    public class RewardController : Controller {
        private readonly IRewardService RewardService;
        private readonly IUserService UserService;
        private readonly IUploadFileService UploadFileService;

        public RewardController(IRewardService rewardService, IUploadFileService uploadFileService, IUserService userService) {
            this.RewardService = rewardService;
            this.UserService = userService;
            this.UploadFileService = uploadFileService;
        }

        [HttpGet("")]
        public ObjectResult GetRewardHandler(string rewardId) {
            var res = new ServerApiResponse<Reward>();
            var reward = this.RewardService.GetRewardByRewardId(rewardId);

            res.data = reward;
            return new ObjectResult(res.getResponse());
        }
        [HttpGet("userReward")]
        public ObjectResult GetUserRewardHandler(string userId) {
            var res = new ServerApiResponse<List<UserReward>>();
            var rewards = this.RewardService.GetUserAllRewards(userId);

            res.data = rewards;
            return new ObjectResult(res.getResponse());
        }


        [HttpPost("")]
        public ObjectResult CreateRewardHandler([FromForm] CreateRewardDto input) {
            var res = new ServerApiResponse<Reward>();
            ValidationResult result = new CreateRewardDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }
            if (!this.UploadFileService.CheckFileSize(input.File, 5)) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.FILE_TOO_LARGE);
                return new BadRequestObjectResult(res.getResponse());
            }

            if (!this.UploadFileService.CheckFileExtension(input.File, new string[] { "jpg", "png", "jpeg", "gif", "tiff", "svg" })) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.FILE_WRONG_EXTENSION);
                return new BadRequestObjectResult(res.getResponse());
            }

            var imageUrl = this.UploadFileService.Upload(input.File);

            var reward = new Reward();
            reward.RewardId = Guid.NewGuid().ToString();
            reward.Name = input.Name;
            reward.Description = input.Description;
            reward.CreateDate = DateTime.Now.ToShortDateString();
            reward.ImageUrl = imageUrl;
            this.RewardService.CreateReward(reward);
            res.data = reward;
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("")]
        public ObjectResult HandleCreateReward([FromForm] UpdateRewardDto input) {
            var res = new ServerApiResponse<Reward>();
            ValidationResult result = new UpdateRewardDtoValidator().Validate(input);

            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var reward = this.RewardService.GetRewardByRewardId(input.RewardId);
            if (reward == null) {
                if (reward == null) {
                    res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "reward");
                    return new BadRequestObjectResult(res.getResponse());
                }
            }

            if (input.File != null) {

                if (!this.UploadFileService.CheckFileSize(input.File, 5)) {
                    res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.FILE_TOO_LARGE);
                    return new BadRequestObjectResult(res.getResponse());
                }

                if (!this.UploadFileService.CheckFileExtension(input.File, new string[] { "jpg", "png", "jpeg", "gif", "tiff", "svg" })) {
                    res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.FILE_WRONG_EXTENSION);
                    return new BadRequestObjectResult(res.getResponse());
                }

                var imageUrl = this.UploadFileService.Upload(input.File);
                reward.ImageUrl = imageUrl;
            }

            reward.Name = input.Name;
            reward.Description = input.Description;
            this.RewardService.UpdateReward(reward);

            res.data = reward;
            return new ObjectResult(res.getResponse());
        }

        [HttpPost("give")]
        public ObjectResult GiveRewardHandler([FromBody] GiveUserRewardDto input) {
            var res = new ServerApiResponse<UserReward>();
            ValidationResult result = new GiveUserRewardDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var reward = this.RewardService.GetRewardByRewardId(input.RewardId);
            if (reward == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "reward");
                return new BadRequestObjectResult(res.getResponse());
            }

            var user = this.UserService.GetUserByUserId(input.UserId);
            if (user == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "user");
                return new BadRequestObjectResult(res.getResponse());
            }

            var isGive = this.RewardService.GetUserReward(reward, user);
            if (isGive != null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, "user reward");
                return new BadRequestObjectResult(res.getResponse());
            }

            var userReward = new UserReward();
            userReward.UserRewardId = Guid.NewGuid().ToString();
            userReward.CreateDate = DateTime.Now.ToShortDateString();
            userReward.RewardId = input.RewardId;
            userReward.Reward = reward;
            userReward.UserId = input.UserId;
            userReward.User = user;

            this.RewardService.GiveUserReward(userReward);
            res.data = userReward;
            return new ObjectResult(res.getResponse());
        }

        [HttpDelete("remove")]
        public ObjectResult RemoveUserRewardHandler([FromBody] RemoveUserRewardDto input) {
            var res = new ServerApiResponse<UserReward>();
            ValidationResult result = new RemoveUserRewardDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var reward = this.RewardService.GetRewardByRewardId(input.RewardId);
            if (reward == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "reward");
                return new BadRequestObjectResult(res.getResponse());
            }

            var user = this.UserService.GetUserByUserId(input.UserId);
            if (user == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "user");
                return new BadRequestObjectResult(res.getResponse());
            }

            var userReward = this.RewardService.GetUserReward(reward, user);
            if (userReward == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "user reward");
                return new BadRequestObjectResult(res.getResponse());
            }

            this.RewardService.RemoveUserReward(userReward);
            res.data = userReward;
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("delete")]
        public ObjectResult DeleteRewardHandler([FromBody] DeleteRewardDto input) {
            var res = new ServerApiResponse<Reward>();
            ValidationResult result = new DeleteRewardDtoValidator().Validate(input);
            if (!result.IsValid) {
                res.mapDetails(result);
                return new BadRequestObjectResult(res.getResponse());
            }

            var reward = this.RewardService.GetRewardByRewardId(input.RewardId);
            if (reward == null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND, "reward");
                return new BadRequestObjectResult(res.getResponse());
            }

            var deleteReward = this.RewardService.IsUseReward(input.RewardId);
            if (deleteReward != null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_EXISTED, "User Reward");
                return new BadRequestObjectResult(res.getResponse());
            }

            this.RewardService.DeleteReward(input.RewardId);
            res.data = reward;
            return new ObjectResult(res.getResponse());
        }


    }
}
