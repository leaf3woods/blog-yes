using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BlogYes.Application.Utilities
{
    public static class EncryptUtil
    {
        private static string _privatePem = string.Empty;
        private static string _publicPem = string.Empty;
        public static ECDsa ECDsa { get; private set; } = ECDsa.Create();

        public static void Initialize(string keyFolder)
        {
            var privateKeyPath = Path.Combine(keyFolder, "private-key.pem");
            var publicKeyPath = Path.Combine(keyFolder, "public-key.pem");

            if (!File.Exists(privateKeyPath) || !File.Exists(publicKeyPath))
            {
                ECDsa.GenerateKey(ECCurve.NamedCurves.nistP256);
                _privatePem = ECDsa.ExportECPrivateKeyPem();
                _publicPem = ECDsa.ExportSubjectPublicKeyInfoPem();
                File.WriteAllText(privateKeyPath, _privatePem);
                File.WriteAllText(publicKeyPath, _publicPem);
            }
            else
            {
                _privatePem = File.ReadAllText(privateKeyPath);
                _publicPem = File.ReadAllText(publicKeyPath);
                ECDsa.ImportFromPem(_publicPem);
            }
        }

        public static string GenerateJwtToken(JwtPayload pairs)
        {
            var credentials = new SigningCredentials(new ECDsaSecurityKey(ECDsa), SecurityAlgorithms.EcdsaSha256);
            var token = new JwtSecurityToken(new JwtHeader(credentials), pairs);
            return token.ToString();
        }

        public static string GenerateJwtToken(string issuer, string audience, TimeSpan expires, params Claim[] claims)
        {
            var credentials = new SigningCredentials(new ECDsaSecurityKey(ECDsa), SecurityAlgorithms.EcdsaSha256);
            var nbf = DateTime.UtcNow;
            var expire = nbf + expires;
            var token = new JwtSecurityToken(issuer, audience,
                claims, nbf, expire, credentials);
            return token.ToString();
        }
    }
}
