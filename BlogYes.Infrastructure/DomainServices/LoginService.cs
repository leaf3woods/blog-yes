using BlogYes.Core;
using BlogYes.Domain.Entities;
using BlogYes.Domain.Services;
using StackExchange.Redis;

namespace BlogYes.Infrastructure.DomainServices
{
    public class LoginService : ILoginService
    {
        public LoginService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public async Task CacheCaptchaAnswerAsync(Captcha captcha, int expire)
        {
            var database = _connectionMultiplexer.GetDatabase();
            var key = string.Format(CacheKeyFormatter.Captcha, captcha.Id);
            await database.StringSetAsync(key, captcha.Answer, TimeSpan.FromSeconds(expire));
        }

        public async Task CacheTokenAsync(Guid userId, string token)
        {
            var database = _connectionMultiplexer.GetDatabase();
            var key = string.Format(CacheKeyFormatter.Token, userId);
            await database.StringSetAsync(key, token);
        }

        public async Task<bool> DeleteTokenAsync(Guid userId)
        {
            var databse = _connectionMultiplexer.GetDatabase();
            var key = string.Format(CacheKeyFormatter.Token, userId);
            return await databse.KeyDeleteAsync(key);
        }

        public async Task<bool> VerifyCaptchaAnswerAsync(Captcha captcha)
        {
            var database = _connectionMultiplexer.GetDatabase();
            var key = string.Format(CacheKeyFormatter.Captcha, captcha.Id);
            var raw = (await database.StringGetAsync(key));
            if (!raw.HasValue)
                throw new Exception("captcha is invalid");
            return raw.ToString() == captcha.Answer;
        }

        public async Task<bool> VerifyTokenAsync(Guid userId, string token)
        {
            var database = _connectionMultiplexer.GetDatabase();
            var key = string.Format(CacheKeyFormatter.Token, userId);
            var raw = (await database.StringGetAsync(key));
            return !raw.HasValue || raw.ToString() == token;
        }
    }
}
