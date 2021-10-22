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

            if (!this.UploadFileService.CheckFileExtension(input.File, new string[] { "svg" })) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.FILE_WRONG_EXTENSION);
                return new BadRequestObjectResult(res.getResponse());
            }

            var imageUrl = this.UploadFileService.Upload(input.File);

            var reward = this.RewardService.GetRewardTypeAndConstraint(input.Type, input.Constraint);
            if (reward != null) {
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_ALLOW);
                return new BadRequestObjectResult(res.getResponse());
            }

            reward = new Reward();
            reward.Name = input.Name;
            reward.Description = input.Description;
            reward.ImageUrl = imageUrl;
            reward.Type = input.Type;
            reward.Constraint = input.Constraint;

            this.RewardService.CreateReward(reward);
            res.data = reward;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("")]
        public ObjectResult HandleUpdateReward([FromForm] UpdateRewardDto input) {
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

                if (!this.UploadFileService.CheckFileExtension(input.File, new string[] { "svg" })) {
                    res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.FILE_WRONG_EXTENSION);
                    return new BadRequestObjectResult(res.getResponse());
                }

                var imageUrl = this.UploadFileService.Upload(input.File);
                reward.ImageUrl = imageUrl;
            }

            reward.Name = input.Name;
            reward.Description = input.Description;
            reward.Type = input.Type;
            reward.Constraint = input.Constraint;
            this.RewardService.UpdateReward(reward);

            res.data = reward;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_UPDATE_SUCCESS);
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
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_REWARD_BELONG_USER);
                return new BadRequestObjectResult(res.getResponse());
            }

            this.RewardService.GiveUserReward(user, reward);
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_ADD_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        [HttpPut("remove")]
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
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_NOT_FOUND);
                return new BadRequestObjectResult(res.getResponse());
            }

            this.RewardService.RemoveUserReward(userReward);
            res.data = userReward;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_DELETE_SUCCESS);
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
                res.setErrorMessage(CustomLanguageValidator.ErrorMessageKey.ERROR_REWARD_BELONG_USER);
                return new BadRequestObjectResult(res.getResponse());
            }

            this.RewardService.DeleteReward(input.RewardId);
            res.data = reward;
            res.setMessage(CustomLanguageValidator.MessageKey.MESSAGE_DELETE_SUCCESS);
            return new ObjectResult(res.getResponse());
        }

        #region Chart
        [HttpGet("chart")]
        public ObjectResult CommentChart() {
            var res = new ServerApiResponse<dynamic>();
            var rewardChart = this.RewardService.GetRewardChart();

            res.data = rewardChart;
            return new ObjectResult(res.getResponse());
        }
        #endregion

    }
}
