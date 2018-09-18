using Microsoft.IdentityModel.Tokens;
using System;
using System.Threading.Tasks;

namespace CrowdfundingSystem.API.Options
{
    public class JwtIssuerOptions
    {
        /// <summary>
        /// "iss" (Issuer) claim - idetifies the principal that issued the JWT.
        /// </summary>
        public string Issuer { get; set; }

        ///<summary>
        /// "sub" (Subject) claim - identifies the principal that is the subject of the JWT.
        /// </summary>
        public string Subject { get; set; }

        ///<summary>
        /// "aud" (Audience) claim - identifies the recipients that the JWT is intended for.
        /// </summary>
        public string Audience { get; set; }

        ///<summary>
        /// "exp" (Expiration Time) claim - identifies the expiration time on or after which the JWT MUST NOT be accepted for processing.
        /// </summary>
        public DateTime Expiration => IssuedAt.Add(ValidFor);

        ///<summary>
        /// "nbf" (Not Before) claim - identifies the time before which the JWT MUST NOT be accepted for processing.
        /// </summary>
        public DateTime NotBefore => DateTime.UtcNow;

        ///<summary>
        /// "iat" (Issued At) claim - identifies the time at which the JWT was issued.
        /// </summary>
        public DateTime IssuedAt => DateTime.UtcNow;

        ///<summary>
        /// Set the timespan the token will be valid for (default is 120 min)
        /// </summary>
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);



        ///<summary>
        /// "jti" (JWT ID) claim (default ID is GUID)
        /// </summary>
        public Func<Task<string>> JtiGenerator =>
            () => Task.FromResult(Guid.NewGuid().ToString());

        ///<summary>
        /// The signing key to use when generating tokens.
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
