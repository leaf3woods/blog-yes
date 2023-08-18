using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BlogYes.Application.Utilities
{
    public static class EncryptUtil
    {

        public static ECDsa pubECDsa = ECDsa.Create();
        public static ECDsa privECDsa = ECDsa.Create();

        public static void Initialize(string keyFolder)
        {
            var privateKeyPath = Path.Combine(keyFolder, "private-key.pem");
            var publicKeyPath = Path.Combine(keyFolder, "public-key.pem");

            if (!File.Exists(privateKeyPath) || !File.Exists(publicKeyPath))
            {
                var ecdsa = ECDsa.Create();
                ecdsa.GenerateKey(ECCurve.NamedCurves.nistP256);
                var privatePem = ecdsa.ExportECPrivateKeyPem();
                var publicPem = ecdsa.ExportSubjectPublicKeyInfoPem();
                File.WriteAllText(privateKeyPath, privatePem);
                File.WriteAllText(publicKeyPath, publicPem);
            }
            else
            {
                var privatePem = File.ReadAllText(privateKeyPath);
                var publicPem = File.ReadAllText(publicKeyPath);
                privECDsa.ImportFromPem(privatePem);
                pubECDsa.ImportFromPem(publicPem);
            }
        }
            

        public static string GenerateJwtToken(JwtPayload pairs)
        {
            var credentials = new SigningCredentials(new ECDsaSecurityKey(privECDsa), SecurityAlgorithms.EcdsaSha256);
            var token = new JwtSecurityToken(new JwtHeader(credentials), pairs);
            return token.ToString();
        }

        public static string GenerateJwtToken(string issuer, string audience, TimeSpan expires, params Claim[] claims)
        {
            var credentials = new SigningCredentials(new ECDsaSecurityKey(privECDsa), SecurityAlgorithms.EcdsaSha256);
            var nbf = DateTime.UtcNow;
            var expire = nbf + expires;
            var token = new JwtSecurityToken(issuer, audience,
                claims, nbf, expire, credentials);
            var encode = new JwtSecurityTokenHandler().WriteToken(token);
            return encode;
        }
    }
}