using BlogYes.Domain.Entities;

namespace BlogYes.Domain.Services
{
    public interface ILoginService : IDomainService
    {
        public Task CacheCaptchaAnswerAsync(Captcha captcha, int expire);
        public Task CacheTokenAsync(Guid userId, string token);
        public Task<bool> VerifyCaptchaAnswerAsync(Captcha captcha);
        public Task<bool> VerifyTokenAsync(Guid userId, string token);
    }
}
